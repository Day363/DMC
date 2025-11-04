using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class circleeffect : MonoBehaviour
{
    public Vector3 targetscale;
    public float duration;

    public void Start()
    {
        transform.DOScale(targetscale, duration).SetEase(Ease.OutCubic).SetUpdate(true);
        GetComponent<SpriteRenderer>().DOFade(0, duration).SetEase(Ease.OutQuart).SetUpdate(true);
        StartCoroutine(Fuck());
    }

    IEnumerator Fuck()
    {
        yield return new WaitForSecondsRealtime(duration + 0.1f);
        Destroy(gameObject);
    }
}
