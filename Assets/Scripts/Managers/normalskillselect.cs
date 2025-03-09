using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalskillselect : MonoBehaviour
{
    public GameObject skilldisplay;
    public GameObject skillset;
    public Normalskill selectedskill;

    public void SkillSelect()
    {
        skillset.GetComponent<playercomboscipt>().playerskillset.Add(selectedskill);
    }
}
