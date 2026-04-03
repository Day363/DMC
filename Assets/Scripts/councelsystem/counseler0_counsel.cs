using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counseler0_counsel : MonoBehaviour
{
    public bool firstmet = false;
    public GameObject player;
    public GameObject gamemanager;
    public GameObject chat;
    public GameObject campos;
    public CameraManager cammanagerCameraManager;

    public PlayerMove playerPlayerMove;

    public void Start()
    {
        boss_hpbar.OnCollusion += Tutorial3;

        battalemanager.Instance.currentenemy = gameObject;
        player = battalemanager.Instance.player;
        gamemanager = battalemanager.Instance.gameObject;
        playerPlayerMove = player.GetComponent<PlayerMove>();
        cammanagerCameraManager.GetComponent<Animator>().SetTrigger("playercam");
    }


    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 10)
        {
            firstmet = true;
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanagerCameraManager.LookCounsel(campos);
            uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            playerPlayerMove.canmove = false;
            playerPlayerMove.Stop();
            gamemanager.GetComponent<chatmanager>().CallDialogue(13);
            GetComponent<Animator>().SetTrigger("meet");
        }
    }

    public void Tutorial3()
    {
        if (battalemanager.Instance.cronometer.GetComponent<cronometer_script>().tutorial)
        {
            Time.timeScale = 0f;
            uimanager.Instance.TutorialGo3();

            battalemanager.Instance.cronometer.GetComponent<cronometer_script>().tutorial = false;
        }
    }
}
