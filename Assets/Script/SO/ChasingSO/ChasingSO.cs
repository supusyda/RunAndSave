using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "ChasingSO", menuName = "Scriptable Objects/ChasingSO")]
public class ChasingSO : ScriptableObject
{
    public List<Phase> phases;
    private Transform _transform;
    private Vector3 targetPosition;
    Sequence sequence;
    // private 
    public ChasingSO(ChasingSO other)
    {
        phases = other.phases;
    }

    public void Init(Transform transform)
    {
        _transform = transform;
        OffsetStartPos();
    }

    void OffsetStartPos()
    {
        _transform.position = new Vector3(5, 0, -phases[0].distance * MyUnit.yMul);//offset Start pos of the chasse in phase 0
        targetPosition = _transform.position;
    }
    protected virtual Tween PhaseMoveToDistance(Phase phase)
    {
        Vector3 direction = Vector3.forward;
        const int offsetX = 5;
        targetPosition = targetPosition + new Vector3(offsetX, 0, (phase.distance * MyUnit.yMul));

        float duration = phase.distance / phase.speed; // Calculate time based on speed and distance
        return _transform.DOMove(targetPosition, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.Log("IS done phase distant" + phase.distance);
            Debug.Log("IS done phase speed" + phase.speed);

        }); // Linear movement
    }

    public void StartMove()
    {
        sequence = DOTween.Sequence();
        for (int i = 0; i < phases.Count; i++)
        {
            sequence.Append(PhaseMoveToDistance(phases[i]));

        }

        sequence.OnComplete(() => Debug.Log("All tweens completed!"));

    }
    public void Stop()
    {
        if (sequence != null && sequence.IsActive())
        {
            sequence.Kill(); // Stop and kill the sequence
            Debug.Log("Sequence stopped.");
        }
    }
    public float GetRoadLength()
    {
        return phases.Max(phases => phases.distance);
    }

    public static ChasingSO GetChasingSOBaseOnThisSO(ChasingSO thisChasingSO, int levelMultiplyer)
    {
        ChasingSO newChasingSO = new ChasingSO(thisChasingSO);
        List<Phase> newCatData = new List<Phase>();
        Phase phase;

        for (int i = 0; i < thisChasingSO.phases.Count; i++)
        {
            if (i == 0) // first phase not chnage the length and speed ??/
            {
                phase = new Phase(thisChasingSO.phases[i].speed, thisChasingSO.phases[i].distance);
            }
            else
            {
                phase = new Phase(thisChasingSO.phases[i].speed * levelMultiplyer, thisChasingSO.phases[i].distance * levelMultiplyer);
            }
            Debug.Log("PhaSE " + i + " new speed" + phase.speed);
            Debug.Log("PhaSE " + i + "new distant" + phase.distance);
            newCatData.Add(phase);
        }
        // newChasingSO.phases.Clear();
        newChasingSO.phases = newCatData;

        return newChasingSO;
    }
}
[Serializable]
public struct Phase
{
    public float speed;
    public float distance;
    public Phase(float speed, float distance)
    {
        this.speed = speed;
        this.distance = distance;

    }

}