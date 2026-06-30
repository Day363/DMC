using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public GameObject enemy;

    public float damage;
    public int bulletspeed;
    public bool canreflect;

    public bool reflected;

    public bool fixdam;
    public bool slash;
    public bool penetrate;
    public bool blow;

    public bool hit;
    public bool heavyattack;
    public bool lightattack;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerattack")
        {
            if (canreflect)
            {
                bulletspeed = bulletspeed * -2;
                reflected = true;
            }
        }
        if (collision.gameObject.tag == "client")
        {
            if (reflected)
            {
                if (slash)
                {
                    collision.GetComponent<boss_hpbar>().SlashDamage((int)damage);
                }
                if (penetrate)
                {
                    collision.GetComponent<boss_hpbar>().PenetrateDamage((int)damage);
                }
                if (blow)
                {
                    collision.GetComponent<boss_hpbar>().BlowDamage((int)damage);
                }
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("playerhit");
            if (!reflected)
            {
                if (heavyattack && hit)
                {
                    if (fixdam)
                    {
                        hit = false;
                        //collision.GetComponent<playerhit>().StrongHit((int)damage, transform);
                        collision.GetComponent<playerhit>().Hit((int)damage, enemy);
                    }
                    if (slash)
                    {
                        hit = false;
                        //collision.GetComponent<playerhit>().SlashStrongHit((int)damage, transform);
                        collision.GetComponent<playerhit>().SlashHit((int)damage, enemy);
                    }
                    if (penetrate)
                    {
                        hit = false;
                        //collision.GetComponent<playerhit>().PenetrateStrongHit((int)damage, transform);
                        collision.GetComponent<playerhit>().PenetrateHit((int)damage, enemy);
                    }
                    if (blow)
                    {
                        hit = false;
                        //collision.GetComponent<playerhit>().BlowStrongHit((int)damage, transform);
                        collision.GetComponent<playerhit>().BlowHit((int)damage, enemy);
                    }
                }

                if (lightattack && hit)
                {
                    if (fixdam)
                    {
                        hit = false;
                        collision.GetComponent<playerhit>().Hit((int)damage, enemy);
                    }
                    if (slash)
                    {
                        hit = false;
                        collision.GetComponent<playerhit>().SlashHit((int)damage, enemy);
                    }
                    if (penetrate)
                    {
                        hit = false;
                        collision.GetComponent<playerhit>().PenetrateHit((int)damage, enemy);
                    }
                    if (blow)
                    {
                        hit = false;
                        collision.GetComponent<playerhit>().BlowHit((int)damage, enemy);
                    }
                }
            }
        }
    }
}
