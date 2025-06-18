using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerbullet : MonoBehaviour
{
    public int bulletspeed;
    public int damage = 0;

    public bool penetrate;

    public bool truedam;
    public bool slashdam;
    public bool penetratedam;
    public bool blowdam;

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
        if (collision.gameObject.CompareTag("client"))
        {
            if (truedam)
            {
                collision.GetComponent<boss_hpbar>().Damage(damage);
                if (!penetrate)
                {
                    Destroy(gameObject);
                }
            }
            if (slashdam)
            {
                collision.GetComponent<boss_hpbar>().SlashDamage(damage);
                if (!penetrate)
                {
                    Destroy(gameObject);
                }
            }
            if (penetratedam)
            {
                collision.GetComponent<boss_hpbar>().PenetrateDamage(damage);
                if (!penetrate)
                {
                    Destroy(gameObject);
                }
            }
            if (blowdam)
            {
                collision.GetComponent<boss_hpbar>().BlowDamage(damage);
                if (!penetrate)
                {
                    Destroy(gameObject);
                }
            }
        }
    }



    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
