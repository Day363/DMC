using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_space_slash : MonoBehaviour
{
    public float angle;
    public float startinput;

    public void Start()
    {
        angle = transform.parent.transform.eulerAngles.z;
        float rad = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        GetComponent<SpriteRenderer>().material.SetVector("_moveto", dir * startinput);
        GetComponent<SpriteRenderer>().material.DOVector(Vector2.zero, "_moveto", 2f).SetEase(Ease.OutQuart);
    }


}
