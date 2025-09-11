using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class filer_counsel : MonoBehaviour
{
    public bool firstmet;
    public GameObject letterbox;
    public GameObject gamemanager;
    public GameObject chat;
    public GameObject cammanager;
    public GameObject player;
    public GameObject campos;

    public void Start()
    {
        StartCoroutine(Cammove());
        gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 13)
        {
            firstmet = true;
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<PlayerMove>().Stop();
            GetComponent<filer_textflow>().yelling = false;
            gamemanager.GetComponent<chatmanager>().CallDialogue(0);
            
        }
    }

    IEnumerator Cammove()
    {
        cammanager.GetComponent<CameraManager>().fuckcinemachine = true;
        player.GetComponent<PlayerMove>().canmove = false;
        campos.transform.position = player.transform.position;
        cammanager.GetComponent<CameraManager>().LookCounsel(campos);
        campos.transform.DOMove(transform.position, 1.5f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(3f);
        campos.transform.DOMove(player.transform.position, 0.5f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.6f);
        cammanager.GetComponent<CameraManager>().LookPlayer();
        player.GetComponent<PlayerMove>().canmove = true;
        cammanager.GetComponent<CameraManager>().fuckcinemachine = false;
    }
}
