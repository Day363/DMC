using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class sqaureeffectloss1 : MonoBehaviour
{
    public bool start;

    public float time;

    public bool tiling;
    public bool offset;
    public bool scale;
    public bool fade;

    public Vector2 settiling;
    public Vector2 setoffset;

    public void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        mr.sortingLayerName = "effect";
        mr.sortingOrder = 100;

        if (start)
        {
            Decrease();
        }
    }

    public void Decrease()
    {
        Material mat = GetComponent<MeshRenderer>().material;

        DOTween.Kill(mat);
        DOTween.Kill(transform);

        Increase();

        if (offset)
        {
            mat.DOOffset(new Vector2(1, 0), time).SetEase(Ease.OutQuart);
            if (fade)
            {
                mat.DOFade(0, time).SetEase(Ease.OutQuad);
            }
        }
        if (tiling)
        {
            mat.DOTiling(new Vector2(10, 0.98f), time).SetEase(Ease.OutQuart);
            if (fade)
            {
                mat.DOFade(0, time).SetEase(Ease.OutQuad);
            }
        }
        if (scale)
        {
            transform.DOScale(new Vector3(-2f ,2f, 1), time).SetEase(Ease.OutQuart);
        }
    }

    public void Increase()
    {
        if (scale)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        GetComponent<MeshRenderer>().material.DOFade(1, 0);
        GetComponent<MeshRenderer>().material.mainTextureOffset = setoffset;
        GetComponent<MeshRenderer>().material.mainTextureScale = settiling;
    }
}
