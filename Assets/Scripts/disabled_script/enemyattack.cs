using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattack : MonoBehaviour
{
    public static Action<GameObject, GameObject> Onhit;
    public static Action<GameObject, GameObject> OnParryed;

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

    public bool hittoanimation;
    public string animationtrigger;

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

    public void Onhit_()
    {
        Onhit?.Invoke(player, enemy);
    }


    public void Effect()
    {
        if (effectcells.Count > 0)
        {
            foreach (Effectcell cell in effectcells)
            {
                if (!cell.randomrotation)
                {
                    GameObject currenteffect = Instantiate(cell.effect, player.transform);
                    currenteffect.transform.localPosition = Vector2.zero;
                }
                else if (cell.randomrotation)
                {
                    GameObject currenteffect = Instantiate(cell.effect, player.transform);
                    currenteffect.transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f));
                    currenteffect.transform.localPosition = Vector2.zero;
                }
            }
        }
    }

    public void Animation()
    {
        if (hittoanimation)
        {
            enemy.GetComponent<Animator>().SetTrigger(animationtrigger);
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
                    player.GetComponent<playerstatus>().ApplyStack(cell.stack, UnityEngine.Random.Range(cell.minstack, cell.maxstack + 1));
                }
            }
        }
        
    }

    public int Damcalcualtion() //속성별 스텟은 이밖에서
    {
        int finaldam;
        float slashcalculationincrease = 0;
        float slashdamageincrease = 0;

        boss_hpbar bh = enemy.GetComponent<boss_hpbar>();

        if (slash == true)
        {
            slashcalculationincrease += bh.effect_slashcalculationIncrease;
            slashdamageincrease += bh.effect_slashdamageincrease;
        }

        finaldam = (int)((bh.attackpower * (calculation + bh.passive_calculationPlus + slashcalculationincrease)) *
                         (1 + bh.passive_damageplus + slashdamageincrease));

        return finaldam;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (canattack)
        {
            if (collision.gameObject.tag == "playerattack" && !cantparry)
            {
                int i = UnityEngine.Random.Range(0, 3);
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
                
                //if (TryGetComponent<communicator_hitbox_passive>(out communicator_hitbox_passive chp))
                //{
                //    chp.Resolve(player, enemy);
                //}

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
                            player.GetComponent<playerhit>().Hit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
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
                            player.GetComponent<playerhit>().SlashHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
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
                            player.GetComponent<playerhit>().PenetrateHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
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
                            player.GetComponent<playerhit>().BlowHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
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
                            player.GetComponent<playerhit>().Hit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
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
                            player.GetComponent<playerhit>().SlashHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
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
                            player.GetComponent<playerhit>().PenetrateHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
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
                            player.GetComponent<playerhit>().BlowHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
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
                            player.GetComponent<playerhit>().Hit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
                        }
                        if (slash)
                        {
                            //player.GetComponent<playerhit>().SlashStrongHit(damage, transform);
                            player.GetComponent<playerhit>().SlashHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
                        }
                        if (penetrate)
                        {
                            //player.GetComponent<playerhit>().PenetrateStrongHit(damage, transform);
                            player.GetComponent<playerhit>().PenetrateHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
                        }
                        if (blow)
                        {
                            //player.GetComponent<playerhit>().BlowStrongHit(damage, transform);
                            player.GetComponent<playerhit>().BlowHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
                        }

                    }

                    if (lightattack && hit)
                    {
                        if (fixdam)
                        {
                            player.GetComponent<playerhit>().Hit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
                        }
                        if (slash)
                        {
                            player.GetComponent<playerhit>().SlashHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
                        }
                        if (penetrate)
                        {
                            player.GetComponent<playerhit>().PenetrateHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
                        }
                        if (blow)
                        {
                            player.GetComponent<playerhit>().BlowHit(Damcalcualtion(), enemy);
                            StackAdd();
                            Effect();
                            Animation();
                            Onhit_();
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
