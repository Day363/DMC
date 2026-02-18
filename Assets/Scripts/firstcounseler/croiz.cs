using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class croiz : MonoBehaviour
{
    public GameObject c1;
    public GameObject c2;
    public GameObject bc;
    public GameObject counseler;
    public GameObject effect1;
    public GameObject effect2;

    public void BookCleaner()
    {
        bc.SetActive(true);
        bc.GetComponent<bookcleaner>().StartMove();
    }

    public void Appear()
    {
        c1.GetComponent<SpriteRenderer>().material.DOFloat(1.1f, "_fade", 1f);
        c2.GetComponent<SpriteRenderer>().material.DOFloat(1f, "_fade", 1f);
    }

    public void Disappear()
    {
        c1.GetComponent<SpriteRenderer>().material.DOFloat(0.1f, "_fade", 0.5f);
        c2.GetComponent<SpriteRenderer>().material.DOFloat(0f, "_fade", 0.5f);
    }

    public void Up()
    {
        transform.DOLocalMoveY(2.6f, 1f).SetEase(Ease.OutQuart);
    }

    public void Down()
    {
        transform.DOLocalMoveY(-4f, 0.2f).SetEase(Ease.InExpo);
        
        StartCoroutine(Disappear_co());
    }

    IEnumerator Disappear_co()
    {
        Instantiate(effect1, transform.position, Quaternion.identity);
        Instantiate(effect2, transform.position, Quaternion.identity);
        BookCleaner();
        counseler.GetComponent<test1>().LightsDown();
        yield return new WaitForSeconds(0.21f);
    }
}
