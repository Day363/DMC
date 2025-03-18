using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashtest : MonoBehaviour
{
    public GameObject self;
    public GameObject slashcore;

    public void CantAttack()
    {
        if (slashcore.GetComponent<attackcore>().isdelay)
        {
            slashcore.GetComponent<attackcore>().canattack = false;
        }
    }

    public void SetEnd()
    {
        slashcore.GetComponent<attackcore>().canattack = true;
        self.SetActive(false);
        
    }

}
