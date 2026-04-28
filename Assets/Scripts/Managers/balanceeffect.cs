using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class balanceeffect : MonoBehaviour
{
    public GameObject effect1;
    public GameObject effect2;

    public void Start()
    {
        StartCoroutine(Effect());

    }

    IEnumerator Effect()
    {
        effect1.transform.DOScale(new Vector3(0f, 0f, 1), 0.5f).SetEase(Ease.OutQuad);
        effect2.transform.DOScale(new Vector3(0f, 0f, 1), 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
