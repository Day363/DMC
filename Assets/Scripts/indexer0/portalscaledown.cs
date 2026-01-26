using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class portalscaledown : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(Scale());
    }

    IEnumerator Scale()
    {
        transform.localScale = new Vector3(0, 0, 1);
        transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.6f);
        transform.DOScale(new Vector3(0, 0, 1), 0.25f).SetEase(Ease.InOutQuad);
    }
}
