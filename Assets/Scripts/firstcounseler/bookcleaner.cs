using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class bookcleaner : MonoBehaviour
{
    public int orderinlayer = 1;

    public void StartMove()
    {
        transform.DOScale(new Vector3(200, 200, 1), 4.5f);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<book>(out book b))
        {
            collision.GetComponent<SpriteRenderer>().sortingOrder = orderinlayer;
            orderinlayer += 1;
            if (!b.moving)
            {
                b.Move();
            }
            
        }
    }
}
