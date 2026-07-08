using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattackdamage : MonoBehaviour
{
    public static Action<GameObject> Onhit;

    public bool canattack = true;
    public bool canjump = false;

    public GameObject player;
    public float damagenum;
    public int damage;
    public float damagepercentplus = 0;
    public float damagepercent;
    public float damagepercentCore = 1f;
    public bool fixdam;
    public bool slash;
    public bool penetrate;
    public bool blow;

    public void Start()
    {
        player = battalemanager.Instance.player;
        playerskillmove.Whenattackend += DamagepercentplusZero;
    }


    public void Onhit_(Collider2D collision)
    {
        Onhit?.Invoke(collision.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        damagepercent = damagepercentCore;
        damagepercentplus = playerstatus.instance.attackdamageplus + playerstatus.instance.damageincrease_c;//여기에 데미지 증가 변수 떄려박기
        damagepercent = damagepercent + damagepercentplus;

        playerstatus playerplayerstatus = player.GetComponent<playerstatus>();
        if (collision.gameObject.tag == "enemybullet")
        {
            damage = Mathf.RoundToInt(playerplayerstatus.attackpower * damagenum * damagepercent);
            enemybullet collisionenemybullet = collision.GetComponent<enemybullet>();
            collisionenemybullet.damage -= damage;
            if (collisionenemybullet.damage < 0)
            {
                if (collisionenemybullet.canreflect)
                {
                    collisionenemybullet.damage = -collision.GetComponent<enemybullet>().damage;
                    collisionenemybullet.reflected = true;
                    collision.transform.rotation = Quaternion.Euler(0, 0, player.GetComponent<playerskillmove>().attackcore.transform.rotation.z);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        if (collision.gameObject.tag == "client")
        {
            if (canattack)
            {
                if (fixdam)
                {
                    damage = Mathf.RoundToInt(playerplayerstatus.attackpower * damagenum * damagepercent);
                    collision.GetComponent<boss_hpbar>().Damage(damage);
                    damagepercentplus = 0;
                }

                if (slash)
                {
                    damage = Mathf.RoundToInt(playerplayerstatus.attackpower * damagenum * damagepercent);
                    collision.GetComponent<boss_hpbar>().SlashDamage(damage);
                    damagepercentplus = 0;
                }

                if (penetrate)
                { 
                    damage = Mathf.RoundToInt(playerplayerstatus.attackpower * damagenum * damagepercent);
                    collision.GetComponent<boss_hpbar>().PenetrateDamage((int)(damage * (1 + playerplayerstatus.penetratedamageup)));
                    damagepercentplus = 0;
                }

                if (blow)
                {
                    damage = Mathf.RoundToInt(playerplayerstatus.attackpower * damagenum * damagepercent);
                    collision.GetComponent<boss_hpbar>().BlowDamage(damage);
                    damagepercentplus = 0;
                }
            }

            if (canjump && player.GetComponent<PlayerMove>().isJump)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0);
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
            
        }
        if (collision.gameObject.tag == "normalenemy")
        {
            if (collision.TryGetComponent<normal_enemy_hp>(out normal_enemy_hp neh))
            {
                neh.currenthitobjcet = player;
                if (fixdam)
                {
                    damage = Mathf.RoundToInt(playerplayerstatus.attackpower * damagenum * damagepercent);
                    neh.Damage(damage);
                    damagepercentplus = 0;
                }

                if (slash)
                {
                    damage = Mathf.RoundToInt(playerplayerstatus.attackpower * damagenum * damagepercent);
                    neh.SlashDamage(damage);
                    damagepercentplus = 0;
                }

                if (penetrate)
                {
                    damage = Mathf.RoundToInt(playerplayerstatus.attackpower * damagenum * damagepercent);
                    neh.PenetrateDamage((int)(damage * playerplayerstatus.penetratedamageup));
                    damagepercentplus = 0;
                }

                if (blow)
                {
                    damage = Mathf.RoundToInt(playerplayerstatus.attackpower * damagenum * damagepercent);
                    neh.BlowDamage(damage);
                    damagepercentplus = 0;
                }
            }

            if (canjump && player.GetComponent<PlayerMove>().isJump)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0);
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
        }


    }

    public void DamagepercentplusZero()
    {
        damagepercentplus = 0;
    }
}
