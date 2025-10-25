using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class indexer0_counsel : MonoBehaviour
{
    public GameObject player;
    public GameObject attackcore;
    public GameObject cammanager;
    public GameObject gamemanager;
    public GameObject campos;
    public GameObject letterbox;
    public GameObject chat;
    public GameObject canvus;

    public bool firstmet;

    public void Start()
    {
        gamemanager.GetComponent<battalemanager>().currentenemy = gameObject;
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 10)
        {
            firstmet = true;
            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanager.GetComponent<CameraManager>().LookCounsel(campos);
            cammanager.GetComponent<CameraManager>().CinemachineInvalidateCache();
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<PlayerMove>().Stop();
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            gamemanager.GetComponent<chatmanager>().CallDialogue(5);
        }
    }
}
