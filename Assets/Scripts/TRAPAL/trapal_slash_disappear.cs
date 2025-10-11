using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_slash_disappear : MonoBehaviour
{
    public float time = 1.2f;

    public float scaley;

    public void Start()
    {
        scaley = transform.localScale.y;

        DOTween.To(() => scaley, x => scaley = x, 0, time).SetEase(Ease.OutQuart);
        StartCoroutine(SelfDes());
    }

    public void FixedUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x, scaley, transform.localScale.z);
    }

    IEnumerator SelfDes()
    {
        yield return new WaitForSeconds(time + 0.1f);
        Destroy(gameObject);
    }
}
