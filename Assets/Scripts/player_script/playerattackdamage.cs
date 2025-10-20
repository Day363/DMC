using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattackdamage : MonoBehaviour
{
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



    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerstatus playerplayerstatus = player.GetComponent<playerstatus>();
        if (collision.gameObject.tag == "client")
        {
            if (canattack)
            {
                if (fixdam)
                {
                    damagepercent = damagepercentCore;
                    damagepercent = damagepercent + damagepercentplus;
                    Debug.Log(damagepercent);
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum * damagepercent);
                    collision.GetComponent<boss_hpbar>().Damage(damage);
                    damagepercentplus = 0;
                }

                if (slash)
                {
                    damagepercent = damagepercentCore;
                    damagepercent = damagepercent + damagepercentplus;
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum * damagepercent);
                    collision.GetComponent<boss_hpbar>().SlashDamage(damage);
                    damagepercentplus = 0;
                }

                if (penetrate)
                {
                    damagepercent = damagepercentCore;
                    damagepercent = damagepercent + damagepercentplus;
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum * damagepercent);
                    collision.GetComponent<boss_hpbar>().PenetrateDamage((int)(damage * playerplayerstatus.penetratedamageup));
                    damagepercentplus = 0;
                }

                if (blow)
                {
                    damagepercent = damagepercentCore;
                    damagepercent = damagepercent + damagepercentplus;
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum * damagepercent);
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
                    damagepercent = damagepercentCore;
                    damagepercent = damagepercent + damagepercentplus;
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum * damagepercent);
                    neh.Damage(damage);
                    damagepercentplus = 0;
                }

                if (slash)
                {

                    damagepercent = damagepercentCore;
                    damagepercent = damagepercent + damagepercentplus;
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum * damagepercent);
                    neh.SlashDamage(damage);
                    damagepercentplus = 0;
                }

                if (penetrate)
                {
                    damagepercent = damagepercentCore;
                    damagepercent = damagepercent + damagepercentplus;
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum * damagepercent);
                    neh.PenetrateDamage((int)(damage * playerplayerstatus.penetratedamageup));
                    damagepercentplus = 0;
                }

                if (blow)
                {
                    damagepercent = damagepercentCore;
                    damagepercent = damagepercent + damagepercentplus;
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum * damagepercent);
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
}
