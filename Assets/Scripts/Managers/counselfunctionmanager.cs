using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class counselfunctionmanager : MonoBehaviour
{
    public static counselfunctionmanager Instance;
    public GameObject attackcore;
    public GameObject letterbox;

    Dictionary<string, Action> functionMap;

    battalemanager battalemanager;

    public void Start()
    {
        battalemanager = GetComponent<battalemanager>();
    }

    public void Awake()
    {
        Instance = this;

        functionMap = new Dictionary<string, Action>
        {
            { "Animation1", Animation1 },
            { "Animation2", Animation2 },
            { "Animation3", Animation3 },
            { "trapal_start", Trapal_start },
            { "start",  BattleStart}
        };
    }

    public void FuctionStart(string fucntionname)
    {
        if (functionMap.TryGetValue(fucntionname, out Action action))
        {
            action.Invoke();
        }
        else
        {
            Debug.LogWarning($"{fucntionname}라는 함수는 넣은적 없단다 빠빡대가리야");
        }
    }

    public void Animation1()
    {
        battalemanager.currentenemy.GetComponent<Animator>().SetTrigger("animation1");
    }

    public void Animation2()
    {
        battalemanager.currentenemy.GetComponent<Animator>().SetTrigger("animation2");
    }

    public void Animation3()
    {
        battalemanager.currentenemy.GetComponent<Animator>().SetTrigger("animation3");
    }

    public void Trapal_start()
    {
        letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        if (battalemanager.currentenemy.TryGetComponent<Animator>(out Animator ta))
        {
            ta.SetTrigger("start");
        }
    }

    public void BattleStart()
    {
        letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        if (battalemanager.currentenemy.TryGetComponent<Animator>(out Animator ta))
        {
            ta.SetTrigger("start");
            StartCoroutine(BattaleStartAttackCore());
        }
    }

    IEnumerator BattaleStartAttackCore()
    {
        yield return new WaitForSeconds(4f);
        attackcore.GetComponent<attackcore>().BattleStart();
    }

}
