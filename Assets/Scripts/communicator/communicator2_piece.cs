using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class communicator2_piece : MonoBehaviour
{
    public float time = 0.5f;
    public int oneminus = 1;
    public bool notease = false;
    public bool random = false;

    public void Start()
    {
        if (random)
        {
            time += Random.Range(-0.6f, 0.2f);
        }

        if (!notease)
        {
            if (!random)
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, 0, -270f * oneminus), time).SetEase(Ease.OutQuart);
            }
            else if (random)
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, 0, Random.Range(-270f, -540f) * oneminus), time).SetEase(Ease.OutQuart);
            }
            transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, time);
            Destroy(gameObject, time + 0.01f);
        }
        else
        {
            if (!random)
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, 0, -270f * oneminus), time);
            }
            else if (random)
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, 0, Random.Range(-270f, -540f) * oneminus), time);
            }

            transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, time);
            Destroy(gameObject, time + 0.01f);
        }
        
    }
}
