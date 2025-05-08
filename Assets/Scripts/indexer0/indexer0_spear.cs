using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_spear : MonoBehaviour
{
    public int speed;
    public float stopspeed;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * -speed;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Floor")
        {
            StartCoroutine(Stop());
        }

    }

    IEnumerator Stop()
    {
        Debug.Log("정지대기");
        yield return new WaitForSeconds(stopspeed);
        speed = 0;
    }
}
