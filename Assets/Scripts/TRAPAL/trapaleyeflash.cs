using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapaleyeflash : MonoBehaviour
{
    public float maxx;
    public float maxy;

    public void OnEnable()
    {
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        transform.DOScaleX(transform.localScale.x * 1.3f, 0.1f).SetEase(Ease.OutQuart);
        GetComponent<SpriteRenderer>().DOFade(1, 0.1f).SetEase(Ease.OutQuart);
        Material mat = GetComponent<SpriteRenderer>().material;
        mat.SetFloat("_power", 0f);
        mat.DOFloat(3.5f, "_power", 0.1f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.1f);
        transform.DOScaleX(transform.localScale.x * 0.5f, 2.5f);
        GetComponent<SpriteRenderer>().DOFade(0, 2.5f);
        mat.DOFloat(1f, "_power", 2.5f);
    }
}
