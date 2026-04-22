using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class normalskilluibutton : MonoBehaviour
{
    public GameObject skillbutton;

    public void SkillSelect()
    {
        soundmanager.instance.SoundPlay("click2");
        skillbutton.GetComponent<skillselectUi>().SkillAddToList();
        skillbutton.GetComponent<skillselectUi>().descriptionUi = gameObject;
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0.1f);
    }
}
