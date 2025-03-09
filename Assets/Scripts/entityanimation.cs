using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityanimation : MonoBehaviour
{
    public string[] parameterlist;

    public void Animationtrigger(int p)
    {
        GetComponent<Animator>().SetBool(parameterlist[p], true);
    }
}
