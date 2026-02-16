using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class eyeflash : MonoBehaviour
{
    public GameObject flash1;
    public GameObject flash2;

    public void Start()
    {
        float time = Random.Range(0.5f, 1.5f);
        float yscale = Random.Range(10f, 15f);
        transform.DORotate(new Vector3(0, 0, Random.Range(90f, 180f)), time).SetEase(Ease.OutQuart);
        flash1.transform.DOScaleX(0, time).SetEase(Ease.OutQuart);
        flash1.transform.DOScaleY(yscale, time).SetEase(Ease.OutQuart);
        flash2.transform.DOScaleX(0, time).SetEase(Ease.OutQuart);
        flash2.transform.DOScaleY(yscale, time).SetEase(Ease.OutQuart);
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
