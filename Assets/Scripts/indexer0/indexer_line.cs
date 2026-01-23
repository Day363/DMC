using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class indexer_line : MonoBehaviour
{
    public int dir;
    public float currentangle;
    public float turnspeed;

    public void Start()
    {
        turnspeed = Random.Range(0.5f, 1.5f);
        if (Random.Range(0, 2) == 1)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }

    }

    public void LookStart()
    {
        
        float i = Random.Range(0.1f, 1f);
        transform.DOScale(new Vector3(i, i, 1), 1f).SetEase(Ease.OutQuart);
        transform.DOLocalMoveZ(Random.Range(30f, 0f), 4.5f).SetEase(Ease.OutQuart);
        StartCoroutine(StartCool());
    }

    IEnumerator StartCool()
    {
        yield return new WaitForSeconds(4.6f);
        StartCoroutine(RandoScale());
        StartCoroutine(RandoTransform());
    }

    public void FixedUpdate()
    {
        currentangle = currentangle + turnspeed * dir;
        transform.localEulerAngles = new Vector3(0, 0, currentangle);
    }

    IEnumerator RandoScale()
    {
        float i = Random.Range(0.3f, 1f);
        transform.DOScale(new Vector3(i, i, 1), 1f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(Random.Range(1.5f, 7f));
        StartCoroutine(RandoScale());
    }

    IEnumerator RandoTransform()
    {
        transform.DOLocalMoveZ(Mathf.Clamp(Random.Range(transform.localPosition.z - 7f, transform.localPosition.z + 7f), 0f, 30f), 1.5f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(Random.Range(1.5f, 7f));
        StartCoroutine(RandoTransform());
    }
}
