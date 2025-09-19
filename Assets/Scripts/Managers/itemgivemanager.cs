using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemgivemanager : MonoBehaviour
{
    public GameObject currentobject;
    public Rapport[] corrects;

    public GameObject itemselectui;

    public GameObject disabled;

    public void Sucssess()
    {
        if (currentobject == disabled)
        {
            Disabled_Sucssess();
        }
    }


    public void Fail()
    {
        if (currentobject == disabled)
        {
            Disabled_Fail();
        }
    }

    public void Disabled_Sucssess()
    {
        disabled.GetComponent<disabled_counsel>().Startcutscene();
    }

    public void Disabled_Fail()
    {
        Debug.Log("½ÇÆÐ");
    }
}
