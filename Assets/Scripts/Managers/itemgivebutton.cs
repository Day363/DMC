using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemgivebutton : MonoBehaviour
{
    public GameObject gamemanager;
    public Rapport currentrapport;

    public void Onclick()
    {
        foreach (Rapport rapport in gamemanager.GetComponent<itemgivemanager>().corrects)
        {
            if (rapport == currentrapport)
            {
                gamemanager.GetComponent<itemgivemanager>().Sucssess();
                break;
            }
            else
            {
                gamemanager.GetComponent<itemgivemanager>().Fail();
                break;
            }
        }
        
    }
}
