using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class playerstatus : MonoBehaviour
{
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

    [Range(-3, 3)]
    public float emotionrate;
    [Range(-3, 3)]
    public float reasonrate;
    [Range(-3, 3)]
    public float recognitionrate;

    public float slash_tolerance = 1;
    public float penetration_tolerance = 1;
    public float blow_tolerance = 1;
    public float slash_toleranceCore = 1;
    public float penetration_toleranceCore = 1;
    public float blow_toleranceCore = 1;

    public float damagedecrease;
    public float damagedecreaseCore = 1;

    public float penetratedamageup;
    public float penetratedamageupCore = 1;

    public float focus;
    public float maxbalance;
    public float currentbalance;
    public int maxlifecount;
    public int lifecount;
    public float speed;
    public float attackpower;
    public float attackpowerCore = 10;

    public float bleeddamagedecrease;
    public float bleeddamagedecreaseCore = 1;

    public float healplus;
    public float healplusCore = 1;

    public float selfharmdamagepercent;
    public float selfharmdamagepercentCore = 1;

    public Coroutine currentparrystop;

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

        Debug.Log($"Applied stack: {newStack.effectName} (+{amount})");

        OnStackApplied?.Invoke(newStack, amount);

        canvus.GetComponent<stackUimanager>().RefreshUI();

        GetComponent<Passivefunction>().WhenAddStack();
    }

    public void RemoveStack(Stack targetStack, int amount)
    {
        StackInstance existing = activeStacks.Find(s => s.stackData == targetStack);

        if (existing != null)
        {
            existing.RemoveStack(amount);
            Debug.Log($"Removed stack: {targetStack.effectName} (-{amount})");

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
        canvus.GetComponent<stackUimanager>().RefreshUI();

        GetComponent<Passivefunction>().WhenRemoveStack();
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

    //public void RemoveStackWhenCycleEnd()
    //{
    //    foreach (StackInstance stackInstance in activeStacks)
    //    {
    //        if (stackInstance.stackData.whendecrease == TriggerType.OnCircumEnd)
    //        {
    //            stackInstance.currentStack -= 1;
    //            if (stackInstance.currentStack <= 0 && stackInstance.stackData.disappear_whenzero)
    //            {
    //                activeStacks.Remove(stackInstance);
    //            }
    //            canvus.GetComponent<stackUimanager>().RefreshUI();
    //        }
    //    }
    //}


    public void PrintStacks()
    {
        foreach (var s in activeStacks)
        {
            Debug.Log($"{s.stackData.effectName}: {s.currentStack}/{s.stackData.maxStacks}");
        }
    }

    public void CycleStart()
    {
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
                    damagedecrease = damagedecrease * (1.01f * stack.currentStack);
                    attackpower = attackpower * (1.01f * stack.currentStack);

                }
            }
        }
    }

    public void CycleEnd()
    {
        if (activeStacks.Count > 0)
        {
            foreach (StackInstance stack in activeStacks)
            {
                if (stack.stackData.effectName == "출혈")
                {
                    BalanceDamage(stack.currentStack * bleeddamagedecrease);
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

    public void PassiveFloatReset()
    {
        bleeddamagedecrease = bleeddamagedecreaseCore;

        healplus = healplusCore;

        slash_tolerance = slash_toleranceCore;

        penetration_tolerance = penetration_toleranceCore;
  
        blow_tolerance = bleeddamagedecreaseCore;

        damagedecrease = damagedecreaseCore;

        penetratedamageup = penetratedamageupCore;

        selfharmdamagepercent = selfharmdamagepercentCore;

        attackpower = attackpowerCore;
    }

    private void Start()
    {
        lifecount = maxlifecount;
        balancebarint.maxValue = maxbalance;
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
    }

    public void BalanceDamage(float balance)
    {
        GetComponent<Passivefunction>().PlayerHit();

        currentbalance += balance * damagedecrease;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            BalanceCollapse();
        }
    }

    public void SlashDamage(float balance)
    {
        GetComponent<Passivefunction>().PlayerHit();

        currentbalance += balance * slash_tolerance;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            BalanceCollapse();
        }
    }

    public void PenetrateDamage(float balance)
    {
        GetComponent<Passivefunction>().PlayerHit();

        currentbalance += balance * penetration_tolerance;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            BalanceCollapse();
        }
    }

    public void BlowDamage(float balance)
    {
        GetComponent<Passivefunction>().PlayerHit();

        currentbalance += balance * blow_tolerance;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            BalanceCollapse();
        }
    }

    public void BalanceHeal(float balance)
    {
        currentbalance -= balance * healplus;
        BalanceCheck();
    }

    public void BalanceCollapse()
    {
        lifeCountDown();
    }

    public void SelfBalanceDamage(float balance)
    {
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
        cronometer.GetComponent<cronometer_script>().WhenLifeCoutDown();
    }

    public void Parrystop()
    {
        currentparrystop = StartCoroutine(ParryStop());
    }

    IEnumerator ParryStop()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.15f);
        Time.timeScale = 1f;
    }

}
