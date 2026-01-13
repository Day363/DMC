using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillbuttondisappear : MonoBehaviour
{
    public GameObject attackcore;
    public Transform skilllist;
    public Skill currentskill;

    public GameObject button;

    private void Start()
    {
        skilllist = transform.parent;
    }

    public void ButtonDisappear()
    {
        button.GetComponent<skillselectUi>().UnSelected();
        skilllist.GetComponent<skillselectarrey>().original_skilllist.Remove(currentskill);
        attackcore.GetComponent<attackcore>().attacklist_original.Remove(currentskill);
        attackcore.GetComponent<attackcore>().SkillarreyUi();
        Destroy(gameObject);
    }

    public void ButtonDisappearWhenUiReset()
    {
        button.GetComponent<skillselectUi>().UnSelected();
        skilllist.GetComponent<skillselectarrey>().original_skilllist.Remove(currentskill);
        Destroy(gameObject);
    }
}
