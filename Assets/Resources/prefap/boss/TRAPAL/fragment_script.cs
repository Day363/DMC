using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragment_script : MonoBehaviour
{
    public GameObject trapal;

    public bool diffusion;
    public bool convergence;

    public float balancedamage;
    public float healbalance;

    public float radius = 5f;        

    public void Die()
    {
        if (diffusion)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    hit.transform.GetComponent<playerstatus>().BalanceDamage(balancedamage);
                }
                if (hit.CompareTag("client"))
                {
                    hit.transform.GetComponent<boss_hpbar>().BalanceDamage(balancedamage);
                }
            }
        }
        else if (convergence)
        {
            if (GetComponent<normal_enemy_hp>().currenthitobjcet.CompareTag("playerattack"))
            {
                GetComponent<normal_enemy_hp>().currenthitobjcet.GetComponent<playerattackdamage>().player.GetComponent<playerstatus>().BalanceDamage(healbalance);
            }
            else if (GetComponent<normal_enemy_hp>().currenthitobjcet.CompareTag("enemyattack"))
            {
                GetComponent<normal_enemy_hp>().currenthitobjcet.GetComponent<enemyattack>().enemy.GetComponent<boss_hpbar>().BalanceDamage(healbalance);
            }
        }
    }

    public void IfDie()
    {
        if (diffusion)
        {
            trapal.GetComponent<boss_hpbar>().RemoveStack(battalemanager.Instance.stackdatas[13], 3);
            trapal.GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[12], 1);
        }
        else if (convergence)
        {
            trapal.GetComponent<boss_hpbar>().RemoveStack(battalemanager.Instance.stackdatas[12], 3);
            trapal.GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[13], 1);
        }
        trapal.GetComponent<trapal_passive>().Fragment();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
