using UnityEngine;

public class CanvasFaceCam : MonoBehaviour
{
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }

}
