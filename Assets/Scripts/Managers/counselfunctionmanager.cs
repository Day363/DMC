using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class counselfunctionmanager : MonoBehaviour
{
    public static Action Onsuccess;
    public static Action OnCutSceneObjectActive;
    public static Action OnCutSceneEnd;


    public static counselfunctionmanager Instance;
    public GameObject attackcore;
    public GameObject letterbox;

    Dictionary<string, Action> functionMap;

    battalemanager battalemanager;

    public GameObject player;

    public GameObject glitch;
    public GameObject fadeout;

    public void Start()
    {
        battalemanager = GetComponent<battalemanager>();
        player = GetComponent<battalemanager>().player;
    }

    public void Awake()
    {
        Instance = this;

        functionMap = new Dictionary<string, Action>
        {
            { "Animation1", Animation1 },
            { "Animation2", Animation2 },
            { "Animation3", Animation3 },
            { "Animation4", Animation4 },
            { "Animation5", Animation5 },
            { "trapal_start", Trapal_start },
            { "start",  BattleStart},
            { "success", CounselSuccess },
            { "trapal_die1", Trapal_Die1 },
            { "indexer_disappear", IndexerDisappear },
            { "cutscene1", CutScene1 },
            { "cutScene2ray", CutScene2Ray },
            { "CutScene2End", CutScene2End }
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
        battalemanager.Instance.currentenemy.GetComponent<Animator>().SetTrigger("animation1");
    }

    public void Animation2()
    {
        battalemanager.Instance.currentenemy.GetComponent<Animator>().SetTrigger("animation2");
    }

    public void Animation3()
    {
        battalemanager.Instance.currentenemy.GetComponent<Animator>().SetTrigger("animation3");
    }

    public void Animation4()
    {
        Debug.Log("$rea");
        battalemanager.Instance.currentenemy.GetComponent<Animator>().SetTrigger("animation4");
    }

    public void Animation5()
    {
        battalemanager.Instance.currentenemy.GetComponent<Animator>().SetTrigger("animation5");
    }

    public void Trapal_start()
    {
        battalemanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        if (battalemanager.currentenemy.TryGetComponent<Animator>(out Animator ta))
        {
            ta.SetTrigger("start");
        }
    }

    public void BattleStart()
    {
        battalemanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        if (battalemanager.Instance.currentenemy.TryGetComponent<Animator>(out Animator ta))
        {
            ta.SetTrigger("start");
            StartCoroutine(BattaleStartAttackCore());
        }
    }

    IEnumerator BattaleStartAttackCore()
    {
        yield return new WaitForSeconds(1.5f);
        battalemanager.Instance.attackcore.GetComponent<attackcore>().SetCronometer();
    }

    public void CounselSuccess()
    {
        Onsuccess?.Invoke();
    }

    public void Trapal_Die1()
    {
        battalemanager.Instance.currentenemy.GetComponent<trapal_counsel>().Die1();
    }

    public void IndexerDisappear()
    {
        battalemanager.Instance.currentenemy.GetComponent<indexer0_counsel>().DieDisapper();
    }

    public void CutScene1()
    {
        player.GetComponent<Animator>().SetTrigger("cutscene1");
    }

    public void CutScene2Ray()
    {
        OnCutSceneObjectActive?.Invoke();
    }

    public void CutScene2End()
    {
        OnCutSceneEnd?.Invoke();
    }
}
