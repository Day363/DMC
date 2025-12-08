using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class letterboxin : MonoBehaviour
{
    public RectTransform topBar;
    public RectTransform bottomBar;

    public float animationDuration = 0.5f;

    // 원하는 위치 지정
    public float topStartY = 500f;
    public float bottomStartY = -500f;
    public float targetY1 = 0f;
    public float targetY2 = 0f;

    public bool whilein = false;

    public void Awake()
    {
        battalemanager.Instance.letterbox = gameObject;
    }

    public void PlayLetterboxIn()
    {
        if (!whilein)
        {
            // 시작 위치 설정
            topBar.anchoredPosition = new Vector2(0, topStartY);
            bottomBar.anchoredPosition = new Vector2(0, bottomStartY);

            // 지정된 위치로 이동
            topBar.DOAnchorPos(new Vector2(0, targetY1), animationDuration).SetEase(Ease.OutQuart).SetUpdate(true); ;
            bottomBar.DOAnchorPos(new Vector2(0, targetY2), animationDuration).SetEase(Ease.OutQuart).SetUpdate(true); ;

            whilein = true;
        }
        
    }

    public void PlayLetterboxOut()
    {
        // 다시 시작 위치로 이동
        topBar.DOAnchorPos(new Vector2(0, topStartY), animationDuration).SetEase(Ease.InQuart).SetUpdate(true); ;
        bottomBar.DOAnchorPos(new Vector2(0, bottomStartY), animationDuration).SetEase(Ease.InQuart).SetUpdate(true); ;
        whilein = false;
    }
}
