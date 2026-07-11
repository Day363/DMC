using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class communicator_eyeeffect : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(Tween());
    }

    IEnumerator Tween()
    {
        transform.DOScaleX(6, 2f).SetEase(Ease.OutQuart).SetUpdate(true);
        transform.GetComponent<SpriteRenderer>().DOFade(0, 1.8f).SetEase(Ease.OutQuart).SetUpdate(true);
        yield return new WaitForSecondsRealtime(2.1f);
        Destroy(gameObject);
    }
}
