using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class communicator_afterimage_shader : MonoBehaviour
{
    public SpriteRenderer sr;

    public void Start()
    {
        communicator2.OnDissolve += Dissolve;
    }

    public void Dissolve()
    {
        GetComponent<SpriteRenderer>().material.DOFloat(1, "_Dissolve", 0.5f);
    }


}
