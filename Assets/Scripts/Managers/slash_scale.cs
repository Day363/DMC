using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class slash_scale : MonoBehaviour
{
    public float time;

    public void Start()
    {
        transform.localScale = new Vector3(Random.Range(0.8f, 1.2f), 1, 1);
        transform.DOScaleY(0, time);
    }

    IEnumerator Ds()
    {
        yield return new WaitForSeconds(time + 0.1f);
        Destroy(gameObject);
    }
}
