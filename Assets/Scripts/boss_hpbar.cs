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

    public float height;
    public float height2;
    public float side;

    public bool iscollapse;
    public bool canhit = true;

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
            if (existing.currentStack <= 0)
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

    public void PrintStacks()
    {
        foreach (var s in activeStacks)
        {
            Debug.Log($"{s.stackData.effectName}: {s.currentStack}/{s.stackData.maxStacks}");
        }
    }

    private void Start()
    {
        currenthealth = maxhealth;
        balancebarint.maxValue = maxbalance;
        currentbalance = 0;
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
            currenthealth -= damage;
            BalanceDamage(damage * 0.1f);
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
            currenthealth -= damage * slashtolerance;
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
            damtdamagetext.damage = damage;
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
            currenthealth -= damage * penetratetolerance;
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
            damtdamagetext.damage = damage;
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
            currenthealth -= damage * blowtolerance;
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
            damtdamagetext.damage = damage;
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

    public bool killcut;

    public void Update()
    {
        if (killcut)
        {
            Time.timeScale = 0.2f;
        }
    }

    IEnumerator Dead_co()
    {
        GetComponent<Animator>().SetTrigger("dying");
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
