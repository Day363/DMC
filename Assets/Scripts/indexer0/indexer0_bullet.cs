using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_bullet : MonoBehaviour
{
    public int bulletspeed;

    public void Start()
    {
        StartCoroutine(DestroySelf());
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * bulletspeed;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
