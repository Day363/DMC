using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class uimanager : MonoBehaviour
{
    public static uimanager Instance;
    public static event Action OnUIReady;

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
    public GameObject itemgiveselect;


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
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
    }
}
