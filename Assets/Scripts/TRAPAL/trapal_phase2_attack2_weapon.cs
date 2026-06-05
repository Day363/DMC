using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_phase2_attack2_weapon : MonoBehaviour
{
    public void Start()
    {
        transform.DORotate(transform.eulerAngles + new Vector3(0, 0, -270f), 1f).SetEase(Ease.OutQuart);
        transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 1f);
        Destroy(gameObject, 1f);
    }
    
}
