using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect3 : MonoBehaviour
{
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject circle4;

    public void Start()
    {
        transform.DORotate(new Vector3(0, 0, 30f), 5f).SetEase(Ease.OutQuad);
        circle3.transform.DOScaleY(0.06f, 5f).SetEase(Ease.OutQuart);
        circle3.transform.DOScaleX(3, 5f).SetEase(Ease.OutQuart);
        circle4.transform.DOScaleY(0.06f, 5f).SetEase(Ease.OutQuart);
        circle4.transform.DOScaleX(3, 5f).SetEase(Ease.OutQuart);
        circle1.GetComponent<SpriteRenderer>().material.DOFloat(5, "_power", 4f);
        circle3.GetComponent<SpriteRenderer>().material.DOFloat(5, "_power", 4f);
        circle4.GetComponent<SpriteRenderer>().material.DOFloat(5, "_power", 4f);
   
    }

 
}
