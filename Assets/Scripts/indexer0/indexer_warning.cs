using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class indexer_warning : MonoBehaviour
{
     public float time;

    public void Start()
    {
        transform.DOScaleY(0, time).SetEase(Ease.OutQuart);
    }

}
