using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attack_stack : MonoBehaviour
{
    public Stack stack;
    public int stackamount;
    public float percent = 1;
    public bool applyinnextcycle;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "client")
        {
            if (UnityEngine.Random.value < percent)
            {
                if (!applyinnextcycle)
                {
                    collision.GetComponent<boss_hpbar>().ApplyStack(stack, stackamount);
                }
                else if (applyinnextcycle)
                {
                    collision.GetComponent<boss_hpbar>().ApplyStackOnNextCycle(stack, stackamount);
                }
                
            }
        }
        if (collision.gameObject.tag == "normalenemy")
        {
            //collision.GetComponent<normal_enemy_hp>().ApplyStack(stack, stackamount);
        }
    }
}
