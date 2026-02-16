using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class etchingfade : MonoBehaviour
{
    public void OnEnable()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_power", 10f);
        GetComponent<SpriteRenderer>().material.DOFloat(1f, "_power", 1f);
    }
}
