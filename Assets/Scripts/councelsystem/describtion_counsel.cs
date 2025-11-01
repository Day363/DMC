using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class describtion_counsel : MonoBehaviour
{
    public bool firstmet;
    public GameObject letterbox;
    public GameObject gamemanager;
    public GameObject chat;
    public GameObject cammanager;
    public GameObject player;
    public GameObject campos;

    public GameObject rapport;

    public void Start()
    {
        gamemanager.GetComponent<battalemanager>().currentenemy = gameObject;
        counselfunctionmanager.Onsuccess += Success;
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 7)
        {
            firstmet = true;
            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanager.GetComponent<CameraManager>().LookCounsel(campos);
            cammanager.GetComponent<CameraManager>().CinemachineInvalidateCache();
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<PlayerMove>().Stop();
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            gamemanager.GetComponent<chatmanager>().CallDialogue(6);
        }
    }

    public void Success()
    {
        GameObject currentrapport = Instantiate(rapport, transform.position, Quaternion.identity);
        currentrapport.GetComponent<Rigidbody2D>().AddForce(new Vector2(-9, 5), ForceMode2D.Force);
    }
}
