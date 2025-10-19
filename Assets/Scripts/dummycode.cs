using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummycode : MonoBehaviour
{
    public GameObject gamemanager;

    private void Start()
    {
        gamemanager.GetComponent<battalemanager>().currentenemy = gameObject;
        InvokeRepeating("Cycle", 0, 3);
 
    }

    public void Cycle()
    {
        GetComponent<boss_hpbar>().PassiveFloatReset();
        GetComponent<boss_hpbar>().CycleEnd();
        GetComponent<boss_hpbar>().CycleStart();
    }
}
