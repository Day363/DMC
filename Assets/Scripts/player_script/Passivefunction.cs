using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Passivefunction : MonoBehaviour
{
    public List<string> activepassivelist = new List<string> { };
    public Dictionary<string, Action> boolFunctions;
    public playerstatus playerStackHandler;

    public Stack Inference;
    public Stack certain;
    public Stack deny;

    public bool ishaloactive;
    public GameObject trapal_halo;
    public List<GameObject> trapal_certain_texts = new List<GameObject> { };
    public List<GameObject> trapal_deny_texts = new List<GameObject> { };
    
    
    private bool trapal_passive1 = false;
    private bool trapal_passive2 = false;
    private bool trapal_passive3 = false;

    private void OnEnable()
    {
        playerstatus.OnStackApplied += WhenStackAddCertain;
        playerstatus.OnStackRemoved += WhenStackRemoveCertain;
    }

    private void OnDisable()
    {
        playerstatus.OnStackApplied -= WhenStackAddCertain;
        playerstatus.OnStackRemoved -= WhenStackRemoveCertain;
    }

    public void SetBoolsFromList(List<string> activeList)
    {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(bool))
            {
                bool shouldBeActive = activeList.Contains(field.Name);
                field.SetValue(this, shouldBeActive);
            }
        }
    }

    public void WhenCircumStart()
    {
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
        if (trapal_passive2)
        {
            playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
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
    }
}
