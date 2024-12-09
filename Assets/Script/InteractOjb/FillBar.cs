using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent<float> OnFillNumberChange = new();
    CanvasGroup _canvasGroup;
    [SerializeField] Image fillImage;
    [SerializeField] private float transitionDuration;
    // static public UnityEvent<Transform> 
    private Coroutine _fillCoroutine;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        // fillImage = GetComponent<Image>();


        _canvasGroup.alpha = 0;
    }
    void OnEnable()
    {
        OnFillNumberChange.AddListener(SetFillAmount);
    }

    void OnDisable()
    {
        OnFillNumberChange.RemoveListener(SetFillAmount);
    }
    public void SetFillAmount(float targetFill)
    {

        if (targetFill == 0)
        {
            StopCoroutine(_fillCoroutine);
            fillImage.fillAmount = 0;
            Debug.Log("0000");
            _canvasGroup.alpha = 0;
            return;
        }
        else
        {
            _canvasGroup.alpha = 1;
        }


        targetFill = Mathf.Clamp01(targetFill);

        if (_fillCoroutine != null)
        {
            StopCoroutine(_fillCoroutine); // Stop the previous coroutine if it's running
        }
        Debug.Log("SDS");
        _fillCoroutine = StartCoroutine(SmoothFill(targetFill));
    }

    private IEnumerator SmoothFill(float targetFill)
    {
        float startFill = fillImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            fillImage.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed / transitionDuration);
            yield return null;
        }

        // Ensure the final value is set
        fillImage.fillAmount = targetFill;
    }


}
