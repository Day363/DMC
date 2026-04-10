using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class effect1 : MonoBehaviour
{
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject circle4;
    public float time = 0.15f;

    public void Start()
    {
        circle1.transform.DOScale(4f, time).SetEase(Ease.OutQuart);
        circle2.transform.DOScale(4f, time).SetEase(Ease.OutQuart);
        circle2.GetComponent<SpriteRenderer>().DOFade(0, time);
        circle3.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        circle4.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        circle3.transform.DOScaleX(0, time).SetEase(Ease.OutQuart);
        circle3.transform.DOScaleY(10, time).SetEase(Ease.OutQuart);
        circle4.transform.DOScaleX(0, time).SetEase(Ease.OutQuart);
        circle4.transform.DOScaleY(10, time).SetEase(Ease.OutQuart);
        StartCoroutine(Destroy_co());
    }

    IEnumerator Destroy_co()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
