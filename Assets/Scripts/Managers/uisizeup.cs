using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uisizeup : MonoBehaviour
{
    public bool isHover;
    public RectTransform rect;

    public float maxsize;
    public float minsize;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        bool nowHover = RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition);

        if (nowHover && !isHover)
        {
            isHover = true;
            ScaleUp();
        }
        else if (!nowHover && isHover)
        {
            isHover = false;
            ScaleDown();
        }
    }

    public void ScaleUp()
    {
        rect.DOKill();
        GetComponent<RectTransform>().DOScale(maxsize, 0.1f).SetId("UIScale1").SetUpdate(true);
    }

    public void ScaleDown()
    {
        rect.DOKill();
        GetComponent<RectTransform>().DOScale(minsize, 0.4f).SetId("UIScale1").SetUpdate(true);
    }
}
