using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class draphen_boss_passive : MonoBehaviour
{
    public GameObject player;
    public GameObject effect;

    public bool outofsharp;

    public bool passive4_1;
    public bool passive4_2;
    public bool passive4_3;
    //public bool passive4_4;
    //public bool passive4_5;
    private bool[] used = new bool[5];

    public bool canuseblacksun;
    public bool whileblacksun;

    public int sheathingcount = 0;
    public GameObject effectpos;
    public GameObject darksuneffect;
    public GameObject darksun;
    public GameObject cammanager;

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
        enemyattack.OnIndex += AttackFunction;

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
            GetComponent<Animator>().SetTrigger("sheathing");
        }
    }

    public void Effect1()
    {
        GameObject cureffect = Instantiate(effect, effectpos.transform.position, Quaternion.identity);
    }

    public void UsePassive1()
    {
        sheathingcount++;

        if (GetComponent<boss_hpbar>().maxhealth / 2 >= GetComponent<boss_hpbar>().currenthealth && sheathingcount >= 3)
        {
            GetComponent<Animator>().SetTrigger("focus1");
            sheathingcount = 0;
        }
    }

    public void GetSharpness()
    {
        Debug.Log("ApplyStack");
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
            boss_hpbar.StackInstance instance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "첨예");
            if (instance != null)
            {
                if (instance.currentStack <= 0 && !outofsharp)
                {
                    GetComponent<boss_hpbar>().passive_recieveDamageUp += 0.5f;
                    GetComponent<boss_hpbar>().passive_balancedamageincrease += 1f;
                    outofsharp = true;
                }
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
        if (enemy == gameObject && canuseblacksun && GetComponent<boss_hpbar>().maxhealth / 2 >= GetComponent<boss_hpbar>().currenthealth)
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
            player.GetComponent<playerstatus>().ApplyStack(battalemanager.Instance.stackdatas[29], (int)dam);
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

    public void Passive4_6()
    {
        player.GetComponent<playerstatus>().ApplyStack(battalemanager.Instance.stackdatas[27], 1);
    }

    public void Focus1Function()
    {
        int usefocus = (int)GetComponent<boss_hpbar>().currentfocus;
        GetComponent<boss_hpbar>().DecreaseFocus((int)GetComponent<boss_hpbar>().currentfocus);
        int probability = usefocus * 5;

        if (Random.Range(0, 100) >= probability)
        {
            return;
        }

        Effect1();

        if (used[0] && used[1] && used[2] && used[3] && used[4])
        {
            Passive4_6();
            return;
        }

        List<int> available = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            if (!used[i])
                available.Add(i);
        }

        int selected = available[Random.Range(0, available.Count)];

        used[selected] = true;

        switch (selected)
        {
            case 0:
                passive4_1 = true;
                break;

            case 1:
                passive4_2 = true;
                break;

            case 2:
                passive4_3 = true;
                break;

            case 3:
                Passive4_4();
                break;

            case 4:
                Passive4_5();
                break;
        }
    }

    
    public void AttackFunction(GameObject enemy, string index, float damage)
    {
        if (enemy == gameObject)
        {
            if (index == "attack1")
            {
                battalemanager.Instance.attackcore.GetComponent<attackcore>().DecreaseFocus(1);
                GetComponent<boss_hpbar>().HealFocus(1);
            }
            else if (index == "attack3")
            {
                if (whileblacksun)
                {
                    player.GetComponent<playerstatus>().ApplyStack(battalemanager.Instance.stackdatas[33], 1);
                }
                
            }
            else if (index == "attack4")
            {
                playerstatus.StackInstance instance = player.GetComponent<playerstatus>().activeStacks.Find(s => s.stackData.effectName == "참격 취약 I");
                if (instance == null)
                {
                    player.GetComponent<playerstatus>().ApplyStack(battalemanager.Instance.stackdatas[30], 1);
                }
                
                if (instance != null)
                {
                    if (instance.currentStack >= 1)
                    {
                        player.GetComponent<playerstatus>().ApplyStack(battalemanager.Instance.stackdatas[2], 1);
                    }
                }
                
            }
            else if (index == "attack5")
            {
                playerstatus.StackInstance instance = player.GetComponent<playerstatus>().activeStacks.Find(s => s.stackData.effectName == "중독");
                if (instance != null)
                {
                    player.GetComponent<playerstatus>().ApplyStack(battalemanager.Instance.stackdatas[2], instance.currentStack);
                }
                playerstatus.StackInstance instance2 = player.GetComponent<playerstatus>().activeStacks.Find(s => s.stackData.effectName == "출혈");
                if (instance2 != null)
                {
                    player.GetComponent<playerstatus>().ApplyStack(battalemanager.Instance.stackdatas[2], instance2.currentStack);
                }
            }
            else if (index == "attack6")
            {
                playerstatus.StackInstance instance = player.GetComponent<playerstatus>().activeStacks.Find(s => s.stackData.effectName == "참격 피해량 증가 II");
                if (instance != null)
                {
                    if (instance.currentStack < 3)
                    {
                        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[27], 1);
                    }
                }
                GetComponent<boss_hpbar>().HealFocus(1);
                player.GetComponent<playerstatus>().AdditionalBalanceDamage(gameObject, damage * 0.1f);


            }
        }
    }

    public void ReturnToCenter()
    {
        transform.DOMoveX(25, 1f).SetEase(Ease.InOutQuad);
    }

    public void DarkSunMade()
    {
        StartCoroutine(CamToSun());
    }

    IEnumerator CamToSun()
    {
        GameObject cursun = Instantiate(darksuneffect, effectpos.transform.position, Quaternion.identity);
        cursun.transform.DOScale(10, 0.7f).SetEase(Ease.OutQuart);

        yield return new WaitForSeconds(0.4f);
        darksun.SetActive(true);
        whileblacksun = true;

        yield return new WaitForSeconds(0.5f);
        cammanager.GetComponent<CameraManager>().counselcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 1;
        cammanager.GetComponent<CameraManager>().LookCounsel(darksun);
        Destroy(cursun);

        yield return new WaitForSeconds(0.5f);
        GetComponent<cutscenemanager>().CameraZoomOut15utFree(2f);

        yield return new WaitForSeconds(4f);

        GetComponent<cutscenemanager>().CameraReturn();
        cammanager.GetComponent<CameraManager>().LookPlayer();
        cammanager.GetComponent<CameraManager>().counselcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 10;
        GetComponent<draphen_boss>().NextAttack();
    }
}
