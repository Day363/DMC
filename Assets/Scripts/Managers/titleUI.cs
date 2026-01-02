using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class titleUI : MonoBehaviour
{
    public GameObject image;
    public string id;

    public void Scale()
    {
        DOTween.Kill(id);
        image.GetComponent<Image>().DOFade(0.6f, 0.15f).SetEase(Ease.OutQuart).SetId(id);
        image.GetComponent<RectTransform>().DOAnchorPosX(-200f, 0.4f).SetEase(Ease.OutQuart).SetId(id);


    }

    public void UnScale()
    {
        DOTween.Kill(id);
        image.GetComponent<Image>().DOFade(0.102f, 0.05f).SetEase(Ease.OutQuart).SetId(id);
        image.GetComponent<RectTransform>().DOAnchorPosX(-243f, 0.2f).SetEase(Ease.OutQuart).SetId(id);
    }
}
