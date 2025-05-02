using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_script : MonoBehaviour
{
    public GameObject cam;
    public GameObject rainmanager;
    public GameObject[] rainshader;
    public string[] weapons;
    [SerializeField]
    public bool testbool = false;

    public void FixedUpdate()
    {
        if (testbool)
        {
            GetComponent<Animator>().SetBool("rain", true);
        }
    }

    public void LookClinet()
    {
        cam.GetComponent<Animator>().SetBool("lookplayer", false);
        cam.GetComponent<Animator>().SetBool("lookclient", true);
    }

    public void Startrain()
    {
        rainmanager.GetComponent<indexer0_rainmanager>().DoRain();
    }
}
