using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class boss_hpbar : MonoBehaviour
{
    public static event Action OnHitCalled;
    public static event Action Die;
    public static event Action OnCycleEnd;

    public GameObject worldlight;
    public GameObject playerlight;
    public GameObject gammanager;
    public GameObject cammanager;

    public GameObject attackcore;

    public GameObject damagepos;

    public GameObject balancebar;
    public GameObject stackbar;
    public GameObject canvas;
    public Slider balancebarint;

    RectTransform hpbarrect;

    public GameObject damagetext;

    public float collapsefloat;

    public float maxhealth;
    public float currenthealth;
    public float maxbalance;
    public float currentbalance;

    public float slashtolerance;
    public float penetratetolerance;
    public float blowtolerance;
    public float slashtoleranceCore;
    public float penetratetoleranceCore;
    public float blowtoleranceCore;

    public float damageplus;
    public float damageplusCore = 1;

    public float bleeddamageincrease;
    public float bleeddamageincreaseCore = 1;

    public float height;
    public float height2;
    public float side;

    public bool iscollapse;
    public bool canhit = true;
    public bool killcut;

    public bool candie = true;

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
            WhenStackAdd(newStack);
        }

        Debug.Log($"Applied stack: {newStack.effectName} (+{amount})");

        OnStackApplied?.Invoke(newStack, amount);

        canvas.GetComponent<boss_stackUIManager>().RefreshUI();


        //GetComponent<Passivefunction>().WhenAddStack();
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
        canvas.GetComponent<boss_stackUIManager>().RefreshUI();

        //GetComponent<Passivefunction>().WhenRemoveStack();
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

        canvas.GetComponent<boss_stackUIManager>().RefreshUI();
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


    private void Start()
    {
        currenthealth = maxhealth;
        balancebarint.maxValue = maxbalance;
        currentbalance = 0;
    }

    //완전 새 스텍일떄만 작동
    public void WhenStackAdd(Stack stack)
    {
        if (activeStacks.Count > 0)
        {
            if (stack.effectName == "치명적 열상 I")
            {
                bleeddamageincrease += 0.1f;
                if (activeStacks.Find(s => s.stackData.effectName == "출혈") != null)
                {
                    damageplus += 0.1f;
                }
            }
            if (stack.effectName == "치명적 열상 II")
            {
                bleeddamageincrease += 0.2f;
                if (activeStacks.Find(s => s.stackData.effectName == "출혈") != null)
                {
                    damageplus += 0.2f;
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
        canvas.GetComponent<boss_stackUIManager>().RefreshUI();
    }

    public void CycleEnd()
    {
        OnCycleEnd?.Invoke();

        if (activeStacks.Count > 0)
        {
            foreach (StackInstance stack in activeStacks)
            {
                if (stack.stackData.effectName == "출혈")
                {
                    Damage((int)(stack.currentStack * bleeddamageincrease));
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
            }
        }
        
    }

    public void BalanceCheck()
    {
        balancebarint.value = currentbalance;
    }

    public void BalanceDamage(float balance)
    {
        currentbalance += balance;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            BalanceCollapse();
        }
    }

    public void BalanceCollapse()
    {
        GetComponent<Animator>().SetBool("idle", false);
        GetComponent<Animator>().SetTrigger("collapse");

        currentbalance = 0;
        if (attackcore.GetComponent<attackcore>().standbyskills.Count > 0)
        {
            Debug.Log("대기스킬이 1개 이상이므로 대기스킬 사용");
            attackcore.GetComponent<attackcore>().UseStandbySkill();
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
        GetComponent<Animator>().SetBool("idle", true);
        iscollapse = false;
        attackcore.GetComponent<attackcore>().NostandByskill();
    }

    public void Redemisson()
    {
        DOTween.Kill("enemyflash");
        GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", 0.5f);
        DOTween.To(() => GetComponent<SpriteRenderer>().material.GetFloat("_flashamount"), value => GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", value), 0f, 0.35f).SetEase(Ease.OutQuart).SetUpdate(true).SetId("enemyflash");
    }

    public void Damage(int damage)
    {
        if (canhit)
        {
            OnHitCalled?.Invoke();

            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;
            float totaldamage = damage * damageplus;
            currenthealth -= totaldamage;
            BalanceDamage(damage * 0.1f);
            GameObject damt = Instantiate(damagetext);
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
            damt.transform.position = damagepos.transform.position;
            damtdamagetext.damage = (int)totaldamage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }
        
    }

    public void SlashDamage(int damage)
    {
        if (canhit)
        {
            OnHitCalled?.Invoke();

            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;
            float totaldamage = (damage * slashtolerance) * damageplus;
            currenthealth -= totaldamage;
            BalanceDamage(damage * 0.1f);
            GameObject damt = Instantiate(damagetext);
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
            damt.transform.position = damagepos.transform.position;
            damtdamagetext.damage = (int)totaldamage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }
        
    }

    public void PenetrateDamage(int damage)
    {
        if (canhit)
        {
            OnHitCalled?.Invoke();

            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;
            float totaldamage = (damage * penetratetolerance) * damageplus;
            currenthealth -= totaldamage;
            BalanceDamage(damage * 0.1f);
            GameObject damt = Instantiate(damagetext);
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
            damt.transform.position = damagepos.transform.position;
            damtdamagetext.damage = (int)totaldamage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }
        
    }

    public void BlowDamage(int damage)
    {
        if (canhit)
        {
            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;
            float totaldamage = (damage * blowtolerance) * damageplus;
            currenthealth -= totaldamage;
            BalanceDamage(damage * 0.1f);
            GameObject damt = Instantiate(damagetext);
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
            damt.transform.position = damagepos.transform.position;
            damtdamagetext.damage = (int)totaldamage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }
        
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
        }
        Die?.Invoke();
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
    }
}
