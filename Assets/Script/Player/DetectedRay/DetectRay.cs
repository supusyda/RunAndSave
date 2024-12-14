using System.Runtime.InteropServices;
using UnityEngine;

public class DetectRay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] int rayNum = 10;
    public float angle = 90;
    public float distant = 10;
    [SerializeField] LayerMask layerMask;
    Logger logger;
    IDetectedObj currentObjectDetect;
    private Transform objHolder;

    void Awake()
    {
        logger = GetComponent<Logger>();
        objHolder = transform.parent.Find("CatHolder");
    }
    void Update()
    {
        Detect();
    }

    void Detect()
    {
        Quaternion rotation = transform.rotation;
        for (int i = 0; i <= rayNum; i++)
        {
            Quaternion rotationMod = Quaternion.AngleAxis((i / (float)rayNum) * angle * 2 - angle, transform.up);
            Vector3 dir = rotation * rotationMod * Vector3.forward;
            bool raycastHit = Physics.Raycast(transform.position, dir, out RaycastHit hitInfo, distant, layerMask);

            if (!raycastHit) continue;// nothing

            hitInfo.collider.transform.TryGetComponent<IDetectedObj>(out currentObjectDetect);
            if (currentObjectDetect == null) continue;// not the right thing

            currentObjectDetect.detector = objHolder;
            currentObjectDetect.OnDetectedObj();

            return; //right thing detected, so stop check other rays
        }

        //not detect current object anymore aka: out of vison
        if (currentObjectDetect == null) return;
        currentObjectDetect.OutDetectedObj();
        currentObjectDetect = null; // out of detectZone


    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Quaternion rotation = transform.rotation;

        for (int i = 0; i <= rayNum; i++)
        {
            // Calculate the rotation for each ray
            Quaternion rotationMod = Quaternion.AngleAxis((i / (float)rayNum) * angle * 2 - angle, transform.up);
            Vector3 dir = rotation * rotationMod * Vector3.forward;

            // Perform the raycast
            bool raycastHit = Physics.Raycast(transform.position, dir, out RaycastHit hitInfo, distant, layerMask);

            // Set gizmo color based on hit
            Gizmos.color = raycastHit ? Color.red : Color.green;

            // Draw the ray
            Gizmos.DrawLine(transform.position, transform.position + dir * distant);

            // Draw a sphere at the hit point if there's a hit
            if (raycastHit)
            {
                Gizmos.DrawSphere(hitInfo.point, 0.2f);
            }
        }
    }
}
