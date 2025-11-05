using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public float damage;
    public int bulletspeed;
    public bool canreflect;

    public bool reflected;

    public bool fixdam;
    public bool slash;
    public bool penetrate;
    public bool blow;

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
            damage = damage - collision.GetComponent<playerattackdamage>().damage;
            if (damage < 0)
            {
                if (canreflect)
                {
                    damage = -damage;
                    reflected = true;
                    transform.rotation = Quaternion.Euler(0, 0, collision.GetComponent<playerattackdamage>().player.GetComponent<playerskillmove>().attackcore.transform.rotation.z);
                }
                else
                {
                    Destroy(gameObject);
                }
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
            if (!reflected)
            {
                if (fixdam)
                {
                    collision.GetComponent<playerstatus>().BalanceDamage(damage);
                }
                if (slash)
                {
                    collision.GetComponent<playerstatus>().SlashDamage(damage);
                }
                if (penetrate)
                {
                    collision.GetComponent<playerstatus>().PenetrateDamage(damage);
                }
                if (blow)
                {
                    collision.GetComponent<playerstatus>().BlowDamage(damage);
                }
            }
        }
    }
}
