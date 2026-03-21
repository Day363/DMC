using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattack : MonoBehaviour
{
    public bool canattack = true;
    public GameObject player;
    public GameObject enemy;
    public int damage;
    public bool lightattack = false;
    public bool heavyattack = false;
    public float parryback = 35;
    public float selfback;
    public float playerback;
    public float ratiosum;

    public bool steadydamage;
    public bool Notifparrysetfalse;

    public bool fixdam;
    public bool slash;
    public bool penetrate;
    public bool blow;

    public bool friendly_hit = false;

    public bool hit = false;

    public void Start()
    {
        player = battalemanager.Instance.player;
        enemy = battalemanager.Instance.currentenemy;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (canattack)
        {
            if (collision.gameObject.tag == "playerattack")
            {
                int i = Random.Range(0, 3);
                if (i == 0)
                {
                    battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay("clash1");
                }
                else if (i == 1)
                {
                    battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay("clash2");
                }
                else if (i == 2)
                {
                    battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay("clash3");
                }

                attackcore.attackcoreInstance.AttackBackDelayDelete();

                playerstatus.instance.ParrySuccess();
                player.GetComponent<playerstatus>().Parrystop();

                

                if (enemy != null)
                {
                    enemy.GetComponent<boss_hpbar>().BalanceDamage(collision.gameObject.GetComponent<playerattackdamage>().damage);
                }
                

                ratiosum = damage + collision.gameObject.GetComponent<playerattackdamage>().damage;
                selfback = (collision.gameObject.GetComponent<playerattackdamage>().damage / ratiosum) * parryback;
                playerback = (damage / ratiosum) * parryback;

                if (player.transform.position.x > enemy.transform.position.x)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * playerback, ForceMode2D.Impulse);
                    if (enemy != null)
                    {
                        if (enemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                        {
                            rb.AddForce(Vector2.right * -selfback, ForceMode2D.Impulse);
                        }
                        else if (enemy.transform.parent.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2))
                        {
                            rb2.AddForce(Vector2.right * -selfback, ForceMode2D.Impulse);
                        }
                            
                    }
                    
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -playerback, ForceMode2D.Impulse);
                    if (enemy != null)
                    {
                        if (enemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                        {
                            rb.AddForce(Vector2.right * selfback, ForceMode2D.Impulse);
                        }
                        else if (enemy.transform.parent.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2))
                        {
                            rb2.AddForce(Vector2.right * selfback, ForceMode2D.Impulse);
                        }
                    }
                    
                }

                if (!Notifparrysetfalse)
                {
                    battalemanager.EnemyAttackDisabled(gameObject);
                }
                else if (Notifparrysetfalse)
                {
                    GetComponent<Collider2D>().enabled = false;
                }
                
            }

            if (collision.gameObject.tag == "Player")
            {
                
                if (!steadydamage)
                {
                    
                    if (heavyattack && hit)
                    {
                        if (fixdam)
                        {
                            hit = false;
                            player.GetComponent<playerhit>().StrongHit(damage, transform);
                        }
                        if (slash)
                        {
                            hit = false;
                            player.GetComponent<playerhit>().SlashStrongHit(damage, transform);
                        }
                        if (penetrate)
                        {
                            hit = false;
                            player.GetComponent<playerhit>().PenetrateStrongHit(damage, transform);
                        }
                        if (blow)
                        {
                            hit = false;
                            player.GetComponent<playerhit>().BlowStrongHit(damage, transform);
                        }
                    }

                    if (lightattack && hit)
                    {
                        if (fixdam)
                        {
                            hit = false;
                            player.GetComponent<playerhit>().Hit(damage);
                        }
                        if (slash)
                        {
                            hit = false;
                            player.GetComponent<playerhit>().SlashHit(damage);
                        }
                        if (penetrate)
                        {
                            hit = false;
                            player.GetComponent<playerhit>().PenetrateHit(damage);
                        }
                        if (blow)
                        {
                            hit = false;
                            player.GetComponent<playerhit>().BlowHit(damage);
                        }
                    }
                }
                else if(steadydamage)
                {
                    if (heavyattack && hit)
                    {
                        if (fixdam)
                        {
                            player.GetComponent<playerhit>().StrongHit(damage, transform);
                        }
                        if (slash)
                        {
                            player.GetComponent<playerhit>().SlashStrongHit(damage, transform);
                        }
                        if (penetrate)
                        {
                            player.GetComponent<playerhit>().PenetrateStrongHit(damage, transform);
                        }
                        if (blow)
                        {
                            player.GetComponent<playerhit>().BlowStrongHit(damage, transform);
                        }

                    }

                    if (lightattack && hit)
                    {
                        if (fixdam)
                        {
                            player.GetComponent<playerhit>().Hit(damage);
                        }
                        if (slash)
                        {
                            player.GetComponent<playerhit>().SlashHit(damage);
                        }
                        if (penetrate)
                        {
                            player.GetComponent<playerhit>().PenetrateHit(damage);
                        }
                        if (blow)
                        {
                            player.GetComponent<playerhit>().BlowHit(damage);
                        }
                    }
                }

            }

            if (friendly_hit)
            {
                if (steadydamage)
                {
                    if (collision.gameObject.tag == "normalenemy")
                    {
                        if (collision.TryGetComponent<normal_enemy_hp>(out normal_enemy_hp neh))
                        {
                            neh.currenthitobjcet = enemy;
                            if (slash)
                            {
                                neh.SlashDamage(damage);
                            }
                            else if (penetrate)
                            {
                                neh.PenetrateDamage(damage);
                            }
                            else if (blow)
                            {
                                neh.BlowDamage(damage);
                            }
                        }
                    }
                }
                else
                {
                    if (hit)
                    {
                        if (collision.gameObject.tag == "normalenemy")
                        {
                            if (collision.TryGetComponent<normal_enemy_hp>(out normal_enemy_hp neh))
                            {
                                neh.currenthitobjcet = enemy;
                                if (slash)
                                {
                                    neh.SlashDamage(damage);
                                    hit = false;
                                }
                                else if (penetrate)
                                {
                                    neh.PenetrateDamage(damage);
                                    hit = false;
                                }
                                else if (blow)
                                {
                                    neh.BlowDamage(damage);
                                    hit = false;
                                }
                            }
                        }
                    }
                    
                }
                
            }
        }
        
    }
    /*public void OnTriggerStay2D(Collider2D collision)
    {
        if (canattack)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (steadydamage)
                {
                    if (heavyattack)
                    {
                        player.GetComponent<playerhit>().StrongHit(damage, transform);

                    }

                    if (lightattack)
                    {
                        player.GetComponent<playerhit>().Hit(damage);
                    }
                }
            }
        }
        
    }*/




}
