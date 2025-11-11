using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class background_gun : MonoBehaviour
{
    public float shootypos;
    public float backtime;
    private float normalpos;

    public bool reload;
    public bool cycle;
    public bool cycle2;
    public bool cycle3;

    public void Start()
    {
        if (cycle)
        {
            StartCoroutine(Cycle());
        }
        if (cycle2)
        {
            StartCoroutine(Cycle2());
        }
        if (cycle3)
        {
            StartCoroutine(Cycle3());
        }
    }

    public void Reload()
    {
        if (reload)
        {
            GetComponent<Animator>().SetTrigger("reload");
        }
        
    }

    public void Shoot()
    {
        if (reload)
        {
            GetComponent<Animator>().SetTrigger("shoot");
        }
        
        StartCoroutine(Shoot_co());
    }

    IEnumerator Shoot_co()
    {
        normalpos = transform.localPosition.y;
        transform.DOLocalMoveY(transform.localPosition.y + shootypos, 0.1f);
        yield return new WaitForSeconds(0.11f);
        transform.DOLocalMoveY(normalpos, backtime).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(backtime + 0.1f);
        if (cycle)
        {
            StartCoroutine(Cycle());
        }
        
    }

    IEnumerator Cycle()
    {
        yield return new WaitForSeconds(Random.Range(3f, 6f));
        Reload();
        yield return new WaitForSeconds(Random.Range(1f, 2.5f));
        Shoot();
    }

    IEnumerator Cycle2()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        Reload();
        StartCoroutine(Cycle2());
    }

    IEnumerator Cycle3()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        Shoot();
        StartCoroutine(Cycle3());
    }
}
