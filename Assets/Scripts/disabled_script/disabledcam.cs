using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledcam : MonoBehaviour
{
    public GameObject cam;

    public void LookSelf()
    {
        cam.GetComponent<Animator>().SetBool("lookself", true);
    }

    public void LookSelfFalse()
    {
        cam.GetComponent<Animator>().SetBool("lookself", false);
    }

    public void Lookplayer()
    {
        cam.GetComponent<Animator>().SetBool("playerlook", true);
    }

    public void Deathattack()
    {
        cam.GetComponent<Animator>().SetBool("deathattack", true);
    }
}
