using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class itemgivebutton : MonoBehaviour
{
    public GameObject gamemanager;
    public Rapport currentrapport;
    public itemgive itemgivecom;

    public void Onclick()
    {
        itemgivecom.Uiclose();

        var manager = gamemanager.GetComponent<itemgivemanager>();

        if (manager.corrects.Contains(currentrapport))
        {
            manager.Sucssess(currentrapport);
        }
        else
        {
            manager.Fail(currentrapport);
        }
    }
}
