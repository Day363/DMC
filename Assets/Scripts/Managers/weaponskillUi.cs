using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class weaponskillUi : MonoBehaviour
{
    public TMP_Text passivetext;
    public TMP_Text normalskilltext;
    public TMP_Text arreyskilltext;

    public GameObject attackcore;
    public GameObject skilllistUi;
    public GameObject skillUi;
    public Weapon weapon;
    public GameObject skillsetlist;

    public void InstitateSkillUi()
    {
        passivetext.text = weapon.passive_description;
        normalskilltext.text = weapon.normalskill_description;
        arreyskilltext.text = weapon.arreyskill_description;

        for (int i = skilllistUi.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = skilllistUi.transform.GetChild(i);
            Destroy(child.gameObject); 
        }

        foreach (Skill skill in weapon.skilllist)
        {
            GameObject currentskillUi = Instantiate(skillUi, skilllistUi.transform);
            currentskillUi.transform.GetChild(0).GetComponent<TMP_Text>().text = skill.skillmarkname;
            currentskillUi.GetComponent<skillselectUi>().attackcore = attackcore;
            currentskillUi.GetComponent<skillselectUi>().skill = skill;
            currentskillUi.GetComponent<skillselectUi>().skilllist = skillsetlist;
        }
    }
}
