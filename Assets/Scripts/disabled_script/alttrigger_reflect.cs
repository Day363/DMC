using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class alttrigger_reflect : MonoBehaviour
{
    private void Start()
    {
        transform.DOScaleY(0, 0.7f).SetEase(Ease.OutQuart);
    }
}
