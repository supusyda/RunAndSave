using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int thisFinishID;
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.OnPassFinishLine.Invoke(thisFinishID);
        // this.enabled = false;

    }
}
