using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "ChasingSO", menuName = "Scriptable Objects/ChasingSO")]
public class ChasingSO : ScriptableObject
{
    public List<Phase> phases;
    private Transform _transform;
    // private 
    public void Init(Transform transform)
    {
        _transform = transform;
        OffsetStartPos();
    }

    void OffsetStartPos()
    {
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, -phases[0].distance * MyUnit.yMul);//offset Start pos of the chasse in phase 0

    }
    protected virtual Tween PhaseMoveToDistance(Phase phase)
    {
        Vector3 direction = Vector3.forward;
        Vector3 targetPosition = _transform.position + direction.normalized * (phase.distance * MyUnit.yMul);
        float duration = phase.distance / phase.speed; // Calculate time based on speed and distance
        return _transform.DOMove(targetPosition, duration).SetEase(Ease.Linear).OnComplete(() => { Debug.Log("DONE Phase"); }); // Linear movement
    }

    public void StartMove()
    {
        Sequence sequence = DOTween.Sequence();
        foreach (Phase phase in phases)
        {
            sequence.Append(PhaseMoveToDistance(phase));

        }
        sequence.OnComplete(() => Debug.Log("All tweens completed!"));

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