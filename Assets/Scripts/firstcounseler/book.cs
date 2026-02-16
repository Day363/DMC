using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class book : MonoBehaviour
{
    public GameObject center;
    public bool moving;


    public void Move()
    {
        StartCoroutine(Move_co());
    }

    IEnumerator Move_co()
    {
        moving = true;
        GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.7f);
        yield return new WaitForSeconds(0.71f);
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            if (transform.position.x > center.transform.position.x)
            {
                transform.DOMoveX(transform.position.x + Random.Range(50, 100), 10f).SetId("MoveBook");
            }
            else
            {
                transform.DOMoveX(transform.position.x - Random.Range(50, 100), 10f).SetId("MoveBook");
            }
        }
        else if (i == 1)
        {
            transform.DOMoveY(transform.position.x + Random.Range(50, 100), 10f).SetId("MoveBook");
        }
        yield return new WaitForSeconds(1.1f);
        moving = false;
        Destroy(gameObject);

    }
}
