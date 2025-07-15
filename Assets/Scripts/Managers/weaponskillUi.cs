using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class weaponskillUi : MonoBehaviour
{
    public GameObject skilllistUi;
    public GameObject skillUi;
    public Weapon weapon;

    public void InstitateSkillUi()
    {
        for (int i = skilllistUi.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = skilllistUi.transform.GetChild(i);
            Destroy(child.gameObject); 
        }

        foreach (Skill skill in weapon.skilllist)
        {
            GameObject currentskillUi = Instantiate(skillUi, skilllistUi.transform);
            currentskillUi.transform.GetChild(0).GetComponent<TMP_Text>().text = skill.skillmarkname;
            currentskillUi.GetComponent<skillselectUi>().skill = skill;
        }
    }
}
