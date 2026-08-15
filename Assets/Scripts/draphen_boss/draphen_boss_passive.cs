using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draphen_boss_passive : MonoBehaviour
{
    public GameObject player;

    public bool outofsharp;

    public bool passive4_1;
    public bool passive4_2;
    public bool passive4_3;
    //public bool passive4_4;
    //public bool passive4_5;

    public bool canuseblacksun;

    public void Start()
    {
        boss_hpbar.Onevasion += Onevasion;
        boss_hpbar.OnImmotalityWorked += OnImmotalityWorked;
        enemyattack.Onhit += LoseSharpness;
        enemyattack.OnParryed += LoseSharpness;
        playerstatus.OnHit += Passive2_2;
        playerstatus.OnHit += Passive4_1;
        boss_hpbar.OnDefenseSkillArrey += Passive3;
        playerstatus.OnHitFixDamage += Passive4_2;
        playerstatus.OnHitFixDamage += Passive4_3;
        boss_hpbar.OnCircumStart += UseDarkSun;

        GetComponent<boss_hpbar>().passive_damageplus += 0.20f;
    }

    public void Onevasion(GameObject enemy)
    {
        if (enemy == gameObject)
        {
            GetComponent<boss_hpbar>().HealFocus(1);
        }
    }

    public void OnevasionFailed(GameObject enemy)
    {
        if (enemy == gameObject)
        {
            GetComponent<boss_hpbar>().DecreaseFocus(1);
        }
    }

    public void OnImmotalityWorked(GameObject enemy)
    {
        if (enemy == gameObject)
        {
            GetComponent<Animator>().SetTrigger("baldo");
        }
    }

    public void UsePassive1()
    {
        if (GetComponent<boss_hpbar>().maxhealth / 2 >= GetComponent<boss_hpbar>().currenthealth)
        {
            GetComponent<Animator>().SetTrigger("focus1");
        }
    }

    public void GetSharpness()
    {
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[26], 10);
        if (outofsharp)
        {
            outofsharp = false;
            GetComponent<boss_hpbar>().passive_recieveDamageUp -= 0.5f;
            GetComponent<boss_hpbar>().passive_balancedamageincrease -= 1f;
        }
    }

    public void LoseSharpness(GameObject player, GameObject enemy)
    {
        if (enemy == gameObject)
        {
            GetComponent<boss_hpbar>().RemoveStack(battalemanager.Instance.stackdatas[26], 1);
            boss_hpbar.StackInstance instance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "Ã·¿¹");
            if (instance.currentStack <= 0)
            {
                GetComponent<boss_hpbar>().passive_recieveDamageUp += 0.5f;
                GetComponent<boss_hpbar>().passive_balancedamageincrease += 1f;
                outofsharp = true;
            }
        }
        
    }

    public void GetSharpness2()
    {
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[27], 1);
    }

    public void Passive2_2(GameObject enemy, float dam)
    {
        if (enemy == gameObject)
        {
            playerstatus ps = battalemanager.Instance.player.GetComponent<playerstatus>();
            ps.AdditionalBalanceDamage(gameObject, dam * 0.1f);
        }
    }

    public void UseDarkSun(GameObject enemy)
    {
        if (enemy == gameObject && canuseblacksun)
        {
            canuseblacksun = false;
            GetComponent<Animator>().SetTrigger("focus2");
        }
    }

    public void Passive3(GameObject enemy)
    {
        if (enemy == gameObject)
        {
            boss_hpbar bh = GetComponent<boss_hpbar>();
            bh.currentenemyDefenses[bh.currentenemyDefenses.Count - 1] = bh.phase[bh.currentphase].defense[1];
        }
    }

    public void Passive4_1(GameObject enemy, float dam)
    {
        if (enemy == gameObject &&  passive4_1)
        {
            playerstatus ps = battalemanager.Instance.player.GetComponent<playerstatus>();
            ps.AdditionalBalanceDamage(gameObject, dam * 0.1f);
        }
    }

    public void Passive4_2(GameObject enemy, float dam)
    {
        if (enemy == gameObject && passive4_2)
        {
            GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[28], (int)dam);
        }
    }

    public void Passive4_3(GameObject enemy, float dam)
    {
        if (enemy == gameObject && passive4_3)
        {
            player.GetComponent<playerstatus>().ApplyStack(battalemanager.Instance.stackdatas[28], (int)dam);
        }
    }

    public void Passive4_4()
    {
        GetComponent<boss_hpbar>().phase[GetComponent<boss_hpbar>().currentphase].skills[7].notinclude = false;
        canuseblacksun = true;
    }

    public void Passive4_5()
    {
        GetComponent<boss_hpbar>().mincycle += 5;
        GetComponent<boss_hpbar>().maxcycle += 5;
    }
}
