using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class battletitleline : MonoBehaviour
{
    public float wheretogo;
    public float time;

    public void Start()
    {
        transform.DOLocalMoveX(wheretogo, time).SetUpdate(true);
    }
}
