using Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "CamSO", menuName = "Scriptable Objects/CamSO")]
public class CamSO : ScriptableObject
{
    public CinemachineVirtualCamera phase1Cam;
    public CinemachineVirtualCamera phase2Cam;
}
