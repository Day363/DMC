using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class weaponskillUi : MonoBehaviour
{
    public GameObject viewpoint;
    public GameObject content;
    public GameObject curcontentnow;

    public TMP_Text passivetext;
    public TMP_Text normalskilltext;
    public TMP_Text arreyskilltext;

    public GameObject attackcore;
    public GameObject skilllistUi;
    public GameObject skillUi;
    public Weapon weapon;
    public GameObject skillsetlist;

    public void Start()
    {
        InstitateSkillUi();
    }

    public void ButtonPress()
    {
        DescriptionUi();
        SkillcontentActive();
    }


    public void DescriptionUi()
    {
        passivetext.text = weapon.passive_description;
        normalskilltext.text = weapon.normalskill_description;
        arreyskilltext.text = weapon.arreyskill_description;
    }

    public void SkillcontentActive()
    {
        foreach (Transform child in viewpoint.transform)
        {
            child.gameObject.SetActive(false);
        }
        curcontentnow.SetActive(true);
    }

    public void InstitateSkillUi()
    {
        GameObject curcontent = Instantiate(content, viewpoint.transform);
        curcontentnow = curcontent;

        foreach (Skill skill in weapon.skilllist)
        {
            GameObject currentskillUi = Instantiate(skillUi, curcontent.transform);
            currentskillUi.transform.GetChild(0).GetComponent<TMP_Text>().text = skill.skillmarkname;
            currentskillUi.GetComponent<skillselectUi>().attackcore = attackcore;
            currentskillUi.GetComponent<skillselectUi>().skill = skill;
            currentskillUi.GetComponent<skillselectUi>().skilllist = skillsetlist;
        }
        curcontentnow.SetActive(false);
    }
}
