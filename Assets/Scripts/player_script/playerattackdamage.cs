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
    public bool fixdam;
    public bool slash;
    public bool penetrate;
    public bool blow;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "client")
        {
            if (canattack)
            {
                if (fixdam)
                {
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum);
                    collision.GetComponent<boss_hpbar>().Damage(damage);
                }

                if (slash)
                {
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum);
                    collision.GetComponent<boss_hpbar>().SlashDamage(damage);
                }

                if (penetrate)
                {
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum);
                    collision.GetComponent<boss_hpbar>().PenetrateDamage(damage);
                }

                if (blow)
                {
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum);
                    collision.GetComponent<boss_hpbar>().BlowDamage(damage);
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
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum);
                    neh.Damage(damage);
                }

                if (slash)
                {
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum);
                    neh.SlashDamage(damage);
                }

                if (penetrate)
                {
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum);
                    neh.PenetrateDamage(damage);
                }

                if (blow)
                {
                    damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum);
                    neh.BlowDamage(damage);
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
