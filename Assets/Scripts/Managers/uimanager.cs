using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using TMPro;

public class uimanager : MonoBehaviour
{
    public static uimanager Instance;
    public static event Action OnUIReady;

    [Header("activewhenbattlestart")]
    public GameObject[] activewhenbattlestart;
    [Header("playerstatus")]
    public GameObject skillwaitprefap;
    public GameObject defense;
    public GameObject bullet;
    public GameObject cycle;
    public GameObject circum;
    public GameObject skillwait;
    public GameObject weaponimage;
    [Header("selectskill")]
    public GameObject skillselectui;
    public GameObject weaponlist;
    public GameObject skilllist;
    public GameObject skilllistviewport;
    public GameObject passiveview;
    public GameObject normalskillview;
    public GameObject arreyskillview;
    public GameObject skilllength;
    public GameObject skilllistset;
    public GameObject warning;
    public SkillQueUi skillQueUi;
    public GameObject waitskillslider;
    [Header("letterbox")]
    public GameObject letterbox;
    [Header("inv")]
    public GameObject iteminv1;
    public GameObject iteminv2;
    public GameObject itemimage;
    public GameObject itemname;
    public GameObject itemstroy;
    public GameObject itemdescription;
    public GameObject itemtag;
    public GameObject itemgiveui;
    public GameObject itemgiveselect;
    [Header("gameover")]
    public GameObject gameover;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // ¾À À¯Áö
    }

    void Start()
    {
        OnUIReady?.Invoke();
    }

    public void ResetUi()
    {
        foreach (Transform ui in skilllistset.transform)
        {
            ui.GetComponent<skillbuttondisappear>().ButtonDisappearWhenUiReset();
        }
        foreach (GameObject gameobject in activewhenbattlestart)
        {
            gameobject.SetActive(false);
        }
        foreach (Transform ui in skilllength.transform)
        {
            Destroy(ui);
        }
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
        gameover.transform.GetChild(0).transform.GetComponent<gamerestart>().whilerestarting = false;
        gameover.transform.GetChild(0).GetComponent<Image>().DOFade(0.447f, 0);
        gameover.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().DOFade(1, 0);
        gameover.SetActive(false);
    }
}
