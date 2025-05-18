using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_bullet : MonoBehaviour
{
    public int bulletspeed;
    public int hp = 0;
    public bool reflected = false;

    public void Start()
    {
        StartCoroutine(DestroySelf());
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * bulletspeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerattack") && hp >= 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, collision.transform.parent.parent.parent.GetComponent<attackcore>().angle));
            reflected = true;
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
