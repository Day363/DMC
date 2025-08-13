using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class boss_hpbar : MonoBehaviour
{
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

    private void Update()
    {
        Vector3 balancebarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + side, transform.position.y + height, 0));
        balancebar.transform.position = balancebarpos;
        Vector2 stackbarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + side, transform.position.y + height2, 0));
        stackbar.transform.position = stackbarpos;
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
            currentbalance = 0;
            if (attackcore.GetComponent<attackcore>().standbyskills.Count > 0)
            {
                attackcore.GetComponent<attackcore>().UseStandbySkill();
            }

            GetComponent<Animator>().SetTrigger("collapse");
            GetComponent<Animator>().SetBool("idle", false);
            iscollapse = true;
            // 균형붕괴
        }
    }

    IEnumerator Collapsetimeout()
    {
        yield return new WaitForSeconds(collapsefloat);
        GetComponent<Animator>().SetBool("collapse", false);
        iscollapse = false;
    }

    public void Damage(int damage)
    {
        cammanager.GetComponent<CameraManager>().CamVibration0_5();
        attackcore.GetComponent<attackcore>().BossDamaged();

        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage;
        BalanceDamage(damage * 0.1f);
        if (currenthealth <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }

    public void SlashDamage(int damage)
    {

        cammanager.GetComponent<CameraManager>().CamVibration0_5();
        attackcore.GetComponent<attackcore>().BossDamaged();

        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage * slashtolerance;
        BalanceDamage(damage * 0.1f);
        GameObject damt = Instantiate(damagetext);
        if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
        {
            damt.GetComponent<damagetext>().wherexpos = 1;
        }
        else
        {
            damt.GetComponent<damagetext>().wherexpos = -1;
        }
        damt.GetComponent<damagetext>().slash = true;
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        if (currenthealth <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }

    public void PenetrateDamage(int damage)
    {
        cammanager.GetComponent<CameraManager>().CamVibration0_5();
        attackcore.GetComponent<attackcore>().BossDamaged();

        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage * penetratetolerance;
        BalanceDamage(damage * 0.1f);
        GameObject damt = Instantiate(damagetext);
        if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
        {
            damt.GetComponent<damagetext>().wherexpos = 1;
        }
        else
        {
            damt.GetComponent<damagetext>().wherexpos = -1;
        }
        damt.GetComponent<damagetext>().penetarte = true;
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        if (currenthealth <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }

    public void BlowDamage(int damage)
    {
        cammanager.GetComponent<CameraManager>().CamVibration0_5();
        attackcore.GetComponent<attackcore>().BossDamaged();

        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage * blowtolerance;
        BalanceDamage(damage * 0.1f);
        GameObject damt = Instantiate(damagetext);
        if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
        {
            damt.GetComponent<damagetext>().wherexpos = 1;
        }
        else
        {
            damt.GetComponent<damagetext>().wherexpos = -1;
        }
        damt.GetComponent<damagetext>().blow = true;
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        if (currenthealth <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }
}
