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
    public bool tutorial3 = true;
    public GameObject cutscenemanager;
    public GameObject battleui;

    public void Start()
    {
        boss_hpbar.OnCollusion += Tutorial3;
        playerstatus.OnHit += Cutscene;

        
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
        if (battalemanager.Instance.cronometer.GetComponent<cronometer_script>().tutorial && tutorial3)
        {
            tutorial3 = false;
            battalemanager.Instance.gameObject.GetComponent<PauseManager>().ispause = true;
            uimanager.Instance.TutorialGo3();

            battalemanager.Instance.cronometer.GetComponent<cronometer_script>().tutorial = false;
        }
        else if (GetComponent<boss_hpbar>().maxhealth / 2 > GetComponent<boss_hpbar>().currenthealth)
        {
            uimanager.Instance.CloseFightUi();
            cutscenemanager.SetActive(true);
            player.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void Cutscene(GameObject enemy, float dam)
    {
        if (battalemanager.Instance.player.GetComponent<playerstatus>().lifecount == 1 && battalemanager.Instance.player.GetComponent<playerstatus>().currentbalance > battalemanager.Instance.player.GetComponent<playerstatus>().maxbalance / 2)
        {
            uimanager.Instance.CloseFightUi();
            cutscenemanager.SetActive(true);
            player.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
}
