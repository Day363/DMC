using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerslashtest : MonoBehaviour
{
    public GameObject[] slashs;
    public GameObject[] specials;
    public GameObject attackcore;
    public GameObject readyeffect;
    public bool canattack = true;
    public int slashnumber = 0;
    public int stack = 0;
    public bool isdelay = true;
    public bool whileeffect = false;
    public int readyparam = 0;
    
    public void Update()
    {
        if (stack == 3)
        {
            whileeffect = true;
            specials[0].SetActive(true);
            attackcore.GetComponent<attackcore>().isdelay = false;
            stack = 0;
            readyparam = 0;
            readyeffect.GetComponent<Animator>().SetInteger("ready", 0);
        }

    }

    public void PassiveCycle()
    {
        if (attackcore.GetComponent<attackcore>().attacklist.Count >= 8)
        {
            if (!whileeffect)
            {
                stack = stack + 1;
                readyparam = readyparam + 1;
                readyeffect.GetComponent<Animator>().SetInteger("ready", readyparam);
            }
        }
    }
}


