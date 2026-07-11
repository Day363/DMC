using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class communicator_effect : MonoBehaviour
{
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject circle4;
    public GameObject circle5;
    public GameObject circle6;

    public float time = 0.15f;

    public void Start()
    {
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        circle1.transform.DOScale(4f, time).SetEase(Ease.OutQuart);
        circle2.transform.DOScale(4f, time).SetEase(Ease.OutQuart);
        circle2.GetComponent<SpriteRenderer>().DOFade(0, time);

        yield return new WaitForSeconds(time);
        Destroy(circle1);
        Destroy(circle2);

        circle3.transform.DOScale(4f, time).SetEase(Ease.OutQuart);
        circle4.transform.DOScale(4f, time).SetEase(Ease.OutQuart);
        circle4.GetComponent<SpriteRenderer>().DOFade(0, time);

        yield return new WaitForSeconds(time);
        Destroy(circle3);
        Destroy(circle4);

        circle5.transform.DOScale(4f, time).SetEase(Ease.OutQuart);
        circle6.transform.DOScale(4f, time).SetEase(Ease.OutQuart);
        circle6.GetComponent<SpriteRenderer>().DOFade(0, time);

        StartCoroutine(Destroy_co());
    }

    IEnumerator Destroy_co()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
