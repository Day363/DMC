using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class filermanage : MonoBehaviour
{
    public GameObject background;
    public GameObject player;
    public GameObject filercamera;
    public float time;

    private void Start()
    {
        background.SetActive(true);
        player.GetComponent<PlayerMove>().canmove = false;
    }

    private void Update()
    {
        time = time + 1 * Time.deltaTime;
        if (time > 1 && time < 2)
        {
            background.GetComponent<Animator>().SetBool("start", true);
        }

        if (time > 2 && time < 3)
        {
            filercamera.GetComponent<Animator>().SetBool("filer", true);
        }

        if (time > 3 && time < 4)
        {
            
        }

        if (time > 4.5 && time < 5)
        {
            filercamera.GetComponent<Animator>().SetBool("filer", false);
            player.GetComponent<PlayerMove>().canmove = true;
        }

        if (transform.position.x - player.transform.position.x < 15)
        {
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<Animator>().ResetTrigger("running");
            player.GetComponent<Animator>().SetBool("idle", true);
            filercamera.GetComponent<Animator>().SetBool("lookplayer", false);
            filercamera.GetComponent<Animator>().SetBool("counsel", true);
        }
    }
}
