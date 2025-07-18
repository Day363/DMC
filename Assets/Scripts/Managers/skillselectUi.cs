using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class skillselectUi : MonoBehaviour
{
    public bool isselect = false;

    public GameObject attackcore;
    public Skill skill;
    public GameObject skilllist;
    public GameObject skillbutton;

    public void SkillAddToList()
    {
        if (isselect == false)
        {
            Selected();
        }
    }

    public void Selected()
    {
        isselect = true;

        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0.2f);

        attackcore.GetComponent<attackcore>().attacklist_original.Add(skill);
        GameObject curskillbutton = Instantiate(skillbutton, skilllist.transform);
        skilllist.GetComponent<skillselectarrey>().original_skilllist.Add(skill);
        curskillbutton.GetComponent<skillbuttondisappear>().currentskill = skill;
        curskillbutton.GetComponent<skillbuttondisappear>().attackcore = attackcore;
        attackcore.GetComponent<attackcore>().SkillarreyUi();

        if (skill.normalskill)
        {
            curskillbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = $"[{skill.skillmarkname}]->";
        }
        else if (skill.enforceskill)
        {
            if (skill.amalagam)
            {
                curskillbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = $"[[     ]{skill.skillmarkname}[     ]]->";
            }
            else
            {
                curskillbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = $"[{skill.skillmarkname}[     ]]->";
            }

        }


        curskillbutton.GetComponent<skillbuttondisappear>().button = gameObject;
        curskillbutton.GetComponent<skillbuttonsize>().ButtonSize();
    }

    public void UnSelected()
    {
        isselect = false;

        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1.4655f);
    }
}
