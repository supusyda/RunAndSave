using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] protected Image fillImage;
    [SerializeField] private float transitionDuration;
    private Coroutine _fillCoroutine;
    // private float _maxDistantce;


    void Awake()
    {

        // fillImage = GetComponent<Image>();
        fillImage.fillAmount = 0;


    }

    public void SetFillAmount(float targetFill)
    {
        targetFill = Mathf.Clamp01(targetFill);

        if (_fillCoroutine != null)
        {
            StopCoroutine(_fillCoroutine); // Stop the previous coroutine if it's running
        }

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
