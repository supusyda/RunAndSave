using UnityEngine;

public interface IDetectedObj
{
    public Transform detector { get; set; }
    public virtual void OnDetectedObj()
    {

    }
    public virtual void OutDetectedObj()
    {

    }
}
