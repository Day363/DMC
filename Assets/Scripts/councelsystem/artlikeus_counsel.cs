using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artlikeus_counsel : MonoBehaviour
{
    public GameObject counselcam;
    public bool firstmet;
    public GameObject letterbox;
    public GameObject gamemanager;
    public GameObject chat;
    public GameObject cammanager;
    public GameObject player;

    public void Start()
    {
        gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 13)
        {
            firstmet = true;
            counselcam.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanager.GetComponent<CameraManager>().LookCounsel(counselcam);
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<PlayerMove>().Stop();
            gamemanager.GetComponent<chatmanager>().CallDialogue(9);

        }
    }
}
