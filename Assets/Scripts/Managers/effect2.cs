using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect2 : MonoBehaviour
{
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject circle4;

    public void Start()
    {
        transform.DORotate(new Vector3(0, 0, 60f), 5f).SetEase(Ease.OutQuad);
        circle1.transform.DOScale(7f, 3f).SetEase(Ease.OutQuart);
        circle2.transform.DOScale(7.5f, 3f).SetEase(Ease.OutQuart);
        circle3.transform.DOScaleY(0, 5f).SetEase(Ease.OutQuart);
        circle3.transform.DOScaleX(15, 5f).SetEase(Ease.OutQuart);
        circle4.transform.DOScaleY(0, 5f).SetEase(Ease.OutQuart);
        circle4.transform.DOScaleX(15, 5f).SetEase(Ease.OutQuart);
        circle1.GetComponent<SpriteRenderer>().material.DOFloat(15, "_power", 4f);
        circle3.GetComponent<SpriteRenderer>().material.DOFloat(15, "_power", 4f);
        circle4.GetComponent<SpriteRenderer>().material.DOFloat(15, "_power", 4f);
        StartCoroutine(Destroy_co());
    }

    IEnumerator Destroy_co()
    {
        yield return new WaitForSeconds(5.1f);
        Destroy(gameObject);
    }
}
