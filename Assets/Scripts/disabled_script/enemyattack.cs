using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattack : MonoBehaviour
{
    public bool canattack = true;
    public GameObject player;
    public GameObject enemy;
    public float calculation;
    public bool lightattack = false;
    public bool heavyattack = false;

    public bool cantparry = false;
    public bool steadydamage;
    public bool Notifparrysetfalse;

    public bool fixdam;
    public bool slash;
    public bool penetrate;
    public bool blow;

    public bool friendly_hit = false;

    public bool hit = false;

    [System.Serializable]
    public class StackCell
    {
        public Stack stack;
        public bool random;
        public int minstack;
        public int maxstack;
        public int fixstack;
    }

    public List<StackCell> stackcells = new List<StackCell> { };

    [System.Serializable]
    public class Effectcell
    {
        public GameObject effect;
        public bool randomrotation;
    }

    public List<Effectcell> effectcells = new List<Effectcell> { };

    public void Start()
    {
        player = battalemanager.Instance.player;
    }

    public void HitOn()
    {
        hit = true;
    }

    public void Effect()
    {
        if (effectcells.Count > 0)
        {
            foreach (Effectcell cell in effectcells)
            {
                if (!cell.randomrotation)
                {
                    GameObject currenteffect = Instantiate(cell.effect, player.transform.position, Quaternion.identity);
                }
                else if (cell.randomrotation)
                {
                    GameObject currenteffect = Instantiate(cell.effect, player.transform.position, Quaternion.identity);
                    currenteffect.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
                }
            }
        }
    }

    public void StackAdd()
    {
        if (stackcells.Count > 0)
        {
            foreach (StackCell cell in stackcells)
            {
                if (!cell.random)
                {
                    player.GetComponent<playerstatus>().ApplyStack(cell.stack, cell.fixstack);
                }
                else if (cell.random)
                {
                    player.GetComponent<playerstatus>().ApplyStack(cell.stack, Random.Range(cell.minstack, cell.maxstack + 1));
                }
            }
        }
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (canattack)
        {
            if (collision.gameObject.tag == "playerattack" && !cantparry)
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

                if (player.transform.position.x > enemy.transform.position.x)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3f, ForceMode2D.Impulse);
                    if (enemy != null)
                    {
                        if (enemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                        {
                            rb.AddForce(Vector2.right * -3f, ForceMode2D.Impulse);
                        }
                        else if (enemy.transform.parent.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2))
                        {
                            rb2.AddForce(Vector2.right * -3f, ForceMode2D.Impulse);
                        }
                            
                    }
                    
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -3f, ForceMode2D.Impulse);
                    if (enemy != null)
                    {
                        if (enemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                        {
                            rb.AddForce(Vector2.right * 3f, ForceMode2D.Impulse);
                        }
                        else if (enemy.transform.parent.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2))
                        {
                            rb2.AddForce(Vector2.right * 3f, ForceMode2D.Impulse);
                        }
                    }
                    
                }

                if (!Notifparrysetfalse)
                {
                    battalemanager.EnemyAttackDisabled(gameObject);
                }
                else if (Notifparrysetfalse)
                {
                    Destroy(GetComponent<Collider2D>());
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
                            if (!Notifparrysetfalse)
                            {
                                if (!Notifparrysetfalse)
                                {
                                    battalemanager.EnemyAttackDisabled(gameObject);
                                }
                                else if (Notifparrysetfalse)
                                {
                                    Destroy(GetComponent<Collider2D>());
                                }
                            }
                            else if (Notifparrysetfalse)
                            {
                                GetComponent<Collider2D>().enabled = false;
                            }
                            //player.GetComponent<playerhit>().StrongHit(damage, transform);
                            player.GetComponent<playerhit>().Hit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (slash)
                        {
                            hit = false;
                            if (!Notifparrysetfalse)
                            {
                                battalemanager.EnemyAttackDisabled(gameObject);
                            }
                            else if (Notifparrysetfalse)
                            {
                                Destroy(GetComponent<Collider2D>());
                            }
                            //player.GetComponent<playerhit>().SlashStrongHit(damage, transform);
                            player.GetComponent<playerhit>().SlashHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (penetrate)
                        {
                            hit = false;
                            if (!Notifparrysetfalse)
                            {
                                battalemanager.EnemyAttackDisabled(gameObject);
                            }
                            else if (Notifparrysetfalse)
                            {
                                Destroy(GetComponent<Collider2D>());
                            }
                            //player.GetComponent<playerhit>().PenetrateStrongHit(damage, transform);
                            player.GetComponent<playerhit>().PenetrateHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (blow)
                        {
                            hit = false;
                            if (!Notifparrysetfalse)
                            {
                                battalemanager.EnemyAttackDisabled(gameObject);
                            }
                            else if (Notifparrysetfalse)
                            {
                                Destroy(GetComponent<Collider2D>());
                            }
                            //player.GetComponent<playerhit>().BlowStrongHit(damage, transform);
                            player.GetComponent<playerhit>().BlowHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                    }

                    if (lightattack && hit)
                    {
                        if (fixdam)
                        {
                            hit = false;
                            if (!Notifparrysetfalse)
                            {
                                battalemanager.EnemyAttackDisabled(gameObject);
                            }
                            else if (Notifparrysetfalse)
                            {
                                Destroy(GetComponent<Collider2D>());
                            }
                            player.GetComponent<playerhit>().Hit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (slash)
                        {
                            hit = false;
                            if (!Notifparrysetfalse)
                            {
                                battalemanager.EnemyAttackDisabled(gameObject);
                            }
                            else if (Notifparrysetfalse)
                            {
                                Destroy(GetComponent<Collider2D>());
                            }
                            player.GetComponent<playerhit>().SlashHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (penetrate)
                        {
                            hit = false;
                            if (!Notifparrysetfalse)
                            {
                                battalemanager.EnemyAttackDisabled(gameObject);
                            }
                            else if (Notifparrysetfalse)
                            {
                                Destroy(GetComponent<Collider2D>());
                            }
                            player.GetComponent<playerhit>().PenetrateHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (blow)
                        {
                            hit = false;
                            if (!Notifparrysetfalse)
                            {
                                battalemanager.EnemyAttackDisabled(gameObject);
                            }
                            else if (Notifparrysetfalse)
                            {
                                Destroy(GetComponent<Collider2D>());
                            }
                            player.GetComponent<playerhit>().BlowHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                    }
                }
                else if(steadydamage)
                {
                    if (heavyattack && hit)
                    {
                        if (fixdam)
                        {
                            //player.GetComponent<playerhit>().StrongHit(damage, transform);
                            player.GetComponent<playerhit>().Hit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (slash)
                        {
                            //player.GetComponent<playerhit>().SlashStrongHit(damage, transform);
                            player.GetComponent<playerhit>().SlashHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (penetrate)
                        {
                            //player.GetComponent<playerhit>().PenetrateStrongHit(damage, transform);
                            player.GetComponent<playerhit>().PenetrateHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (blow)
                        {
                            //player.GetComponent<playerhit>().BlowStrongHit(damage, transform);
                            player.GetComponent<playerhit>().BlowHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }

                    }

                    if (lightattack && hit)
                    {
                        if (fixdam)
                        {
                            player.GetComponent<playerhit>().Hit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (slash)
                        {
                            player.GetComponent<playerhit>().SlashHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (penetrate)
                        {
                            player.GetComponent<playerhit>().PenetrateHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
                        }
                        if (blow)
                        {
                            player.GetComponent<playerhit>().BlowHit((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation), enemy);
                            StackAdd();
                            Effect();
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
                                neh.SlashDamage((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation));
                            }
                            else if (penetrate)
                            {
                                neh.PenetrateDamage((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation));
                            }
                            else if (blow)
                            {
                                neh.BlowDamage((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation));
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
                                    neh.SlashDamage((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation));
                                    hit = false;
                                    if (!Notifparrysetfalse)
                                    {
                                        battalemanager.EnemyAttackDisabled(gameObject);
                                    }
                                    else if (Notifparrysetfalse)
                                    {
                                        Destroy(GetComponent<Collider2D>());
                                    }
                                }
                                else if (penetrate)
                                {
                                    neh.PenetrateDamage((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation));
                                    hit = false;
                                    if (!Notifparrysetfalse)
                                    {
                                        battalemanager.EnemyAttackDisabled(gameObject);
                                    }
                                    else if (Notifparrysetfalse)
                                    {
                                        Destroy(GetComponent<Collider2D>());
                                    }
                                }
                                else if (blow)
                                {
                                    neh.BlowDamage((int)(enemy.GetComponent<boss_hpbar>().attackpower * calculation));
                                    hit = false;
                                    if (!Notifparrysetfalse)
                                    {
                                        battalemanager.EnemyAttackDisabled(gameObject);
                                    }
                                    else if (Notifparrysetfalse)
                                    {
                                        Destroy(GetComponent<Collider2D>());
                                    }
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
