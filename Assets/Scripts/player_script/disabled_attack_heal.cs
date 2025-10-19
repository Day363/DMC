using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabled_attack_heal : MonoBehaviour
{
    public float healpercent;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "client")
        {
            boss_hpbar cb = collision.GetComponent<boss_hpbar>();
            GameObject player = GetComponent<playerattackdamage>().player;

            if (cb.activeStacks.Find(s => s.stackData.effectName == "ÃâÇ÷") != null)
            {
                boss_hpbar.StackInstance bleed = cb.activeStacks.Find(s => s.stackData.effectName == "ÃâÇ÷");
                player.GetComponent<playerstatus>().BalanceHeal(bleed.currentStack * healpercent);
            }

        }
        if (collision.gameObject.tag == "normalenemy")
        {
            //collision.GetComponent<normal_enemy_hp>().ApplyStack(stack, stackamount);
        }
    }
}
