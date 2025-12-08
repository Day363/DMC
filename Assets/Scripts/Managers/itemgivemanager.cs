using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemgivemanager : MonoBehaviour
{
    public GameObject currentobject;
    public Rapport[] corrects;

    public GameObject itemselectui;

    public Rapport file;
    public Rapport bulb;

    public void Start()
    {
        itemselectui = battalemanager.Instance.itemgiveui;
    }

    public void Sucssess(Rapport rapport)
    {
        if (currentobject.name == "disabled")
        {
            Disabled_Sucssess();
        }
        if (currentobject.name == "filer")
        {
            Filer_Sucssess(rapport);
        }
    }


    public void Fail(Rapport rapport)
    {
        if (currentobject.name == "disabled")
        {
            Disabled_Fail();
        }
        if (currentobject.name == "filer")
        {
            Filer_Fail(rapport);
        }
    }

    public void Disabled_Sucssess()
    {
        currentobject.GetComponent<disabled_counsel>().Startcutscene();
    }

    public void Disabled_Fail()
    {
        Debug.Log("실패");
    }

    public void Filer_Sucssess(Rapport rapport)
    {
        if (rapport == file)
        {

        }
        else if (rapport == bulb)
        {

        }
    }

    public void Filer_Fail(Rapport rapport)
    {
        Debug.Log("실패");
        currentobject.GetComponent<filer_counsel>().ItemFail();
    }
}
