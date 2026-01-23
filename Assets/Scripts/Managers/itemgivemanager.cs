using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemgivemanager : MonoBehaviour
{
    public GameObject currentobject;
    public List<Rapport> corrects;

    public GameObject itemselectui;


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
        Debug.Log("½ÇÆÐ");
    }

    public void Filer_Sucssess(Rapport rapport)
    {
        Debug.Log(rapport.name);
        if (rapport == battalemanager.Instance.rapportdatas[4])
        {
            currentobject.GetComponent<filer_counsel>().ItemSuccessFile();
        }
        else if (rapport == battalemanager.Instance.rapportdatas[0])
        {
            currentobject.GetComponent<filer_counsel>().ItemSuccessBulb();
        }
        else if (rapport == battalemanager.Instance.rapportdatas[8])
        {
            currentobject.GetComponent<filer_counsel>().ItemSuccessEtching();
        }
    }

    public void Filer_Fail(Rapport rapport)
    {
        Debug.Log(rapport.name);
        currentobject.GetComponent<filer_counsel>().ItemFail();
    }
}
