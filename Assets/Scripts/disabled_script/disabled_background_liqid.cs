using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class disabled_background_liqid : MonoBehaviour
{
    public GameObject ligid;

    public Vector2 wheretospawn;

    public void Start()
    {
        StartCoroutine(Routine());
    }

    IEnumerator Routine()
    {
        while (true)
        {
            StartCoroutine(Spwanliqid());
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        }
    }

    IEnumerator Spwanliqid()
    {
        GameObject currentliqid = Instantiate(ligid, transform);
        currentliqid.transform.localPosition = new Vector3(Random.Range(wheretospawn.x, wheretospawn.y), 0);
        currentliqid.transform.localScale = new Vector3(0, 1, 1);
        float max = Random.Range(0.3f, 1.5f);
        float time = Random.Range(0.7f, 3f);
        currentliqid.transform.DOScaleX(max, time).SetEase(Ease.OutQuart);
        float time2 = Random.Range(3f, 35f);
        currentliqid.transform.DOLocalMoveX(0, time2);
        yield return new WaitForSeconds(time + Random.Range(0f, 1.5f));
        time = Random.Range(0.5f, 2.5f);
        currentliqid.transform.DOScaleX(0, time).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(time + 0.3f);
        Destroy(currentliqid);
    }
}
