using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemgivebutton : MonoBehaviour
{
    public GameObject gamemanager;
    public Rapport currentrapport;
    public itemgive itemgivecom;

    public void Onclick()
    {
        foreach (Rapport rapport in gamemanager.GetComponent<itemgivemanager>().corrects)
        {
            if (rapport == currentrapport)
            {
                itemgivecom.Uiclose();
                gamemanager.GetComponent<itemgivemanager>().Sucssess(rapport);
                break;
            }
            else
            {
                itemgivecom.Uiclose();
                gamemanager.GetComponent<itemgivemanager>().Fail(rapport);
                break;
            }
        }
        
    }
}
