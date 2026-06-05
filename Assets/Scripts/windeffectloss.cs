using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windeffectloss : MonoBehaviour
{
    public float time;

    public void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        mr.sortingLayerName = "effect";
        mr.sortingOrder = 100;

        Material mat = GetComponent<MeshRenderer>().material;
        mat.DOFade(0, time).SetEase(Ease.OutQuad);
        mat.DOOffset(new Vector2(1, 0), time).SetEase(Ease.OutQuart);

        transform.DOScale(new Vector3(transform.localScale.x * 2, transform.localScale.y * 2, 1f), time).SetEase(Ease.OutQuad);
    }
}
