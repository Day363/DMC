using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Passivefunction : MonoBehaviour
{
    public GameObject attackcore;
    public GameObject gamemanager;
    public List<string> activepassivelist = new List<string> { };
    public Dictionary<string, Action> boolFunctions;
    public playerstatus playerStackHandler;

    public Stack penetrationup;
    public Stack penetrationup2;
    public Stack attention_rate;
    public Stack Inference;
    public Stack certain;
    public Stack deny;
    public Stack regret;
    public Stack usedregret;
    public Stack absorbtion;

    public List<GameObject> indexer_rains = new List<GameObject> { };
    public bool Indexer_istransformed = false;
    public float Indexer_minus;
    public GameObject indexer_gun;
    public bool ishaloactive;
    public GameObject trapal_halo;
    public List<GameObject> trapal_certain_texts = new List<GameObject> { };
    public List<GameObject> trapal_deny_texts = new List<GameObject> { };


    public bool disabled_passive1 = false;
    //public bool alttrigger_passive1 = false;
    public bool alttrigger_passive2 = false;
    public bool alttrigger_passive3 = false;
    public bool indexer_passive1 = false;
    public bool indexer_passive2 = false;
    public bool indexer_passive3 = false;
    public bool trapal_passive1 = false;
    public bool trapal_passive2 = false;
    public bool trapal_passive3 = false;
    public bool trapal_passive4 = false;
    public bool trapal_passive5 = false;
    public bool trapal_passive6 = false;
    public bool warfrigment_passive1 = false;
    public bool warfrigment_passive2 = false;

    private void OnEnable()
    {
        playerstatus.OnStackApplied += WhenStackAddCertain;
        playerstatus.OnStackRemoved += WhenStackRemoveCertain;
        boss_hpbar.OnStackApplied += WhenBossApplyStack;
        playerattackdamage.Onhit += Communicator_Scar;
    }

    public void Start()
    {
        gamemanager = battalemanager.Instance.gameObject;
    }

    private void OnDisable()
    {
        playerstatus.OnStackApplied -= WhenStackAddCertain;
        playerstatus.OnStackRemoved -= WhenStackRemoveCertain;
    }

    public void SetBoolsFromList(List<string> activeList)
    {
        //Debug.Log(activeList);
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(bool))
            {
                bool shouldBeActive = activeList.Contains(field.Name);
                field.SetValue(this, shouldBeActive);
            }
        }
    }

    public void FixedUpdate()
    {
        if (indexer_passive3)
        {
            foreach (GameObject currentenemy in battalemanager.Instance.currentenemys)
            {
                if (!currentenemy.GetComponent<boss_hpbar>().iscollapse)
                {
                    boss_hpbar.StackInstance enemyStackInstance = currentenemy.GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "주시율");
                    if (enemyStackInstance != null)
                    {
                        float distance = Mathf.Abs(currentenemy.transform.position.x - transform.position.x);
                        distance = Mathf.Clamp(distance, 0, 30);
                        float t = Mathf.InverseLerp(30, 0, distance);
                        int value = Mathf.RoundToInt(Mathf.Lerp(1, 99, t));
                        enemyStackInstance.currentStack = value;
                        currentenemy.GetComponent<boss_hpbar>().canvas.GetComponent<boss_stackUIManager>().RefreshUI(currentenemy.GetComponent<boss_hpbar>());
                    }

                }
            }
            

        }
    }

    public void Communicator_Scar(GameObject enemy)
    {
        playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "전흔-전달자");
        if (instance.currentStack >= 5)
        {
            GetComponent<playerstatus>().RemoveStack(instance.stackData, 5);
            enemy.GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[23], 5);
        }
    }

    public void WhenCircumStart()
    {
        Indexer_minus = 0;

        if (disabled_passive1)
        {
            GetComponent<playerstatus>().bleeddamagedecrease += 0.5f;
        }

        if (alttrigger_passive3)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "후회");
            playerstatus.StackInstance instance2 = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "잔흔");
            if (instance == null)
            {
                GetComponent<playerstatus>().ApplyStack(regret, 11);
            }
            else
            {
                instance.currentStack = 11;
                GetComponent<playerstatus>().canvus.GetComponent<stackUimanager>().RefreshUI();
            }

            if (instance2 == null)
            {
                
            }
            else if (instance2 != null)
            {
                GetComponent<playerstatus>().RemoveStack(instance2.stackData, instance2.currentStack);
            }

            GetComponent<playerstatus>().ApplyStack(penetrationup2, 11);
        }

        if (indexer_passive3)
        {
            foreach (GameObject currentenemy in battalemanager.Instance.currentenemys)
            {
                currentenemy.GetComponent<boss_hpbar>().ApplyStack(attention_rate, 1);
            }
            
        }

        if (trapal_passive3)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
            if (instance == null)
            {
                GetComponent<playerstatus>().ApplyStack(Inference, 12);
            }

        }
    }

    public void WhenCycleStart()
    {
        if (disabled_passive1)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "출혈");
            if (instance != null)
            {
                if (instance.currentStack >= 10)
                {
                    GetComponent<playerstatus>().healplus += 0.2f;
                    GetComponent<playerstatus>().slash_tolerance += 0.1f;
                    GetComponent<playerstatus>().penetration_tolerance += 0.1f;
                    GetComponent<playerstatus>().blow_tolerance += 0.1f;
                }
            }
            
            
        }

        if (alttrigger_passive2)
        {
            GetComponent<playerstatus>().selfharmdamagepercent -= 0.3f;
        }

        if (indexer_passive1)
        {
            foreach (GameObject currentenemy in battalemanager.Instance.currentenemys)
            {
                boss_hpbar.StackInstance enemyStackInstance = currentenemy.GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "주시율");
                if (enemyStackInstance != null)
                {
                    if (enemyStackInstance.currentStack >= 50)
                    {
                        int index = UnityEngine.Random.Range(0, 4);
                        Transform bm_enemy = currentenemy.transform;
                        Vector3 pos = new Vector3(bm_enemy.position.x, 35.47f, 0);
                        GameObject currain = Instantiate(indexer_rains[index], pos, Quaternion.identity);
                        currain.GetComponent<playerattackdamage>().player = gameObject;
                    }
                }
            }
            
            
        }

        if (trapal_passive2)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
            if (instance != null)
            {
                if (instance.currentStack > 12)
                {
                    GetComponent<playerstatus>().RemoveStack(Inference, 1);
                }
                if (instance.currentStack < 12)
                {
                    GetComponent<playerstatus>().ApplyStack(Inference, 1);
                }
            }
            
        }

        if (trapal_passive6)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
            if (instance != null)
            {
                GetComponent<playerstatus>().ApplyStack(absorbtion, (int)(GetComponent<playerstatus>().maxbalance * (24f - instance.currentStack)));
            }
        }

        if (warfrigment_passive1)
        {
            if (attackcore.GetComponent<attackcore>().cycle <= 5)
            {
                GetComponent<playerstatus>().damageincrease_c += 0.15f;
            }
        }
    }

    public int trapal_hit = 0;

    public void HitEnemy()
    {
        if (trapal_passive3)
        {
            trapal_hit++;
            if (trapal_hit >= 3)
            {
                GetComponent<playerstatus>().ApplyStack(Inference, 1);
                trapal_hit = 0;
            }
        }
    }

    public int trapal_player_hit = 0;

    public void PlayerHit()
    {
        

        if (trapal_passive3)
        {
            trapal_player_hit++;
            if (trapal_player_hit >= 3)
            {
                GetComponent<playerstatus>().RemoveStack(Inference, 1);
                trapal_player_hit = 0;
            }
        }
    }

    public void DefenseSuccess()
    {
        if (trapal_passive3)
        {
            trapal_player_hit++;
            if (trapal_player_hit >= 3)
            {
                GetComponent<playerstatus>().RemoveStack(Inference, 1);
                trapal_player_hit = 0;
            }
        }
    }

    public void WhenAddStack()
    {

        if (trapal_passive1)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "확신");
            if (instance != null && ishaloactive == false)
            {
                ishaloactive = true;
                trapal_halo.SetActive(true);
            }
        }

        if (trapal_passive3)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
            playerstatus.StackInstance instance1 = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "확신");
            if (instance.currentStack >= 24 && instance1 == null)
            {
                GetComponent<playerstatus>().ApplyStack(certain, 1);
            }
        }

        if (trapal_passive4)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "순환 연산자-결단");
            playerstatus.StackInstance instance1 = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "순환 연산자-수렴");
            if (instance != null && instance1 != null)
            {
                //대기 스킬에 이중결단 추가
            }
            
        }
    }

    public void WhenRemoveStack()
    {
        if (trapal_passive1)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "확신");
            if (instance == null && ishaloactive == true)
            {
                ishaloactive = false;
                trapal_halo.SetActive(false);
            }
        }

        if (trapal_passive3)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
            playerstatus.StackInstance instance1 = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "부정");
            if (instance.currentStack <= 0 && instance1 == null)
            {
                GetComponent<playerstatus>().ApplyStack(deny, 1);
            }
        }
    }

    public int trapal_haloint;

    public void WhenStackAddCertain(Stack stack, int stackint)
    {
        if (trapal_passive1)
        {
            if (ishaloactive)
            {
                if (stack.effectName == "추론")
                {
                    for (int i = 0; i < stackint; i++)
                    {
                        if (trapal_haloint < 12)
                        {
                            trapal_certain_texts[trapal_haloint].SetActive(true);
                            trapal_haloint++;
                        }
                        
                    }
                }
            }
            
        }
    }

    public int trapal_haloint2;

    public void WhenStackRemoveCertain(Stack stack, int stackint)
    {
        if (trapal_passive1)
        {
            if (ishaloactive)
            {
                if (stack.effectName == "추론")
                {
                    for (int i = 0; i < stackint; i++)
                    {
                        if (trapal_haloint2 < 12)
                        {
                            trapal_certain_texts[trapal_haloint2].SetActive(true);
                            trapal_haloint2++;
                        }
                            
                    }
                }
            }
            
        }
        if (alttrigger_passive3)
        {
            if (stack.effectName == "후회")
            {
                GetComponent<playerstatus>().ApplyStack(usedregret, stackint);
            }
        }
    }

    public void WhenBossApplyStack(Stack stack, int stackint)
    {
        if (disabled_passive1)
        {
            if (stack.effectName == "출혈")
            {
                GetComponent<playerstatus>().ApplyStack(stack, stackint);
            }
        }
    }

    public void Indexer_Call()
    {
        if (indexer_passive1)
        {
            foreach (GameObject currentenemy in battalemanager.Instance.currentenemys)
            {
                int index = UnityEngine.Random.Range(0, 4);
                Transform bm_enemy = currentenemy.transform;
                Vector3 pos = new Vector3(bm_enemy.position.x, 35.47f, 0);
                GameObject currain = Instantiate(indexer_rains[index], pos, Quaternion.identity);
                currain.GetComponent<playerattackdamage>().player = gameObject;
            }
            
        }
    }

    public void Indexer_Transform()
    {
        Indexer_istransformed = true;
    }

    public void Indexer_gun_call(GameObject gun)
    {
        
        if (indexer_passive2)
        {
            
            
            if (!Indexer_istransformed && attackcore.GetComponent<attackcore>().weaponsmagazine.Find(x => x.Weapon == gun.GetComponent<player_gunprefap>().weapon).Remainmagazine > 0)
            {
                Debug.Log("sdv");
                Indexer_minus += 0.4f;
                gun.GetComponent<player_gunprefap>().damagenmum -= Indexer_minus;
            }
            else if (!Indexer_istransformed && attackcore.GetComponent<attackcore>().weaponsmagazine.Find(x => x.Weapon == gun.GetComponent<player_gunprefap>().weapon).Remainmagazine <= 0)
            {
                Indexer_minus += 0.8f;
                gun.GetComponent<player_gunprefap>().damagenmum -= Indexer_minus;
            }
            else if (Indexer_istransformed)
            {
                Indexer_istransformed = false;
            }
        }
    }
}
