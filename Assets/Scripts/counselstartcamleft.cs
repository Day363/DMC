using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counselstartcamleft : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public GameObject slashcore;
    public float distance;

    private void Start()
    {
        cam.SetActive(true);
    }
    private void Update()
    {
        if (transform.position.x - player.transform.position.x > distance)
        {
            GetComponent<counseltext_test>().counselling = true;
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<Animator>().ResetTrigger("running");
            player.GetComponent<Animator>().SetBool("idle", true);
            cam.GetComponent<Animator>().SetBool("counsel", true);
            cam.GetComponent<Animator>().SetBool("playerlook", false);
            slashcore.GetComponent<attackcore>().canattack = false;
        }
    }
}
