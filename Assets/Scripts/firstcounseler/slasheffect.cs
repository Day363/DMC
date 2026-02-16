using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class slasheffect : MonoBehaviour
{
    public void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, Random.Range(6f, 8f));
        float time = Random.Range(0.1f, 0.3f);
        float yscale = Random.Range(10f, 15f);
        transform.DOScaleX(0, time).SetEase(Ease.OutQuart);
        transform.DOScaleY(yscale, time).SetEase(Ease.OutQuart);
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
