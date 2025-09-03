using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_weapon1_radius : MonoBehaviour
{
    public float startradius;
    public float radius;
    public float endradius;
    public float time;

    private void Start()
    {
        radius = startradius;
        DOTween.To(() => radius, x => radius = x, endradius, time).SetEase(Ease.OutQuart);
    }
}
