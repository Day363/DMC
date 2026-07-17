using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class communicator_passive : MonoBehaviour
{
    public int howmuchcalculationplusyoushoot = 0;
    public int focus1int;
    public GameObject effect1;
    public bool phase2;
    public boss_hpbar bh;
    public bool focus1trigger;
    public int focus1triggerint;

    public void Start()
    {
        bh = GetComponent<boss_hpbar>();

        enemyattack.Onhit += Resolve;
        playerhit.OnPlayerEvasionCalled += WhenPlayerEvsioned;
        playerhit.OnPlayerHitCalled += WhenPlayerHit;
        boss_hpbar.OnCycleEnd += Focus1Trigger;

        GetComponent<boss_hpbar>().passive_balancedamagedecrease += 0.35f;
        GetComponent<boss_hpbar>().passive_damageplus += 0.40f;
    }

    public void Update()
    {
        if (!phase2 && (bh.maxhealth / 2 > bh.currenthealth) && GetComponent<communicator>().communicator2.GetComponent<boss_hpbar>().died)
        {
            phase2 = true;
            GetComponent<Animator>().speed = 1;
        }
    }

    public void Resolve(GameObject player, GameObject enemy)
    {
        if (enemy == gameObject)
        {
            playerstatus.StackInstance playerStackInstance = player.GetComponent<playerstatus>().activeStacks.Find(s => s.stackData.effectName == "¿µÅº±Õ¿­");
            if (playerStackInstance.currentStack >= 3)
            {
                player.GetComponent<playerstatus>().RemoveStack(playerStackInstance.stackData, 3);
                enemy.GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[24], 1);

                GameObject currenteffect = Instantiate(effect1, battalemanager.Instance.player.transform);
                currenteffect.transform.localPosition = Vector3.zero;

                playerstatus.StackInstance playerStackInstance1 = player.GetComponent<playerstatus>().activeStacks.Find(s => s.stackData.effectName == "ÀüÈç-Àü´ÞÀÚ");
                player.GetComponent<playerstatus>().BalanceDamage(gameObject, playerStackInstance1.currentStack);
            }
        }
        
    }

    public void WhenPlayerEvsioned(GameObject enemy)
    {
        if (enemy == gameObject)
        {
            howmuchcalculationplusyoushoot += 1;
            GetComponent<boss_hpbar>().passive_calculationPlus += 0.1f;
        }
    }

    public void WhenPlayerHit(GameObject enemy, int dam)
    {
        if (enemy == gameObject)
        {
            GetComponent<boss_hpbar>().passive_calculationPlus -= (0.1f * howmuchcalculationplusyoushoot);
            howmuchcalculationplusyoushoot = 0;
        }
    }

    public void Focus1intPlus()
    {
        if (focus1int < 3)
        {
            focus1int++;
        }
    }

    public void Focus1()
    {
        if (focus1int == 3)
        {
            focus1int = 0;
            GetComponent<boss_hpbar>().UseFocusSkill(0);
        }
    }

    public void Focus1Trigger(GameObject enemy)
    {
        if (enemy == gameObject)
        {
            if (focus1trigger)
            {
                focus1triggerint++;
                if (focus1triggerint == 2)
                {
                    float currentbarrier = bh.currentbarrier;
                    bh.barrierMinus((int)currentbarrier);
                    bh.Heal((int)currentbarrier);
                    focus1trigger = false;
                    focus1triggerint = 0;
                }
            }
        }
        
    }
}
