using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class playerstatus : MonoBehaviour
{
    public static playerstatus instance;

    public static Action OnHit;

    public enum GroundType
    {
        snow, plate, metal, soil, grass, gore
    };

    public GroundType groundtype;

    [SerializeField]
    public Transform stackbar;
    public Slider balancebar;
    public Slider focusbar;
    public TMP_Text hptext;
    public Slider balancebarint;
    public GameObject canvus;
    public GameObject chat;

    public Coroutine typingCoroutine;

    public GameObject cronometer;

    public GameObject emotionbar;
    public GameObject reasonbar;
    public GameObject recognitionbar;

    public GameObject cutsceneuidisappear;

    public GameObject numberimage;
    public GameObject numbercount;

    public standbyskill currentstandbyskill;
    public int skillcellindex = 0;

    //프리팹
    public GameObject bleedtext;
    //

    [Range(-3, 3)]
    public float emotionrate;
    [Range(-3, 3)]
    public float reasonrate;
    [Range(-3, 3)]
    public float recognitionrate;

    public Stack bleed;
    public Stack penetrationup1;
    public Stack absorbtion;
    //주기 시작

    public float damageincrease_c = 0;
    public float damageincreaseCore_c = 0;

    //
    //실시간 체크(스텍의 효과가 순환 시작이나 끝 같은 특정한 트리거가 없으며 업데이트 할떄마다 이 수치를 조정해야함)
    //
    //효과용 변수

    public float backdelay = 0.4f;
    public float backdelayCore = 0.4f;

    public float slash_tolerance = 1;
    public float penetration_tolerance = 1;
    public float blow_tolerance = 1;
    public float slash_toleranceCore = 1;
    public float penetration_toleranceCore = 1;
    public float blow_toleranceCore = 1;

    public float attackdamageplus;
    public float balancedamageplus;
    public float damagedecreaserealtime;

    public float damagedecrease;
    public float damagedecreaseCore = 1;

    public float penetratedamageup;
    public float penetratedamageupCore = 0;

    public float focus;
    public float maxbalance;
    public float currentbalance;
    public int maxlifecount;
    public int lifecount;

    public float speed;
    public float speedCore = 14;

    public float attackpower;
    public float attackpowerCore = 10;

    public float bleeddamagedecrease;
    public float bleeddamagedecreaseCore = 1;

    public float healplus;
    public float healplusCore = 1;

    public float selfharmdamagepercent;
    public float selfharmdamagepercentCore = 1;
    //
    //라포용 변수
    public bool have_endofimpulse = false;
    public bool have_endofimpulse_2 = false;
    public bool have_compulsion = false;
    public bool have_penetration = false;
    public bool r_penetration_cycle = true;
    public bool have_forgotten = false;
    public int forgotten_cycle = 0;

    public float r_balancemaxincrease;
    public float r_balancemaxincreaseCore = 1;

    public float r_bleeddamagedecrease;
    public float r_bleeddamagedecreaseCore = 0;

    public float r_healincrease;
    public float r_healincreaseCore = 0;

    public float r_compulsion_balancedamageincrease;
    public float r_compulsion_balancedamageincreaseCore = 0;

    public float r_penetrationdamageincrease;
    public float r_penetrationdamageincreaseCore = 0;

    public float r_bleedApplyadd;
    public float r_bleedApplyaddCore = 0;

    public float r_enemybleeddamageincrease;
    public float r_enemybleeddamageincreaseCore = 0;
    //
    public Coroutine currentparrystop;

    public List<StackInstance> activeStacks = new List<StackInstance>();

    public static event Action<Stack, int> OnStackApplied;
    public static event Action<Stack, int> OnStackRemoved;

    private void Awake()
    {
        battalemanager.Instance.player = gameObject;
        battalemanager.Instance.playerchatbox = chat;

        instance = this;

        playerhit.OnHitCalled += WhenHit;

        //numbercount.GetComponent<TMP_Text>().text = battalemanager.Instance.number.ToString();
        //numberimage.GetComponent<Image>().sprite = battalemanager.Instance.numberimage;

        
    }

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

        canvus.GetComponent<stackUimanager>().RefreshUI();
        uimanager.Instance.playerstack.GetComponent<stackUimanager>().RefreshUI();

        GetComponent<Passivefunction>().WhenAddStack();
        WhenStackChange();
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
                //Debug.Log($"{targetStack.effectName} stack fully removed.");
            }
        }
        else
        {
            Debug.LogWarning($"Tried to remove stack that doesn't exist: {targetStack.effectName}");
        }

        canvus.GetComponent<stackUimanager>().RefreshUI();
        uimanager.Instance.playerstack.GetComponent<stackUimanager>().RefreshUI();

        GetComponent<Passivefunction>().WhenRemoveStack();
        WhenStackChange();
    }

    public void RapportAdd()
    {
        have_endofimpulse = false;
        have_endofimpulse_2 = false;
        have_compulsion = false;
        have_penetration = false;
        have_forgotten = false;
        forgotten_cycle = 0;
        r_balancemaxincrease = r_balancemaxincreaseCore;
        r_bleeddamagedecrease = r_bleeddamagedecreaseCore;
        r_healincrease = r_healincreaseCore;
        r_compulsion_balancedamageincrease = r_compulsion_balancedamageincreaseCore;
        r_penetrationdamageincrease = r_penetrationdamageincreaseCore;
        r_bleedApplyadd = r_bleedApplyaddCore;
        r_enemybleeddamageincrease = r_enemybleeddamageincreaseCore;

        List<Rapport> currentrapportinv = player_inventory.instance.rapportinv;
        if (currentrapportinv.Count > 0)
        {
            foreach (Rapport rapport in currentrapportinv)
            {
                if (rapport.itemName == "미약한 박동")
                {
                    r_balancemaxincrease += 0.1f;
                    have_endofimpulse = true;
                }
                if (rapport.itemName == "미약한 박동의 말로")
                {
                    r_bleeddamagedecrease += 0.2f;
                    r_healincrease += 0.1f;
                    have_endofimpulse_2 = true;
                }
                if (rapport.itemName == "강박")
                {
                    have_compulsion = true;
                }
                if (rapport.itemName == "관통상")
                {
                    r_penetrationdamageincrease += 0.1f;
                    have_penetration = true;
                }
                if (rapport.itemName == "유기성 물감")
                {
                    r_bleedApplyadd += 1;
                    r_enemybleeddamageincrease += 0.15f;
                }
                if (rapport.itemName == "잊혀진 것")
                {
                    have_forgotten = true;
                }
            }
        }
    }

    public void ParrySuccess()
    {
        if (have_compulsion)
        {
            Mathf.Clamp(r_compulsion_balancedamageincrease += 0.05f, 0f, 0.5f);
        }
    }

    public void WhenHit()
    {
        r_compulsion_balancedamageincrease = r_compulsion_balancedamageincreaseCore;
        if (have_forgotten && forgotten_cycle == 0)
        {
            forgotten_cycle = 5;
            ApplyStack(absorbtion, (int)(maxbalance * 0.3f));
        }
    }

    public void PenetrationHit()
    {
        if (have_penetration && r_penetration_cycle)
        {
            ApplyStack(penetrationup1, 1);
            r_penetration_cycle = false;
        }
    }

    public void AddEmotion(float rate)
    {
        emotionrate = Mathf.Clamp(emotionrate + rate, -3, 3);
        EmotionUIUpdate();
    }

    public void AddReason(float rate)
    {
        reasonrate = Mathf.Clamp(reasonrate + rate, -3, 3);
        ResonUIUpdate();
    }

    public void AddRecognition(float rate)
    {
        recognitionrate = Mathf.Clamp(recognitionrate + rate, -3, 3);
        RecognitionUUpdate();
    }

    public void EmotionUIUpdate()
    {
        DOTween.Kill("emotionrate");
        float mappedTarget1 = Mathf.Lerp(-43.5f, 43.5f, (emotionrate + 3f) / 6f);
        emotionbar.GetComponent<RectTransform>().DOAnchorPosY(mappedTarget1, 1f).SetEase(Ease.OutQuad).SetId("emotionrate");


    }

    public void ResonUIUpdate()
    {
        DOTween.Kill("reasonrate");
        float mappedTarget2 = Mathf.Lerp(-43.5f, 43.5f, (reasonrate + 3f) / 6f);
        reasonbar.GetComponent<RectTransform>().DOAnchorPosY(mappedTarget2, 1f).SetEase(Ease.OutQuad).SetId("reasonrate");
    }

    public void RecognitionUUpdate()
    {
        DOTween.Kill("recognitionrate");
        float mappedTarget3 = Mathf.Lerp(-43.5f, 43.5f, (recognitionrate + 3f) / 6f);
        recognitionbar.GetComponent<RectTransform>().DOAnchorPosY(mappedTarget3, 1f).SetEase(Ease.OutQuad).SetId("recognitionrate");
    }

    public void WhenStackChange()
    {
        penetratedamageup = penetratedamageupCore;
        damagedecrease = damagedecreaseCore;
        attackpower = attackpowerCore;
        speed = speedCore;
        attackdamageplus = 0;
        balancedamageplus = 0;
        damagedecreaserealtime = 0;
        battalemanager.Instance.attackcore.GetComponent<attackcore>().candash = true;

        if (activeStacks.Count > 0)
        {
            foreach (StackInstance stack in activeStacks)
            {
                if (stack.stackData.effectName == "관통 피해량 증가 I")
                {
                    penetratedamageup += 0.1f;
                }
                if (stack.stackData.effectName == "관통 피해량 증가 II")
                {
                    penetratedamageup += 0.2f;
                }
                if (stack.stackData.effectName == "후회")
                {
                    damagedecrease = +damagedecrease * (0.01f * stack.currentStack);
                    attackpower = +attackpower * (0.01f * stack.currentStack);
                    speed = -speed * (0.01f * stack.currentStack);
                }
                if (stack.stackData.effectName == "잔흔")
                {
                    damagedecrease = -damagedecrease * (0.01f * stack.currentStack);
                    attackpower = -attackpower * (0.01f * stack.currentStack);
                    speed = +speed * (0.01f * stack.currentStack);
                }
                if (stack.stackData.effectName == "추론")
                {
                    if (GetComponent<Passivefunction>().trapal_passive5)
                    {
                        attackdamageplus += attackdamageplus * (0.02f * stack.currentStack);
                        balancedamageplus += balancedamageplus * (0.02f * stack.currentStack);
                    }
                    if (GetComponent<Passivefunction>().trapal_passive6)
                    {
                        damagedecreaserealtime += damagedecreaserealtime * (0.02f * (24 - stack.currentStack));
                    }
                }
                if (stack.stackData.effectName == "착란")
                {
                    battalemanager.Instance.attackcore.GetComponent<attackcore>().candash = false;
                }
            }
        }
    }

    public void PrintStacks()
    {
        foreach (var s in activeStacks)
        {
            Debug.Log($"{s.stackData.effectName}: {s.currentStack}/{s.stackData.maxStacks}");
        }
    }

    public void CycleStart()
    {
        r_penetration_cycle = true;
        Mathf.Clamp(forgotten_cycle--, 0, 5);
        damageincrease_c = damageincreaseCore_c;

        if (have_endofimpulse)
        {
            ApplyStack(bleed, 5);
        }
        if (have_endofimpulse_2)
        {
            ApplyStack(bleed, 3);
        }

        if (activeStacks.Count > 0)
        {
            foreach (StackInstance stack in activeStacks)
            {  
                
                
            }
        }
    }

    public void CycleEnd()
    {
        Debug.Log("qwrf");
        if (activeStacks.Count > 0)
        {
            for (int i = activeStacks.Count - 1; i >= 0; i--)
            {
                StackInstance stack = activeStacks[i];

                if (stack.stackData.effectName == "출혈")
                {
                    Debug.Log("adqf");
                    float bleeddam = stack.currentStack * (bleeddamagedecrease - r_bleeddamagedecrease);
                    BalanceDamage(bleeddam);
                    GameObject curbleedtext = Instantiate(bleedtext, transform.position, Quaternion.identity);
                    curbleedtext.transform.GetChild(0).GetComponent<TMP_Text>().text = bleeddam.ToString("F0");
                    RemoveStack(stack.stackData, (int)Math.Truncate(stack.currentStack * (2f / 3f)));
                    

                    if (stack.currentStack == 1)
                    {
                        RemoveStack(stack.stackData, 1);
                    }
                }

                if (stack.stackData.effectName == "관통 피해량 증가 I")
                {
                    RemoveStack(stack.stackData, 1);
                }

                if (stack.stackData.effectName == "관통 피해량 증가 II")
                {
                    RemoveStack(stack.stackData, 1);
                }
            }
        }
    }

    public int WhenHitAbsorbtion(int damage)
    {
        if (activeStacks.Count > 0)
        {
            for (int i = activeStacks.Count - 1; i >= 0; i--)
            {
                StackInstance stack = activeStacks[i];
                if (stack.stackData.name == "흡수")
                {
                    if (damage <= stack.currentStack)
                    {
                        RemoveStack(stack.stackData, damage);
                        return damage;
                    }
                    else if (damage > stack.currentStack)
                    {
                        RemoveStack(stack.stackData, stack.currentStack);
                        return stack.currentStack;
                    }
                }
            }
        }
        return 0;
    }

    public void PassiveFloatReset()
    {
        bleeddamagedecrease = bleeddamagedecreaseCore;

        healplus = healplusCore;

        backdelay = backdelayCore;

        slash_tolerance = slash_toleranceCore;

        penetration_tolerance = penetration_toleranceCore;
  
        blow_tolerance = bleeddamagedecreaseCore;

        damagedecrease = damagedecreaseCore;

        penetratedamageup = penetratedamageupCore;

        selfharmdamagepercent = selfharmdamagepercentCore;

        speed = speedCore;

        attackpower = attackpowerCore;

        WhenStackChange();
    }

    public void BattaleStart()
    {
        RapportAdd();
        lifecount = maxlifecount;
        balancebarint.maxValue = maxbalance * r_balancemaxincrease;
        currentbalance = 0;
    }

    public void StartTyping(string message)
    {
        chat.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        chat.GetComponent<Image>().DOFade(255f, 0.4f);
        chat.GetComponentInChildren<TMP_Text>().color = new Color(255f, 255f, 255f, 0f);
        chat.GetComponentInChildren<TMP_Text>().DOFade(255f, 0.4f);

        // 이미 실행 중인 코루틴이 있으면 개별적으로 정지
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypingCoroutine(message));
    }

    private IEnumerator TypingCoroutine(string message)
    {
        chat.GetComponentInChildren<TMP_Text>().text = "";

        foreach (char c in message)
        {
            chat.GetComponentInChildren<TMP_Text>().text += c;
            yield return new WaitForSeconds(0.05f);
        }

        typingCoroutine = null; // 끝나면 null로 초기화
        chat.GetComponent<Image>().DOFade(0f, 0.4f);
        chat.GetComponentInChildren<TMP_Text>().DOFade(0f, 0.4f);
    }

    public void BalanceCheck()
    {
        balancebarint.value = currentbalance;

        uimanager.Instance.playerbalance.GetComponent<Slider>().maxValue = maxbalance;
        uimanager.Instance.playerbalance.GetComponent<Slider>().value = currentbalance;
    }

    public void BalanceDamage(float balance)
    {
        OnHit?.Invoke();
        //Debug.Log("ADada");
        GetComponent<Passivefunction>().PlayerHit();

        float totaldamage = balance * (damagedecrease + damagedecreaserealtime);
        int absorbtion = WhenHitAbsorbtion((int)totaldamage);
        currentbalance += totaldamage - absorbtion;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            BalanceCollapse();
        }
    }

    public void StartUseStandbySkill()
    {
        skillcellindex = 0;
    }

    public void StandbySkill()
    {
        var skillcell = currentstandbyskill.skillcells[skillcellindex];
        //딜
    }

    public void SlashDamage(float balance)
    {
        OnHit?.Invoke();
        //Debug.Log("ADada");
        GetComponent<Passivefunction>().PlayerHit();

        float totaldamage = (balance * (damagedecrease + damagedecreaserealtime)) * slash_tolerance;
        int absorbtion = WhenHitAbsorbtion((int)totaldamage);
        currentbalance += totaldamage - absorbtion;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            BalanceCollapse();
        }
    }

    public void PenetrateDamage(float balance)
    {
        OnHit?.Invoke();
        //Debug.Log("ADada");
        GetComponent<Passivefunction>().PlayerHit();
        PenetrationHit();

        float totaldamage = (balance * (damagedecrease + damagedecreaserealtime)) * penetration_tolerance;
        int absorbtion = WhenHitAbsorbtion((int)totaldamage);
        currentbalance += totaldamage - absorbtion;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            BalanceCollapse();
        }
    }

    public void BlowDamage(float balance)
    {
        OnHit?.Invoke();
        //Debug.Log("ADada");
        GetComponent<Passivefunction>().PlayerHit();

        float totaldamage = (balance * (damagedecrease + damagedecreaserealtime)) * blow_tolerance;
        int absorbtion = WhenHitAbsorbtion((int)totaldamage);
        currentbalance += totaldamage - absorbtion;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            BalanceCollapse();
        }
    }

    public void BalanceHeal(float balance)
    {
        //Debug.Log("ADada");
        currentbalance -= balance * (healplus + r_healincrease);
        BalanceCheck();
    }

    public void BalanceCollapse()
    {
        lifeCountDown();
    }

    public void SelfBalanceDamage(float balance)
    {
        //Debug.Log("ADada");
        GetComponent<Passivefunction>().PlayerHit();

        currentbalance += (balance * selfharmdamagepercent);
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            BalanceCollapse();
        }

        
    }

    public void lifeCountDown()
    {
        lifecount = lifecount - 1;
        if (lifecount > 0)
        {
            battalemanager.Instance.cronometer.GetComponent<cronometer_script>().WhenLifeCoutDown();
        }
        else if (lifecount == 0)
        {
            battalemanager.Instance.cronometer.GetComponent<cronometer_script>().WhenLifeCoutDownEnd();
        }
        
    }

    public void Parrystop()
    {
        if (currentparrystop != null)
        {
            StopCoroutine(currentparrystop);
        }
        currentparrystop = StartCoroutine(ParryStop());
    }

    IEnumerator ParryStop()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.15f);
        Time.timeScale = 1f;
    }

    public void CutSceneUiDisappear()
    {
        cutsceneuidisappear.SetActive(false);
    }

    public void CutSceneUiAppear()
    {
        cutsceneuidisappear.SetActive(true);
    }


}
