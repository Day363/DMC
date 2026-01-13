using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class arreyskillui : MonoBehaviour
{
    public cynthskill cynthskill;
    public standbyskill standbyskill;

    public GameObject skillArreyViewport;

    public List<GameObject> selectedskilluis = new List<GameObject> { };

    public List<string> arreyskills = new List<string> { };
    public  List<GameObject> normalskillUIs = new List<GameObject> { };
    public List<string> selectedskilluistring = new List<string> { };

    public void AutoArrey()
    {
        selectedskilluis.Clear();
        selectedskilluistring.Clear();
        normalskillUIs.Clear();

        if (cynthskill != null)
        {
            arreyskills = cynthskill.condition;
        }
        else if (standbyskill != null)
        {
            arreyskills = standbyskill.skillarreyto;
        }

        foreach (Transform contents in skillArreyViewport.transform)
        {
            foreach (Transform skilluis in contents.transform)
            {
                normalskillUIs.Add(skilluis.gameObject);
            }
        }

        foreach (string skillstring in arreyskills)
        {
            foreach (GameObject normalskillui in normalskillUIs)
            {
                if (normalskillui.GetComponent<skillselectUi>().isselect == false && normalskillui.GetComponent<skillselectUi>().skill.skillmarkname == skillstring)
                {
                    selectedskilluis.Add(normalskillui);
                    break;
                }
            }
        }

        foreach (GameObject uiobject in selectedskilluis)
        {
            selectedskilluistring.Add(uiobject.GetComponent<skillselectUi>().skill.skillmarkname);
        }

        if (arreyskills.SequenceEqual(selectedskilluistring))
        {
            foreach (GameObject uiobject in selectedskilluis)
            {
                uiobject.GetComponent<skillselectUi>().SkillAddToList();
            }
        }
        else
        {
            selectedskilluis.Clear();
            Debug.Log("±×·¸°ÔµÆ´Ù");
        }

        selectedskilluistring.Clear();
        normalskillUIs.Clear();
    }
}
