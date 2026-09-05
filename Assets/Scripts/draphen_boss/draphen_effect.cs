using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class draphen_effect : MonoBehaviour
{
    public GameObject circle1;
    public GameObject circle2;

    public void Start()
    {
        circle1.transform.DOScale(4.5f, 1).SetEase(Ease.OutQuart);
        circle2.transform.DOScale(4.5f, 1).SetEase(Ease.OutQuart);
        circle1.GetComponent<SpriteRenderer>().DOFade(0, 1f);
        circle2.GetComponent<SpriteRenderer>().DOFade(0, 1f);
        Destroy(gameObject, 1.1f);
    }
}
