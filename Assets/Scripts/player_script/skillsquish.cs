using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class skillsquish : MonoBehaviour
{
    private void Start()
    {
        transform.DOScaleY(0, 0.5f).SetEase(Ease.OutQuart);
        StartCoroutine(DestoyOb());
    }
    
    IEnumerator DestoyOb()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
