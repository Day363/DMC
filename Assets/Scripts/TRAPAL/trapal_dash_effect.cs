using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_dash_effect : MonoBehaviour
{
    public GameObject effect2;

    public void Start()
    {
        transform.DOScaleY(0, 2f).SetEase(Ease.OutQuad);
        effect2.GetComponent<SpriteRenderer>().material.DOFloat(0, "_power", 1.5f).SetEase(Ease.OutQuad);
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2.1f);
        Destroy(gameObject);
    }
}
