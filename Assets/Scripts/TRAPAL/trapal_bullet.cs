using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_bullet : MonoBehaviour
{
    public int bulletspeed;
    public int hp = 0;
    public bool reflected = false;
    public GameObject player;

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
        if (collision.gameObject.CompareTag("playerattack"))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, player.GetComponent<playerskillmove>().attackcore.GetComponent<attackcore>().angle));
            reflected = true;
        }
        if (collision.gameObject.CompareTag("enemyobject1"))
        {
            if (collision.gameObject.GetComponent<trapal_gun>().trigger)
            {
                Vector2 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }



    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
