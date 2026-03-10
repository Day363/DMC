using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battletitleflash : MonoBehaviour
{

    public bool whenstart;

    public void Start()
    {
        if (whenstart)
        {
            Flash();
        }
    }

    public void Flash()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_power", 7f);
        GetComponent<SpriteRenderer>().material.DOFloat(1f, "_power", 1f).SetUpdate(true);
    }
}
