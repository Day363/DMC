using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabled_counsel : MonoBehaviour
{
    public GameObject gamemanager;
    public GameObject cammanager;
    public bool firstmet;
    public GameObject player;
    public GameObject letterbox;
    public GameObject chat;
    public GameObject campos;

    public void Start()
    {
        gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 10)
        {
            firstmet = true;
            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanager.GetComponent<CameraManager>().LookCounsel(campos);
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<PlayerMove>().Stop();
            gamemanager.GetComponent<chatmanager>().CallDialogue(1);

        }
    }
}
