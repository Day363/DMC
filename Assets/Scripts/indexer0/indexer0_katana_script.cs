using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_katana_script : MonoBehaviour
{
    public int speed;
    public float stopspeed;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * -speed;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            StartCoroutine(Stop());
        }

    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(stopspeed);
        speed = 0;
    }
}
