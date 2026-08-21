using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static DefenseSkill;
using static UnityEngine.EventSystems.EventTrigger;

public class boss_hpbar : MonoBehaviour
{
    public enum EnemyAttackType
    {
        slash, penetrate, blow, fix
    };

    public enum EnemyDefenseType
    {
        defense, evasion, counter, immobility
    };

    public enum EnemyCounterType
    {
        slash, penetrate, blow, fix
    };

    public static event Action OnHitCalled;
    public static event Action OnPenetrationHitCalled;
    public static event Action<GameObject> Die;
    public static event Action<GameObject> OnCycleEnd;
    public static event Action<GameObject> OnCircumStart;
    public static event Action OnCollusion;
    public static event Action OnCollusionSolve;
    public static event Action<GameObject, int> OnCycleDesicion;
    public static event Action<GameObject> Onevasion;
    public static event Action<GameObject> OnevasionFail;
    public static event Action<GameObject> OnImmotalityWorked;
    public static event Action<GameObject> OnDefenseSkillArrey;

    public GameObject worldlight;
    public GameObject playerlight;
    public GameObject gammanager;
    public GameObject cammanager;

    public GameObject attackcore_;

    public GameObject damagepos;

    public int mincycle;
    public int maxcycle;
    public int decisionedcycle;
    public int currentcycle;

    public int currentphase;
    public int currentattacknumber;

    public float maxfocus;
    public float currentfocus;

    public bool defense;
    public bool evasion;
    public bool counter;
    public bool immobility;
    public EnemyDefenses currentdefense;
    public GameObject defenseskillui;
    public GameObject defenseskillView;
    public float defenseskilluiposX;
    public float defenseskilluiposY;

    [System.Serializable]
    public class EnemyDefenses
    {
        public string defensename;
        public EnemyDefenseType enemyDefenseType;
        public EnemyCounterType enemyCounterType;
        public string animationtrigger;
        public float calculation;
        public GameObject skillprephep;
    }

    [System.Serializable]
    public class EnemyFocusSkill
    {
        public string focuskillname;
        public string animationtrigger;
        public int focusspend;
    }


    [System.Serializable]
    public class EnemySkills
    {
        public string skillname;
        public List<EnemyAttackType> enemyAttackTypes;
        public string animationtrigger;

        public bool only;
        public bool notinclude;
    }

    [System.Serializable]
    public class EnemySkillPhase
    {
        public List<EnemySkills> skills;
        public List<EnemyDefenses> defense;
    }

    public List<EnemySkillPhase> phase;
    public List<EnemyFocusSkill> focusskills;

    public List<EnemySkills> currentenemySkills;
    public List<EnemyDefenses> currentenemyDefenses;


    public GameObject balancebar;
    public GameObject healthbar;
    public GameObject barrierbar;
    public GameObject focusbar;
    public GameObject focusbar_background;
    public GameObject focusbar_fillarea;
    public GameObject stackbar;
    public GameObject canvas;
    public Slider balancebarint;
    public Slider healthbarint;
    public Slider focusbarint;
    public GameObject chatbox;



    public string hitsound;

    //프리팹
    public GameObject damagetext;
    public GameObject bleedtext;
    public GameObject balanceeffect;
    public GameObject evasiontext;
    public GameObject defenseskilluiprephep;
    //

    public float collapsefloat;

    //코어 변수
    public int attackpower;

    public float maxhealth;
    public float currenthealth;
    public float maxbalance;
    public float currentbalance;
    public float maxbalanceminus;
    public float currentbarrier;
    public float maxbarrier;

    public float slashtolerance;
    public float penetratetolerance;
    public float blowtolerance;
    public float slashtoleranceCore;
    public float penetratetoleranceCore;
    public float blowtoleranceCore;

    public float damageplus; //가하는 피해량 증가
    public float damageplusCore = 1;

    public float bleeddamageincrease;
    public float bleeddamageincreaseCore = 1;


    //

    //페시브(전투 내내) 변수 or 기타
    public float passive_balancedamagedecrease; //받는 균형피해 감소
    public float passive_balancedamagedecreaseCore = 0;

    public float passive_balancedamageincrease; //받는 균형피해 증가
    public float passive_balancedamageincreaseCore = 0;

    public float passive_damageplus; //가하는 피해량 증가
    public float passive_damageplusCore = 0;

    public float passive_calculationPlus;
    public float passive_calculationPlusCore = 0;

    public float passive_recieveDamageUp; //받는 피해량 증가
    public float passive_recieveDamageUpCore = 0;
    //

    //효과(스텍) 용
    public float effect_slashcalculationIncrease; //참격 공격 계수 증가
    public float effect_slashcalculationIncreaseCore = 0;

    public float effect_slashdamageincrease; //참격 공격 피해량 증가
    public float effect_slashdamageincreaseCore = 0;
    //



    public bool iscollapse;
    public bool canhit = true;
    public bool killcut;

    public bool candie = true;
    public bool died;
    //라포용
    
    //

    public List<StackInstance> nextcycleStacks = new List<StackInstance>();
    public List<StackInstance> activeStacks = new List<StackInstance>();

    public static event Action<Stack, int> OnStackApplied;
    public static event Action<Stack, int> OnStackRemoved;
    

    public class StackInstance
    {
        public Stack stackData;
        public int currentStack;

        public StackInstance(Stack data, int initialStack)
        {
            stackData = data;
            currentStack = Mathf.Clamp(initialStack, 1, data.maxStacks);
        }

        public void AddStack(int amount)
        {
            if (stackData.stackable)
            {
                currentStack = Mathf.Clamp(currentStack + amount, 0, stackData.maxStacks);
            }
        }

        public void RemoveStack(int amount)
        {
            currentStack = Mathf.Clamp(currentStack - amount, 0, stackData.maxStacks);
        }
    }

    public void ApplyStack(Stack newStack, int amount)
    {
        Debug.Log("ApplyStack");
        StackInstance existing = activeStacks.Find(s => s.stackData == newStack);

        if (existing != null)
        {
            existing.AddStack(amount);

        }
        else
        {
            int initialStack = Mathf.Clamp(amount, 1, newStack.maxStacks);
            StackInstance instance = new StackInstance(newStack, initialStack);
            activeStacks.Add(instance);
        }

        //Debug.Log($"Applied stack: {newStack.effectName} (+{amount})");

        OnStackApplied?.Invoke(newStack, amount);

        canvas.GetComponent<boss_stackUIManager>().RefreshUI(this);

        GameObject targetObj = uimanager.Instance.enemystatessets.Find(x => x.GetComponent<enemystateui>().enemy == gameObject);

        targetObj.GetComponent<enemystateui>().stackbar.GetComponent<boss_stackUIManager>().RefreshUI(this);


        //GetComponent<Passivefunction>().WhenAddStack();
        WhenStackAdd();
    }

    public void RemoveStack(Stack targetStack, int amount)
    {
        StackInstance existing = activeStacks.Find(s => s.stackData == targetStack);

        if (existing != null)
        {
            existing.RemoveStack(amount);
            //Debug.Log($"Removed stack: {targetStack.effectName} (-{amount})");

            OnStackRemoved?.Invoke(targetStack, amount);

            // 스택이 0이면 목록에서 제거
            if (existing.currentStack <= 0 && existing.stackData.disappear_whenzero)
            {
                activeStacks.Remove(existing);
                Debug.Log($"{targetStack.effectName} stack fully removed.");
            }
        }
        else
        {
            Debug.LogWarning($"Tried to remove stack that doesn't exist: {targetStack.effectName}");
        }
        canvas.GetComponent<boss_stackUIManager>().RefreshUI(this);

        GameObject targetObj = uimanager.Instance.enemystatessets.Find(x => x.GetComponent<enemystateui>().enemy == gameObject);

        targetObj.GetComponent<enemystateui>().stackbar.GetComponent<boss_stackUIManager>().RefreshUI(this);

        //GetComponent<Passivefunction>().WhenRemoveStack();
        WhenStackAdd();
    }

    public void ApplyStackOnNextCycle(Stack newStack, int amount)
    {
        StackInstance existing = nextcycleStacks.Find(s => s.stackData == newStack);

        if (existing != null)
        {
            existing.AddStack(amount);

        }
        else
        {
            int initialStack = Mathf.Clamp(amount, 1, newStack.maxStacks);
            StackInstance instance = new StackInstance(newStack, initialStack);
            nextcycleStacks.Add(instance);
        }

        Debug.Log($"Applied next stack: {newStack.effectName} (+{amount})");

        canvas.GetComponent<boss_stackUIManager>().RefreshUI(this);

        GameObject targetObj = uimanager.Instance.enemystatessets.Find(x => x.GetComponent<enemystateui>().enemy == gameObject);

        targetObj.GetComponent<enemystateui>().stackbar.GetComponent<boss_stackUIManager>().RefreshUI(this);
    }

    public void PrintStacks()
    {
        foreach (var s in activeStacks)
        {
            Debug.Log($"{s.stackData.effectName}: {s.currentStack}/{s.stackData.maxStacks}");
        }
    }

    public void PassiveFloatReset()
    {
        slashtolerance = slashtoleranceCore;
        penetratetolerance = penetratetoleranceCore;
        blowtolerance = blowtoleranceCore;

        damageplus = damageplusCore;

        bleeddamageincrease = bleeddamageincreaseCore;

    }

    public void Awake()
    {
        gammanager = battalemanager.Instance.gameObject;
        
    }

    private void Start()
    {
        currenthealth = maxhealth;
        balancebarint.maxValue = maxbalance;
        healthbarint.maxValue = maxhealth * 2;
        currentbalance = 0;
        attackcore_ = attackcore.attackcoreInstance.gameObject;
        
    }

   
    public void WhenStackAdd()
    {
        bleeddamageincrease = bleeddamageincreaseCore;
        damageplus = damageplusCore;
        effect_slashcalculationIncrease = effect_slashcalculationIncreaseCore;
        effect_slashdamageincrease = effect_slashdamageincreaseCore;

        if (activeStacks.Count > 0)
        {
            foreach (StackInstance stack in activeStacks)
            {
                if (stack.stackData.effectName == "치명적 열상 I")
                {
                    bleeddamageincrease += 0.1f;
                    if (activeStacks.Find(s => s.stackData.effectName == "출혈") != null)
                    {
                        damageplus += 0.1f;
                    }
                }
                if (stack.stackData.effectName == "치명적 열상 II")
                {
                    bleeddamageincrease += 0.2f;
                    if (activeStacks.Find(s => s.stackData.effectName == "출혈") != null)
                    {
                        damageplus += 0.2f;
                    }
                }
                if (stack.stackData.effectName == "고조")
                {
                    if (stack.currentStack == 100)
                    {
                        BalanceHeal(100f);
                        RemoveStack(stack.stackData, 100);
                        RemoveStack(battalemanager.Instance.stackdatas[24], 3);
                        if (TryGetComponent<communicator_passive>(out communicator_passive cp))
                        {
                            cp.Focus1();
                        }
                    }
                }
                if (stack.stackData.effectName == "첨예")
                {
                    effect_slashcalculationIncrease += (0.5f * stack.currentStack);
                }
                if (stack.stackData.effectName == "참격 피해량 증가 II")
                {
                    effect_slashdamageincrease += 0.2f;
                }
            }
        }
    }

    public void CycleStart()
    {
        foreach (StackInstance stack in nextcycleStacks)
        {
            ApplyStack(stack.stackData, stack.currentStack);
        }
        nextcycleStacks.Clear();
        canvas.GetComponent<boss_stackUIManager>().RefreshUI(this);
    }

    public void CycleEnd()
    {
        Debug.Log("dkjjAE");

        OnCycleEnd?.Invoke(gameObject);

        if (activeStacks.Count > 0)
        {
            for (int i = activeStacks.Count - 1; i >= 0; i--)
            {
                StackInstance stack = activeStacks[i];

                if (stack.stackData.effectName == "출혈")
                {
                    float bleeddam = stack.currentStack * (bleeddamageincrease + playerstatus.instance.r_enemybleeddamageincrease);
                    Damage((int)bleeddam, false);
                    GameObject curbleedtext = Instantiate(bleedtext,damagepos.transform);
                    curbleedtext.transform.localPosition = Vector3.zero;
                    curbleedtext.transform.GetChild(0).GetComponent<TMP_Text>().text = bleeddam.ToString("F0");
                    RemoveStack(stack.stackData, (int)Math.Truncate(stack.currentStack * (2f / 3f)));
                    if (stack.currentStack == 1)
                    {
                        RemoveStack(stack.stackData, 1);
                    }
                }

                if (stack.stackData.effectName == "치명적 열상 I")
                {
                    RemoveStack(stack.stackData, 1);
                }

                if (stack.stackData.effectName == "치명적 열상 II")
                {
                    RemoveStack(stack.stackData, 1);
                }

                if (stack.stackData.effectName == "참격 피해량 증가 II")
                {
                    RemoveStack(stack.stackData, 1);
                }
            }
        }
        
    }

    public void HitSound()
    {
        if (hitsound != null)
        {
            battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay(hitsound);
        }
    }

    public void BalanceCheck()
    {
        balancebarint.value = currentbalance;

        GameObject targetObj = uimanager.Instance.enemystatessets.Find(x => x.GetComponent<enemystateui>().enemy == gameObject);

        targetObj.GetComponent<enemystateui>().balancebar.GetComponent<Slider>().maxValue = maxbalance;
        targetObj.GetComponent<enemystateui>().balancebar.GetComponent<Slider>().value = currentbalance;
    }

    public void HeathCheck()
    {
        healthbarint.value = maxhealth - currenthealth;

        maxbalanceminus = currenthealth / (2 * maxhealth);

        GameObject targetObj = uimanager.Instance.enemystatessets.Find(x => x.GetComponent<enemystateui>().enemy == gameObject);

        targetObj.GetComponent<enemystateui>().healthbar.GetComponent<Slider>().maxValue = maxhealth * 2;
        targetObj.GetComponent<enemystateui>().healthbar.GetComponent<Slider>().value = maxhealth - currenthealth;
    }

    public void Heal(int amount)
    {
        currenthealth = Mathf.Max(currenthealth + amount, maxhealth);
        HeathCheck();
    }

    public void BalanceDamage(float balance)
    {
        currentbalance += (balance += (balance * (playerstatus.instance.balancedamageplus + playerstatus.instance.r_compulsion_balancedamageincrease + passive_balancedamageincrease)));
        currentbalance = currentbalance *(1 - passive_balancedamagedecrease);
        BalanceCheck();
        if (currentbalance >= maxbalance - maxbalanceminus)
        {
            BalanceCollapse();
        }
    }

    public void BalanceHeal(float balance)
    {
        currentbalance += balance;
        BalanceCheck();
    }

    public void BalanceCollapse()
    {
        OnCollusion?.Invoke();

        GameObject obj = Instantiate(balanceeffect, balancebar.transform);

        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        rt.anchoredPosition = Vector2.zero;


        GetComponent<Animator>().SetBool("idle", false);
        GetComponent<Animator>().SetTrigger("collapse");

        currentbalance = 0;
        if (attackcore_.GetComponent<attackcore>().standbyskills.Count > 0)
        {
            Debug.Log("대기스킬이 1개 이상이므로 대기스킬 사용");
            attackcore_.GetComponent<attackcore>().UseStandbySkill();
        }
        else
        {
            StartCoroutine(Collapsetimeout());
        }



        iscollapse = true;
        // 균형붕괴
    }

    IEnumerator Collapsetimeout()
    {
        yield return new WaitForSeconds(collapsefloat);
        CollusionSolve();
        GetComponent<Animator>().SetBool("idle", true);
        iscollapse = false;
        attackcore_.GetComponent<attackcore>().NostandByskill();

    }

    public void CollusionSolve()
    {
        iscollapse = false; 
        OnCollusionSolve?.Invoke();
        CycleDecision();
    }

    public void Redemisson()
    {
        DOTween.Kill("enemyflash");
        GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", 0.5f);
        DOTween.To(() => GetComponent<SpriteRenderer>().material.GetFloat("_flashamount"), value => GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", value), 0f, 0.35f).SetEase(Ease.OutQuart).SetUpdate(true).SetId("enemyflash");
    }

    public void Damage(int damage, bool damNotion = true)
    {
        bool balanceDefense = false;

        DefenseUiPosition(battalemanager.Instance.player);

        defense = false;
        evasion = false;
        counter = false;
        immobility = false;

        if (currentenemyDefenses.Count > 0)
        {
            currentdefense = currentenemyDefenses[0];

            if (currentdefense.enemyDefenseType == EnemyDefenseType.defense)
                defense = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.evasion)
                evasion = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.counter)
                counter = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.immobility)
                immobility = true;
        }

        if (canhit)
        {
            float totaldamage = damage;

            if (currentdefense != null)
            {
                if (defense)
                {
                    int damdecrease = (int)(currentdefense.calculation * attackpower);
                    totaldamage = Mathf.Max(1, damage - damdecrease);
                }
                else if (evasion)
                {
                    int evasioncal = (int)(currentdefense.calculation * attackpower);

                    if (damage <= evasioncal)
                    {
                        GameObject currenttext = Instantiate(evasiontext, transform.position, Quaternion.identity);
                        currenttext.transform.localPosition = transform.position;

                        DefenseSkillUiDecrease();

                        Onevasion?.Invoke(gameObject);
                        return;
                    }

                    totaldamage = (int)(damage * 1.2f);
                    OnevasionFail?.Invoke(gameObject);
                }
                else if (counter)
                {
                    if (currentdefense.enemyCounterType == EnemyCounterType.slash)
                        battalemanager.Instance.player.GetComponent<playerhit>().SlashHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.penetrate)
                        battalemanager.Instance.player.GetComponent<playerhit>().PenetrateHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.blow)
                        battalemanager.Instance.player.GetComponent<playerhit>().BlowHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.fix)
                        battalemanager.Instance.player.GetComponent<playerhit>().Hit((int)(currentdefense.calculation * attackpower), gameObject);

                    if (currentdefense.skillprephep != null)
                    {
                        GameObject currentprephep = Instantiate(currentdefense.skillprephep, transform);
                        currentprephep.transform.position = Vector2.zero;
                    }

                }
                else if (immobility)
                {
                    balanceDefense = true;
                }

                DefenseSkillUiDecrease();
            }

            HitSound();
            OnHitCalled?.Invoke();

            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore_.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;

            
            totaldamage *= (damageplus + passive_recieveDamageUp);

            if (currentbarrier > 0)
            {
                totaldamage = Mathf.Max(totaldamage - currentbarrier, 0);
                BarrierCheck();
            }

            currenthealth -= totaldamage;

            HeathCheck();

            if (!iscollapse)
            {
                if (!balanceDefense)
                {
                    BalanceDamage(damage * 0.1f); 
                }
                else if (balanceDefense)
                {
                    OnImmotalityWorked?.Invoke(gameObject);
                }
                
            }
            if (damNotion)
            {
                {
                    GameObject damt = Instantiate(damagetext, damagepos.transform.position, Quaternion.identity);
                    damagetext damtdamagetext = damt.GetComponent<damagetext>();
                    if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
                    {
                        damtdamagetext.wherexpos = 1;
                    }
                    else
                    {
                        damtdamagetext.wherexpos = -1;
                    }
                    damtdamagetext.fix = true;
                    damtdamagetext.damage = (int)totaldamage;
                }
            }
            
            if (currenthealth <= 0)
            {
                Dead();
            }
        }
        
    }

    public void SlashDamage(int damage)
    {
        bool balanceDefense = false;

        DefenseUiPosition(battalemanager.Instance.player);

        defense = false;
        evasion = false;
        counter = false;
        immobility = false;

        if (currentenemyDefenses.Count > 0)
        {
            currentdefense = currentenemyDefenses[0];

            if (currentdefense.enemyDefenseType == EnemyDefenseType.defense)
                defense = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.evasion)
                evasion = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.counter)
                counter = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.immobility)
                immobility = true;
        }

        if (canhit)
        {
            float totaldamage = damage;

            if (currentdefense != null)
            {
                if (defense)
                {
                    int damdecrease = (int)(currentdefense.calculation * attackpower);
                    totaldamage = Mathf.Max(1, damage - damdecrease);
                }
                else if (evasion)
                {
                    int evasioncal = (int)(currentdefense.calculation * attackpower);

                    if (damage <= evasioncal)
                    {
                        GameObject currenttext = Instantiate(evasiontext, transform.position, Quaternion.identity);
                        currenttext.transform.localPosition = transform.position;

                        DefenseSkillUiDecrease();

                        Onevasion?.Invoke(gameObject);
                        return;
                    }

                    totaldamage = (int)(damage * 1.2f);
                    OnevasionFail?.Invoke(gameObject);
                }
                else if (counter)
                {
                    if (currentdefense.enemyCounterType == EnemyCounterType.slash)
                        battalemanager.Instance.player.GetComponent<playerhit>().SlashHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.penetrate)
                        battalemanager.Instance.player.GetComponent<playerhit>().PenetrateHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.blow)
                        battalemanager.Instance.player.GetComponent<playerhit>().BlowHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.fix)
                        battalemanager.Instance.player.GetComponent<playerhit>().Hit((int)(currentdefense.calculation * attackpower), gameObject);

                    if (currentdefense.skillprephep != null)
                    {
                        GameObject currentprephep = Instantiate(currentdefense.skillprephep, transform);
                        currentprephep.transform.position = Vector2.zero;
                    }

                }
                else if (immobility)
                {
                    balanceDefense = true;
                }

                DefenseSkillUiDecrease();
            }



            HitSound();
            OnHitCalled?.Invoke();

            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore_.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;


            totaldamage *= slashtolerance;
            totaldamage *= (damageplus + passive_recieveDamageUp);

            if (currentbarrier > 0)
            {
                totaldamage = Mathf.Max(totaldamage - currentbarrier, 0);
                BarrierCheck();
            }

            currenthealth -= totaldamage;

            HeathCheck();

            if (!iscollapse)
            {
                if (!balanceDefense)
                {
                    BalanceDamage(damage * 0.1f);
                }
                else if (balanceDefense)
                {
                    OnImmotalityWorked?.Invoke(gameObject);
                }
            }
            GameObject damt = Instantiate(damagetext, damagepos.transform.position, Quaternion.identity);
            damagetext damtdamagetext = damt.GetComponent<damagetext>();
            if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
            {
                damtdamagetext.wherexpos = 1;
            }
            else
            {
                damtdamagetext.wherexpos = -1;
            }
            damtdamagetext.slash = true;
            damtdamagetext.damage = (int)totaldamage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }
        
    }

    public void PenetrateDamage(int damage)
    {
        bool balanceDefense = false;

        DefenseUiPosition(battalemanager.Instance.player);

        defense = false;
        evasion = false;
        counter = false;
        immobility = false;

        if (currentenemyDefenses.Count > 0)
        {
            currentdefense = currentenemyDefenses[0];

            if (currentdefense.enemyDefenseType == EnemyDefenseType.defense)
                defense = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.evasion)
                evasion = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.counter)
                counter = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.immobility)
                immobility = true;
        }

        //Debug.Log(damage);
        if (canhit)
        {
            float totaldamage = damage;

            if (currentdefense != null)
            {
                if (defense)
                {
                    int damdecrease = (int)(currentdefense.calculation * attackpower);
                    totaldamage = Mathf.Max(1, damage - damdecrease);
                }
                else if (evasion)
                {
                    int evasioncal = (int)(currentdefense.calculation * attackpower);

                    if (damage <= evasioncal)
                    {
                        GameObject currenttext = Instantiate(evasiontext, transform.position, Quaternion.identity);
                        currenttext.transform.localPosition = transform.position;

                        DefenseSkillUiDecrease();

                        Onevasion?.Invoke(gameObject);
                        return;
                    }

                    totaldamage = (int)(damage * 1.2f);
                    OnevasionFail?.Invoke(gameObject);
                }
                else if (counter)
                {
                    if (currentdefense.enemyCounterType == EnemyCounterType.slash)
                        battalemanager.Instance.player.GetComponent<playerhit>().SlashHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.penetrate)
                        battalemanager.Instance.player.GetComponent<playerhit>().PenetrateHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.blow)
                        battalemanager.Instance.player.GetComponent<playerhit>().BlowHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.fix)
                        battalemanager.Instance.player.GetComponent<playerhit>().Hit((int)(currentdefense.calculation * attackpower), gameObject);

                    if (currentdefense.skillprephep != null)
                    {
                        GameObject currentprephep = Instantiate(currentdefense.skillprephep, transform);
                        currentprephep.transform.position = Vector2.zero;
                    }

                }
                else if (immobility)
                {
                    balanceDefense = true;
                }

                DefenseSkillUiDecrease();
            }

            HitSound();
            OnHitCalled?.Invoke();
            OnPenetrationHitCalled?.Invoke();

            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore_.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;

            totaldamage *= penetratetolerance;
            totaldamage *= (damageplus + passive_recieveDamageUp);

            if (currentbarrier > 0)
            {
                totaldamage = Mathf.Max(totaldamage - currentbarrier, 0);
                BarrierCheck();
            }

            currenthealth -= totaldamage;

            HeathCheck();

            if (!iscollapse)
            {
                if (!balanceDefense)
                {
                    BalanceDamage(damage * 0.1f);
                }
                else if (balanceDefense)
                {
                    OnImmotalityWorked?.Invoke(gameObject);
                }
            }
            GameObject damt = Instantiate(damagetext, damagepos.transform.position, Quaternion.identity);
            damagetext damtdamagetext = damt.GetComponent<damagetext>();
            if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
            {
                damtdamagetext.wherexpos = 1;
            }
            else
            {
                damtdamagetext.wherexpos = -1;
            }
            damtdamagetext.penetarte = true;
            damtdamagetext.damage = (int)totaldamage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }
        
    }

    public void BlowDamage(int damage)
    {
        bool balanceDefense = false;

        DefenseUiPosition(battalemanager.Instance.player);

        defense = false;
        evasion = false;
        counter = false;
        immobility = false;

        if (currentenemyDefenses.Count > 0)
        {
            currentdefense = currentenemyDefenses[0];

            if (currentdefense.enemyDefenseType == EnemyDefenseType.defense)
                defense = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.evasion)
                evasion = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.counter)
                counter = true;

            if (currentdefense.enemyDefenseType == EnemyDefenseType.immobility)
                immobility = true;
        }

        if (canhit)
        {
            float totaldamage = damage;

            if (currentdefense != null)
            {
                if (defense)
                {
                    int damdecrease = (int)(currentdefense.calculation * attackpower);
                    totaldamage = Mathf.Max(1, damage - damdecrease);
                }
                else if (evasion)
                {
                    int evasioncal = (int)(currentdefense.calculation * attackpower);

                    if (damage <= evasioncal)
                    {
                        GameObject currenttext = Instantiate(evasiontext, transform.position, Quaternion.identity);
                        currenttext.transform.localPosition = transform.position;

                        DefenseSkillUiDecrease();

                        Onevasion?.Invoke(gameObject);
                        return;
                    }

                    totaldamage = (int)(damage * 1.2f);
                    OnevasionFail?.Invoke(gameObject);
                }
                else if (counter)
                {
                    if (currentdefense.enemyCounterType == EnemyCounterType.slash)
                        battalemanager.Instance.player.GetComponent<playerhit>().SlashHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.penetrate)
                        battalemanager.Instance.player.GetComponent<playerhit>().PenetrateHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.blow)
                        battalemanager.Instance.player.GetComponent<playerhit>().BlowHit((int)(currentdefense.calculation * attackpower), gameObject);
                    else if (currentdefense.enemyCounterType == EnemyCounterType.fix)
                        battalemanager.Instance.player.GetComponent<playerhit>().Hit((int)(currentdefense.calculation * attackpower), gameObject);

                    if (currentdefense.skillprephep != null)
                    {
                        GameObject currentprephep = Instantiate(currentdefense.skillprephep, transform);
                        currentprephep.transform.position = Vector2.zero;
                    }

                }
                else if (immobility)
                {
                    balanceDefense = true;
                }

                DefenseSkillUiDecrease();
            }

            HitSound();
            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore_.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;

            totaldamage *= blowtolerance;
            totaldamage *= (damageplus + passive_recieveDamageUp);

            if (currentbarrier > 0)
            {
                totaldamage = Mathf.Max(totaldamage - currentbarrier, 0);
                BarrierCheck();
            }

            currenthealth -= totaldamage;

            HeathCheck();

            if (!iscollapse)
            {
                if (!balanceDefense)
                {
                    BalanceDamage(damage * 0.1f);
                }
                else if (balanceDefense)
                {
                    OnImmotalityWorked?.Invoke(gameObject);
                }
            }
            GameObject damt = Instantiate(damagetext, damagepos.transform.position, Quaternion.identity);
            damagetext damtdamagetext = damt.GetComponent<damagetext>();
            if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
            {
                damtdamagetext.wherexpos = 1;
            }
            else
            {
                damtdamagetext.wherexpos = -1;
            }
            damtdamagetext.blow = true;
            damtdamagetext.damage = (int)totaldamage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }
        
    }

    public void barrierAdd(int amount)
    {
        currentbarrier += amount;
        barrierbar.GetComponent<Slider>().value = currentbarrier;
    }

    public void barrierMinus(int amount)
    {
        currentbarrier = Mathf.Max(currentbarrier - amount, 0);
        barrierbar.GetComponent<Slider>().value = currentbarrier;
    }

    public void BarrierCheck()
    {
        barrierbar.GetComponent<Slider>().value = currentbarrier;
    }

    public void Dead()
    {
        StartCoroutine(Dead_co());
    }

    
    public void Update()
    {
        if (killcut)
        {
            Time.timeScale = 0.2f;
        }
    }

    IEnumerator Dead_co()
    {  
        if (candie)
        {
            GetComponent<Animator>().SetTrigger("dying");
            died = true;
        }
        Die?.Invoke(gameObject);
        float worldlightintensity = worldlight.GetComponent<Light2D>().intensity;
        Light2D worldlightLight2D = worldlight.GetComponent<Light2D>();
        worldlightLight2D.color = new Color(1, 0, 0, 1);
        worldlightLight2D.intensity = 15;
        playerlight.GetComponent<Light2D>().color = new Color(0, 0, 0, 1);
        killcut = true;
        cammanager.GetComponent<CameraManager>().KIllcam();
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        killcut = false;
        worldlightLight2D.color = new Color(1, 1, 1, 1);
        worldlightLight2D.intensity = worldlightintensity;
        playerlight.GetComponent<Light2D>().color = new Color(1, 1, 1, 1);

        battalemanager.Instance.currentenemys.Remove(gameObject);
    }

    
    public void FocusUpdate()
    {
        GameObject targetObj = uimanager.Instance.enemystatessets.Find(x => x.GetComponent<enemystateui>().enemy == gameObject);

        targetObj.GetComponent<enemystateui>().focusbar.GetComponent<Slider>().value = currentfocus;
        focusbarint.value = currentfocus;

        StartCoroutine(FocusBarUi());
    }

    public Tween enemyfocusbartween1;
    public Tween enemyfocusbartween2;

    IEnumerator FocusBarUi()
    {
        if (enemyfocusbartween1 != null)
        {
            DOTween.Kill(enemyfocusbartween1);
        }
        if (enemyfocusbartween2 != null)
        {
            DOTween.Kill(enemyfocusbartween2);
        }
        
        focusbar_background.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        focusbar_fillarea.GetComponent<Image>().color = new Color(255, 255, 255, 255);

        yield return new WaitForSeconds(1.5f);

        Color targetColor = new Color(255, 255, 255, 0);
        enemyfocusbartween1 = focusbar_background.GetComponent<Image>().DOColor(targetColor, 1.5f).SetEase(Ease.OutQuart);
        enemyfocusbartween2 = focusbar_fillarea.GetComponent<Image>().DOColor(targetColor, 1.5f).SetEase(Ease.OutQuart);
    }

    public void PhaseUp()
    {
        currentphase++;
        CycleDecision();
    }

    public void CycleDecision()
    {
        OnCircumStart?.Invoke(gameObject);

        currentcycle++;
        OnCycleDesicion?.Invoke(gameObject, currentcycle);

        currentattacknumber = 0;
        decisionedcycle = UnityEngine.Random.Range(mincycle, maxcycle + 1);
        maxfocus = decisionedcycle;
        currentfocus = maxfocus;
        maxbarrier = maxhealth;
        barrierbar.GetComponent<Slider>().maxValue = maxbarrier;

        GameObject targetObj = uimanager.Instance.enemystatessets.Find(x => x.GetComponent<enemystateui>().enemy == gameObject);

        targetObj.GetComponent<enemystateui>().focusbar.GetComponent<Slider>().maxValue = currentfocus;

        focusbarint.maxValue = maxfocus;
        focusbarint.value = currentfocus;

        currentenemySkills.Clear();

        List<EnemySkills> tempList = new List<EnemySkills>(phase[currentphase].skills);

        tempList.RemoveAll(skill => skill.notinclude);

        int lastRandomIndex = -1;

        for (int i = 0; i < decisionedcycle; i++)
        {
            if (tempList.Count == 0)
                break;

            int randomIndex;

            do
            {
                randomIndex = UnityEngine.Random.Range(0, tempList.Count);
            }
            while (tempList.Count > 1 && randomIndex == lastRandomIndex);

            EnemySkills selectedSkill = tempList[randomIndex];

            currentenemySkills.Add(selectedSkill);

            if (selectedSkill.only)
            {
                tempList.RemoveAt(randomIndex);

                lastRandomIndex = -1;
            }
            else
            {
                lastRandomIndex = randomIndex;
            }
        }

        DefenseSkillArrey();
    }

    public void DefenseSkillArrey()
    {
        Debug.Log("dkajb");
        currentenemyDefenses.Clear();

        for (int i = 0; i < decisionedcycle; i++)
        {
            currentenemyDefenses.Add(phase[currentphase].defense[0]);
        }

        OnDefenseSkillArrey?.Invoke(gameObject);
        DefenseSkillUiSet();
    }

    public void DefenseSkillUiSet()
    {
        for (int i = defenseskillui.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(defenseskillui.transform.GetChild(i).gameObject);
        }

        foreach (EnemyDefenses defenseSkill in currentenemyDefenses)
        {
            GameObject currentdefenseui = Instantiate(defenseskilluiprephep, defenseskillui.transform);
            currentdefenseui.GetComponent<TMP_Text>().text = $"[{defenseSkill.defensename}]";
        }

        DefenseSkilluiAlphaToZero(defenseskillui);
    }

    private Tween defenseSkillFadeDelayTween;
    private Tween DefenseSkilluiAlphaToZeroEnemy;

    public void DefenseSkilluiAlpha(GameObject parent)
    {
        DOTween.Kill(DefenseSkilluiAlphaToZeroEnemy);

        if (defenseSkillFadeDelayTween != null && defenseSkillFadeDelayTween.IsActive())
            defenseSkillFadeDelayTween.Kill();

        int childCount = parent.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            TMP_Text tt = parent.transform.GetChild(i).GetComponent<TMP_Text>();
            if (tt == null) continue;

            Color color = tt.color;

            if (i >= 4)
            {
                color.a = 0f;
            }
            else
            {
                color.a = 1f - (i * 0.25f);
            }

            tt.color = color;
        }

        defenseSkillFadeDelayTween = DOVirtual.DelayedCall(1.5f, () =>
        {
            if (parent != null)
                DefenseSkilluiAlphaToZeroTween(parent);
        });
    }

    public void DefenseSkilluiAlphaToZeroTween(GameObject parent)
    {
        int childCount = parent.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            TMP_Text tt = parent.transform.GetChild(i).GetComponent<TMP_Text>();
            if (tt == null) continue;

            DefenseSkilluiAlphaToZeroEnemy = tt.DOFade(0, 1.5f);
        }
    }

    public void DefenseSkilluiAlphaToZero(GameObject parent)
    {
        int childCount = parent.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            TMP_Text tt = parent.transform.GetChild(i).GetComponent<TMP_Text>();
            if (tt == null) continue;

            DefenseSkilluiAlphaToZeroEnemy = tt.DOFade(0, 0f);
        }
    }

    private Tween currentTween;
    private Transform currentRemovingUi;

    public void DefenseSkillUiDecrease()
    {
        if (currentenemyDefenses.Count > 0)
            currentenemyDefenses.RemoveAt(0);
        if (defenseskillui.transform.childCount == 0)
            return;

        if (currentTween != null)
        {
            currentTween.Kill();
            currentTween = null;
        }
        if (currentRemovingUi != null)
        {
            currentRemovingUi.SetSiblingIndex(defenseskillui.transform.childCount - 1);
            Destroy(currentRemovingUi.gameObject);
            currentRemovingUi = null;
        }

        if (defenseskillui.transform.childCount == 0)
            return;

        Transform currentui = defenseskillui.transform.GetChild(0);
        TMP_Text text = currentui.GetComponent<TMP_Text>();
        currentRemovingUi = currentui;

        currentTween = DOTween.Sequence()
        .SetUpdate(true)
        .Join(text.DOFade(0, 0.45f))
        .Join(currentui.DOScaleY(0, 0.5f))
        .Join(currentui.DOLocalMoveX(currentui.localPosition.x - 30f, 0.45f))
        .OnComplete(() =>
        {
            if (currentui != null)
                Destroy(currentui.gameObject);
            currentTween = null;
            currentRemovingUi = null;
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(
             defenseskillui.GetComponent<RectTransform>());
            DefenseSkilluiAlpha(defenseskillui);
        });

        DefenseSkilluiAlpha(defenseskillui);
    }

    public void DefenseUiPosition(GameObject player)
    {
        if (transform.position.x < player.transform.position.x)
        {
            RectTransform defenseskilluiRectTransform = defenseskillView.GetComponent<RectTransform>();
            defenseskilluiRectTransform.localPosition = new Vector3(-defenseskilluiposX, defenseskilluiposY, 0);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            RectTransform defenseskilluiRectTransform = defenseskillView.GetComponent<RectTransform>();
            defenseskilluiRectTransform.localPosition = new Vector3(defenseskilluiposX, defenseskilluiposY, 0);
        }

    }

    public void Attack()
    {
        if (currentattacknumber == 0)
        {
            CycleStart();
        }
        GetComponent<Animator>().SetTrigger(currentenemySkills[currentattacknumber].animationtrigger);
        currentattacknumber++;
        if (currentenemySkills.Count <= currentattacknumber)
        {
            currentattacknumber = 0;
            CycleEnd();
            DefenseSkillArrey();
        }
    }

    public void BattleStart()
    {
        uimanager.Instance.EnemyStatesSet(gameObject);

        GameObject targetObj = uimanager.Instance.enemystatessets.Find(x => x.GetComponent<enemystateui>().enemy == gameObject);

        targetObj.GetComponent<enemystateui>().focusbar.GetComponent<Slider>().maxValue = currentfocus;
        targetObj.GetComponent<enemystateui>().focusbar.GetComponent<Slider>().value = currentfocus;
        targetObj.GetComponent<enemystateui>().balancebar.GetComponent<Slider>().maxValue = maxbalance;
        targetObj.GetComponent<enemystateui>().balancebar.GetComponent<Slider>().value = currentbalance;
        targetObj.GetComponent<enemystateui>().stackbar.GetComponent<boss_stackUIManager>().RefreshUI(this);

        CycleDecision();
    }

    public void UseFocusSkill(int index)
    {
        EnemyFocusSkill currentfocusskill = focusskills[index];
        if (currentfocusskill.focusspend <= currentfocus)
        {
            currentfocus = currentfocus - currentfocusskill.focusspend;
            FocusUpdate();

            GetComponent<Animator>().SetTrigger(currentfocusskill.animationtrigger);
        }
    }

    public void HealFocus(int i)
    {
        currentfocus = currentfocus + i;
        FocusUpdate();
    }

    public void DecreaseFocus(int i)
    {
        currentfocus = currentfocus - i;
        FocusUpdate();
    }
}
