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
        soundmanager.instance.SoundPlay("book");
        moving = true;
        GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.7f);
        yield return new WaitForSeconds(0.71f);
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            if (transform.position.x > center.transform.position.x)
            {
                transform.DOLocalMoveX(transform.localPosition.x + Random.Range(70, 100), 1.5f).SetId("MoveBook");
            }
            else
            {
                transform.DOLocalMoveX(transform.localPosition.x - Random.Range(70, 100), 1.5f).SetId("MoveBook");
            }
        }
        else if (i == 1)
        {
            transform.DOLocalMoveY(transform.localPosition.y + Random.Range(70, 100), 1.5f).SetId("MoveBook");
        }
        yield return new WaitForSeconds(5f);
        moving = false;
        Destroy(gameObject);

    }
}
