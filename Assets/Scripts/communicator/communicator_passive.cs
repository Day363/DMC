using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class communicator_passive : MonoBehaviour
{
    public int howmuchcalculationplusyoushoot = 0;
    public GameObject effect1;

    public void Start()
    {
        enemyattack.Onhit += Resolve;
        playerhit.OnPlayerEvasionCalled += WhenPlayerEvsioned;
        playerhit.OnPlayerHitCalled += WhenPlayerHit;

        GetComponent<boss_hpbar>().passive_balancedamagedecrease += 0.35f;
        GetComponent<boss_hpbar>().passive_damageplus += 0.40f;
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
}
