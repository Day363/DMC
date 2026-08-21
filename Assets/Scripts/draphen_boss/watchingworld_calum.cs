using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class watchingworld_calum : MonoBehaviour
{
    public GameObject calum;

    public void Start()
    {
        StartCoroutine(Summon_co());
        transform.DOScaleY(130, 1.5f).SetEase(Ease.InQuart);
        transform.DOScaleX(Random.Range(0.2f, 1.5f), 0.5f).SetEase(Ease.OutQuart);

        int dir = Random.Range(0, 2);
        if (dir == 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 90);

        StartCoroutine(Disappeaar_co());
    }


    IEnumerator Summon_co()
    {
        GameObject curcalum = Instantiate(calum, transform);
        curcalum.transform.localPosition = Vector3.zero;
        curcalum.transform.localScale = new Vector3(0, 1, 1);

        int dir = Random.Range(0, 2);
        float time = Random.Range(1f, 1f);
        float movex = Random.Range(0, 0.7f);

        if (dir == 0)
            curcalum.transform.DOLocalMoveX(movex, time).SetEase(Ease.OutQuart);
        else
            curcalum.transform.DOLocalMoveX(-movex, time).SetEase(Ease.OutQuart);


        float t = movex / 0.5f;
        float thickness = Mathf.Lerp(10f, 0.1f, Mathf.Pow(t, 0.5f));

        curcalum.transform.DOScaleX(thickness, time).SetEase(Ease.OutQuart);
        curcalum.transform.DOScaleY(Random.Range(0.5f, 1.5f), time).SetEase(Ease.OutQuart);

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(Summon_co());

        yield return new WaitForSeconds(time);

        curcalum.transform.DOScaleX(0f, 0.3f).SetEase(Ease.InQuart)
            .OnComplete(() => Destroy(curcalum));
    }

    IEnumerator Disappeaar_co()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
        Disappear();
    }

    public void Disappear()
    {
        transform.DOScaleX(0, 1f).SetEase(Ease.InOutQuart);
        Destroy(gameObject, 1.1f);
    }
}
