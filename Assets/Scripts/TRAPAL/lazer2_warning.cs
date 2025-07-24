using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class lazer2_warning : MonoBehaviour
{
    public float scaley;

    public float time = 1.2f;

    public void Start()
    {
        DOTween.To(() => scaley, x => scaley = x, 0, time).SetEase(Ease.OutQuart);
        StartCoroutine(SelfDes());
    }

    public void FixedUpdate()
    {
        transform.localScale = new Vector3(1, scaley, 1);
    }

    IEnumerator SelfDes()
    {
        yield return new WaitForSeconds(time + 0.1f);
        Destroy(gameObject);
    }

}
