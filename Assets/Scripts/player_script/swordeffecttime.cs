using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordeffecttime : MonoBehaviour
{
    public GameObject slashcore;
    public GameObject attackcore;
    public GameObject self;
    public int count = 0;
    public int cool;
    void Count()
    {
        count = count + 1;
    }

    private void Update()
    {
        if (count >= cool)
        {
            count = 0;
            slashcore.GetComponent<playerslashtest>().whileeffect = false;
            attackcore.GetComponent<attackcore>().isdelay = true;
            self.SetActive(false);
        }
    }
}
