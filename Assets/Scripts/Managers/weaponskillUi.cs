using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class weaponskillUi : MonoBehaviour
{
    public GameObject scrollview;
    public GameObject passivescrollview;
    public GameObject normalskillscrollview;
    public GameObject arreyskilllscrollview;

    public GameObject viewpoint;
    public GameObject content;
    public GameObject curcontentnow;

    public GameObject passivelistViewPort;
    public GameObject normalskilltlistViewPort;
    public GameObject arreyskilllistViewPort;
    public GameObject descriptionContentpre;
    public GameObject passivedescriptionContentpre;

    public GameObject passivelist;
    public GameObject normalskilltlist;
    public GameObject arreyskilllist;

    public GameObject attackcore;
    public GameObject skillUi;
    public Weapon weapon;
    public GameObject skillsetlist;

    public GameObject normalskilltextprefap;
    public GameObject arreyskilltextprefap;

    public RectTransform rect;
    public bool isHover;


    public void OnEnable()
    {
        InstitateSkillUi();
    }

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void ButtonPress()
    {
        soundmanager.instance.SoundPlay("click2");
        SkillcontentActive();
    }

    void Update()
    {
        bool nowHover = RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition);

        if (nowHover && !isHover)
        {
            isHover = true;
            ScaleUp();
        }
        else if (!nowHover && isHover)
        {
            isHover = false;
            ScaleDown();
        }
    }

    

    public void ScaleUp()
    {
        rect.DOKill();
        GetComponent<RectTransform>().DOScale(1.3f, 0.1f).SetId("UIScale").SetUpdate(true); 
    }

    public void ScaleDown()
    {
        rect.DOKill();
        GetComponent<RectTransform>().DOScale(1f, 0.4f).SetId("UIScale").SetUpdate(true);
    }




    public void SkillcontentActive()
    {
        scrollview.GetComponent<rullet>().target = curcontentnow.transform;
        scrollview.GetComponent<rullet>().totalAngle = 90 - (curcontentnow.transform.childCount * curcontentnow.GetComponent<circlelayout>().angleStep);
        curcontentnow.transform.rotation = Quaternion.Euler(0, 0, 90 - (curcontentnow.transform.childCount * curcontentnow.GetComponent<circlelayout>().angleStep));
        foreach (Transform child in viewpoint.transform)
        {
            child.gameObject.SetActive(false);
        }
        curcontentnow.SetActive(true);

        foreach (Transform child in passivelistViewPort.transform)
        {
            child.gameObject.SetActive(false);
        }
        passivelist.SetActive(true);
        passivescrollview.GetComponent<ScrollRect>().content = passivelist.GetComponent<RectTransform>();

        foreach (Transform child in normalskilltlistViewPort.transform)
        {
            child.gameObject.SetActive(false);
        }
        normalskilltlist.SetActive(true);
        normalskillscrollview.GetComponent<ScrollRect>().content = normalskilltlist.GetComponent<RectTransform>();

        foreach (Transform child in arreyskilllistViewPort.transform)
        {
            child.gameObject.SetActive(false);
        }
        arreyskilllist.SetActive(true);
        arreyskilllscrollview.GetComponent<ScrollRect>().content = arreyskilllist.GetComponent<RectTransform>();
    }

    public void InstitateSkillUi()
    {
        if (passivelist == null)
        {
            passivelist = Instantiate(passivedescriptionContentpre, passivelistViewPort.transform);
        }
        if (normalskilltlist == null)
        {
            normalskilltlist = Instantiate(descriptionContentpre, normalskilltlistViewPort.transform);
        }
        if (arreyskilllist == null)
        {
            arreyskilllist = Instantiate(descriptionContentpre, arreyskilllistViewPort.transform);
        }
        


        passivelist.transform.GetChild(0).GetComponent<TMP_Text>().text = weapon.passive_description;
        if (arreyskilllist.transform.childCount < 1)
        {
            foreach (cynthskill cynthskill in weapon.cynthskilllist)
            {
                GameObject currentbox = Instantiate(arreyskilltextprefap, arreyskilllist.transform);
                currentbox.transform.GetChild(0).GetComponent<TMP_Text>().text = cynthskill.skill.skilldescription;
                currentbox.GetComponent<arreyskillui>().skillArreyViewport = viewpoint;
                currentbox.GetComponent<arreyskillui>().cynthskill = cynthskill;
            }
            foreach (standbyskill standbyskill in weapon.standbyskilllist)
            {
                GameObject currentbox = Instantiate(arreyskilltextprefap, arreyskilllist.transform);
                currentbox.transform.GetChild(0).GetComponent<TMP_Text>().text = standbyskill.skilldescription;
                currentbox.GetComponent<arreyskillui>().skillArreyViewport = viewpoint;
                currentbox.GetComponent<arreyskillui>().standbyskill = standbyskill;
            }
        }
        

        if (curcontentnow !=  null)
        {
            //Destroy(curcontentnow);
            curcontentnow.SetActive(true);
        }
        else
        {
            GameObject curcontent = Instantiate(content, viewpoint.transform);
            curcontentnow = curcontent;
        }
        
        if (curcontentnow.transform.childCount < 1) 
        {
            foreach (Skill skill in weapon.skilllist)
            {
                GameObject currentskillUi = Instantiate(skillUi, curcontentnow.transform);
                currentskillUi.transform.GetChild(0).GetComponent<TMP_Text>().text = skill.skillmarkname;
                currentskillUi.GetComponent<skillselectUi>().attackcore = attackcore;
                currentskillUi.GetComponent<skillselectUi>().skill = skill;
                currentskillUi.GetComponent<skillselectUi>().skilllist = skillsetlist;

                GameObject currentbox = Instantiate(normalskilltextprefap, normalskilltlist.transform);
                currentbox.transform.GetChild(0).GetComponent<TMP_Text>().text = skill.skilldescription;
                currentbox.GetComponent<normalskilluibutton>().skillbutton = currentskillUi;

                currentskillUi.GetComponent<skillselectUi>().descriptionUi = currentbox;


            }
        }
        
        curcontentnow.GetComponent<circlelayout>().ArrangeNotEquel();
        curcontentnow.SetActive(false);
        passivelist.SetActive(false);
        normalskilltlist.SetActive(false);
        arreyskilllist.SetActive(false);
    }
}
