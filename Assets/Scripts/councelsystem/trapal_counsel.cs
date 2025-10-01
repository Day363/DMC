using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_counsel : MonoBehaviour
{
    public GameObject player;
    public GameObject cammanager;
    public GameObject gamemanager;
    public GameObject campos;
    public GameObject letterbox;
    public GameObject chat;

    public bool firstmet;

    PlayerMove playerPlayerMove;
    CameraManager cammanagerCameraManager;

    public void Start()
    {
        playerPlayerMove = player.GetComponent<PlayerMove>();
        cammanagerCameraManager = cammanager.GetComponent<CameraManager>();
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 10)
        {
            firstmet = true;

            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanagerCameraManager.LookCounsel(campos);
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            playerPlayerMove.canmove = false;
            playerPlayerMove.Stop();
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            gamemanager.GetComponent<chatmanager>().CallDialogue(3);

        }
    }
}
