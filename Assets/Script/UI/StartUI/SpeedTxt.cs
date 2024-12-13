using TMPro;
using UnityEngine;

public class SpeedTxt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    TMP_Text _text;


    public void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetText(float speed)
    {
        if (_text == null) return;
        _text.text = "Speed: " + speed.ToString();
    }
}
