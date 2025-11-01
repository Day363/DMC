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

    public bool slash;
    public bool penetrate;
    public bool blow;

    public bool friendly_hit = false;

    public bool hit = false;

    public void OnEnable()
    {
        hit = true;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (canattack)
        {
            if (collision.gameObject.tag == "playerattack")
            {
                attackcore.attackcoreInstance.AttackBackDelayDelete();

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
                        enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -selfback, ForceMode2D.Impulse);
                    }
                    
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -playerback, ForceMode2D.Impulse);
                    if (enemy != null)
                    {
                        enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.right * selfback, ForceMode2D.Impulse);
                    }
                    
                }

                if (!Notifparrysetfalse)
                {
                    battalemanager.EnemyAttackDisabled(gameObject);
                }
                else if (Notifparrysetfalse)
                {
                    GetComponent<Collider>().enabled = false;
                }
                
            }

            if (collision.gameObject.tag == "Player")
            {
                
                if (!steadydamage)
                {
                    
                    if (heavyattack)
                    {
                        Debug.Log("ada");
                        heavyattack = false;
                        player.GetComponent<playerhit>().StrongHit(damage, transform);

                    }

                    if (lightattack)
                    {
                        Debug.Log("ada");
                        lightattack = false;
                        player.GetComponent<playerhit>().Hit(damage);
                    }
                }
                else if(steadydamage)
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
