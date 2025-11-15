using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void Start()
    {
        InstitateSkillUi();
    }

    public void ButtonPress()
    {
        SkillcontentActive();
    }


   

    public void SkillcontentActive()
    {
        foreach (Transform child in viewpoint.transform)
        {
            child.gameObject.SetActive(false);
        }
        curcontentnow.SetActive(true);
        scrollview.GetComponent<ScrollRect>().content = curcontentnow.GetComponent<RectTransform>();

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
        passivelist = Instantiate(passivedescriptionContentpre, passivelistViewPort.transform);
        normalskilltlist = Instantiate(descriptionContentpre, normalskilltlistViewPort.transform);
        arreyskilllist = Instantiate(descriptionContentpre, arreyskilllistViewPort.transform);


        passivelist.transform.GetChild(0).GetComponent<TMP_Text>().text = weapon.passive_description;
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

        GameObject curcontent = Instantiate(content, viewpoint.transform);
        curcontentnow = curcontent;

        foreach (Skill skill in weapon.skilllist)
        {
            GameObject currentskillUi = Instantiate(skillUi, curcontent.transform);
            currentskillUi.transform.GetChild(0).GetComponent<TMP_Text>().text = skill.skillmarkname;
            currentskillUi.GetComponent<skillselectUi>().attackcore = attackcore;
            currentskillUi.GetComponent<skillselectUi>().skill = skill;
            currentskillUi.GetComponent<skillselectUi>().skilllist = skillsetlist;

            GameObject currentbox = Instantiate(normalskilltextprefap, normalskilltlist.transform);
            currentbox.transform.GetChild(0).GetComponent<TMP_Text>().text = skill.skilldescription;
            currentbox.GetComponent<normalskilluibutton>().skillbutton = currentskillUi;

            currentskillUi.GetComponent<skillselectUi>().descriptionUi = currentbox;
        }
        curcontentnow.SetActive(false);
        passivelist.SetActive(false);
        normalskilltlist.SetActive(false);
        arreyskilllist.SetActive(false);
    }
}
