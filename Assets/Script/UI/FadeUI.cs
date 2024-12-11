using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    [SerializeField] float timefade;
    private Vector3 startPos;
    private RectTransform rectTransform;
    private void OnEnable()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        FadeIn();
    }
    void FadeIn()
    {

        rectTransform.localScale = Vector3.zero;
        rectTransform.anchoredPosition = new Vector3(0, -100, 0);
        rectTransform.DOAnchorPos(startPos, timefade).SetEase(Ease.OutElastic);
        rectTransform.DOScale(new Vector3(1, 1, 1), timefade).onComplete += () =>
        {
            if (gameObject.activeInHierarchy == false) return;
            // StartCoroutine(FadeOut());
        };



    }
    public void OnClose()
    {
        StartCoroutine(FadeOut());

    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0);
        rectTransform.localScale = Vector3.one;
        rectTransform.DOScale(new Vector3(0, 0, 0), timefade).SetEase(Ease.InOutQuint).onComplete += () =>
        {
            transform.gameObject.SetActive(false);
        };

    }
}
