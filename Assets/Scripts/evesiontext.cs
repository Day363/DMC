using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class evesiontext : MonoBehaviour
{
    public void Start()
    {
        transform.DOLocalMoveY(transform.position.y + Random.Range(3f, 5f), 2.5f).SetEase(Ease.OutQuart);
        GetComponent<TMP_Text>().DOFade(0, 2.7f);
        StartCoroutine(DestroyOb());
    }

    IEnumerator DestroyOb()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
