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
    Sequence sequence;
    // private 
    public void Init(Transform transform)
    {
        _transform = transform;
        OffsetStartPos();
    }

    void OffsetStartPos()
    {
        _transform.position = new Vector3(5, 0, -phases[0].distance * MyUnit.yMul);//offset Start pos of the chasse in phase 0

    }
    protected virtual Tween PhaseMoveToDistance(Phase phase)
    {
        Vector3 direction = Vector3.forward;
        const int offsetX = 5;
        Vector3 targetPosition = new Vector3(offsetX, 0, (phase.distance * MyUnit.yMul));
        Debug.Log("cccccc" + (phase.distance * MyUnit.yMul));
        Debug.Log("TARGETGO" + targetPosition.z);
        float duration = phase.distance / phase.speed; // Calculate time based on speed and distance
        return _transform.DOMove(targetPosition, duration).SetEase(Ease.Linear).OnComplete(() => { Debug.Log("DONE Phase"); }); // Linear movement
    }

    public void StartMove()
    {
        sequence = DOTween.Sequence();
        foreach (Phase phase in phases)
        {
            sequence.Append(PhaseMoveToDistance(phase));

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