using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class communicator2_passive : MonoBehaviour
{
    public boss_hpbar bh;
    public bool phase2trigger;
    public GameObject communicator1_slashcore;

    public void Start()
    {
        battalemanager.WhenBattleStart += Passive1;
        battalemanager.WhenBattleStart += Passive2;
        boss_hpbar.Die += Passive1_End;
        playerstatus.OnHit += Passive1_2;

        bh = GetComponent<boss_hpbar>();
    }

    public void FixedUpdate()
    {
        if (bh.maxhealth / 2 >= bh.currenthealth && !phase2trigger)
        {
            phase2trigger = true;
            GetComponent<communicator2>().Phase2();
            if (GetComponent<communicator2>().communicator.GetComponent<boss_hpbar>().died == false)
            {
                communicator1_slashcore.SetActive(true);
                GetComponent<communicator2>().communicator.GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[25], 1);
            }
        }
    }

    public void Passive1()
    {
        playerstatus ps = battalemanager.Instance.player.GetComponent<playerstatus>();
        ps.enemypassive_slash_tolerance_up += 0.2f;
        ps.enemypassive_penetration_tolerance_up += 0.2f;
        ps.enemypassive_blow_tolerance_up += 0.2f;
    }

    public void Passive1_End(GameObject enemy)
    {
        if (enemy == gameObject)
        {
            playerstatus ps = battalemanager.Instance.player.GetComponent<playerstatus>();
            ps.enemypassive_slash_tolerance_up -= 0.2f;
            ps.enemypassive_penetration_tolerance_up -= 0.2f;
            ps.enemypassive_blow_tolerance_up -= 0.2f;
        }
    }

    public void Passive1_2(GameObject enemy, float dam)
    {
        if (enemy == gameObject)
        {
            playerstatus ps = battalemanager.Instance.player.GetComponent<playerstatus>();
            if (ps.slash_tolerance + ps.enemypassive_slash_tolerance_up >= ps.penetration_tolerance + ps.enemypassive_penetration_tolerance_up && ps.slash_tolerance + ps.enemypassive_slash_tolerance_up >= ps.blow_tolerance + ps.enemypassive_blow_tolerance_up)
            {
                ps.AdditionalSlashDamage(gameObject, dam * 0.2f);
            }
            else if (ps.penetration_tolerance + ps.enemypassive_penetration_tolerance_up >= ps.slash_tolerance + ps.enemypassive_slash_tolerance_up && ps.penetration_tolerance + ps.enemypassive_penetration_tolerance_up >= ps.blow_tolerance + ps.enemypassive_blow_tolerance_up)
            {
                ps.AdditionalPenetrateDamage(gameObject, dam * 0.2f);
            }
            else
            {
                ps.AdditionalBlowDamage(gameObject, dam * 0.2f);
            }
        }
    }

    public void Passive2()
    {
        GetComponent<boss_hpbar>().passive_damageplus += 0.4f;
    }
}
