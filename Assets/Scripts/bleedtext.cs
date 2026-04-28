using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bleedtext : MonoBehaviour
{
    public void Start()
    {
        transform.DOLocalMoveY(transform.position.y + Random.Range(3f, 5f), 2.5f).SetEase(Ease.OutQuart);
        transform.GetChild(0).GetComponent<TMP_Text>().DOFade(0, 2.7f);
        transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, 2.7f);
        StartCoroutine(DestroyOb());
    }

    IEnumerator DestroyOb()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
