using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinemachinecontrol : MonoBehaviour
{
    public string[] parameterlist;
    public GameObject cinemachine;

    public void Cinemachinetrigger(int p)
    {
        cinemachine.GetComponent<Animator>().SetBool(parameterlist[p], true);
    }

    public void Cinemachinetriggern(int p)
    {
        cinemachine.GetComponent<Animator>().SetBool(parameterlist[p], false);
    }
}
