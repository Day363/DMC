using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledcounselmanager : MonoBehaviour
{
    public GameObject player;
    public GameObject disabledcam;
    public GameObject slashcore;
    public float distance;

    private void Start()
    {
        disabledcam.SetActive(true);
    }
    private void Update()
    {
        if (transform.position.x - player.transform.position.x < distance)
        {
            GetComponent<counseltext_test>().counselling = true;
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<Animator>().ResetTrigger("running");
            player.GetComponent<Animator>().SetBool("idle", true);
            disabledcam.GetComponent<Animator>().SetBool("counsel", true);
            disabledcam.GetComponent<Animator>().SetBool("playerlook", false);
            slashcore.GetComponent<playerslashtest>().canattack = false;
        }
    }
}
