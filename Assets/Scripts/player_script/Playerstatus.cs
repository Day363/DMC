using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class playerstatus : MonoBehaviour
{
    [SerializeField]
    public Transform stackbar;
    public Slider balancebar;
    public TMP_Text hptext;
    public Slider balancebarint;
    public GameObject canvus;

    public float slash_tolerance = 1;
    public float penetration_tolerance = 1;
    public float blow_tolerance = 1;

    public float side;
    public float height;
    public float side2;
    public float height2;

    public float maxbalance;
    public float currentbalance;
    public float speed;
    public int attackpower;

    public float bleeddamage;
    public float disabledbleeddamagepercent = 1;
    public float alttriggerdecreaseselfdamagepercent = 1;

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

            // ½ºÅÃÀÌ 0ÀÌ¸é ¸ñ·Ï¿¡¼­ Á¦°Å
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

    public void RemoveStackWhenCycleEnd()
    {
        foreach (StackInstance stackInstance in activeStacks)
        {
            if (stackInstance.stackData.whendecrease == TriggerType.OnCircumEnd)
            {
                stackInstance.currentStack -= 1;
                if (stackInstance.currentStack <= 0 && stackInstance.stackData.disappear_whenzero)
                {
                    activeStacks.Remove(stackInstance);
                }
                canvus.GetComponent<stackUimanager>().RefreshUI();
            }
        }
    }

    public void TriggerWhenCycleEnd()
    {
        StackInstance instance = activeStacks.Find(s => s.stackData.effectName == "ÃâÇ÷");
        if (instance == null)
        {
            bleeddamage = 0;
        }

        foreach (StackInstance stackInstance in activeStacks)
        {
            if (stackInstance.stackData.trigger == TriggerType.OnCircumEnd)
            {
                if (stackInstance.stackData.effectName == "ÃâÇ÷")
                {
                    bleeddamage += 5;
                    BalanceDamage(bleeddamage * disabledbleeddamagepercent);
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

    private void Start()
    {

        balancebarint.maxValue = maxbalance;
        currentbalance = 0;
    }

    private void Update()
    {
        Vector3 balancebarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + side, transform.position.y + height, 0));
        balancebar.transform.position = balancebarpos;

        Vector3 stackbarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + side2, transform.position.y + height2, 0));
        stackbar.transform.position = stackbarpos;
    }

    public void BalanceCheck()
    {
        balancebarint.value = currentbalance;
    }

    public void BalanceDamage(float balance)
    {
        GetComponent<Passivefunction>().PlayerHit();

        currentbalance += balance;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            // ±ÕÇüºØ±«
        }

        
    }

    public void SelfBalanceDamage(float balance)
    {
        GetComponent<Passivefunction>().PlayerHit();

        currentbalance += (balance * alttriggerdecreaseselfdamagepercent);
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            // ±ÕÇüºØ±«
        }

        
    }

}
