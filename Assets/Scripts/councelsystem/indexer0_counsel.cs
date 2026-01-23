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
    public GameObject line;

    public bool firstmet;

    public boss_hpbar bosshp;
    public int cycleint_;
    public int cycleint;

    public GameObject portal;
    

    public void Start()
    {
        gamemanager = battalemanager.Instance.gameObject;

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
            uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<PlayerMove>().Stop();
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            gamemanager.GetComponent<chatmanager>().CallDialogue(5);
            line.GetComponent<indexer_line_core>().LookStart();
        }
    }

    public void ToleranceDown()
    {
        GetComponent<boss_hpbar>().slashtolerance = 0.1f;
        GetComponent<boss_hpbar>().blowtolerance = 0.1f;
        GetComponent<boss_hpbar>().penetratetolerance = 0.1f;
    }

    public void ToleranceDownEnd()
    {
        GetComponent<boss_hpbar>().slashtolerance = GetComponent<boss_hpbar>().slashtoleranceCore;
        GetComponent<boss_hpbar>().blowtolerance = GetComponent<boss_hpbar>().blowtoleranceCore;
        GetComponent<boss_hpbar>().penetratetolerance = GetComponent<boss_hpbar>().penetratetoleranceCore;
    }

    public void AttackEnd()
    {
        cycleint_++;
        if (cycleint_ >= cycleint)
        {
            bosshp.CycleEnd();
            bosshp.CycleStart();
            cycleint_ = 0;
        }
    }

    
    public void DieDialogue()
    {
        cammanager.GetComponent<CameraManager>().LookEnemy();
        cammanager.GetComponent<CameraManager>().CinemachineInvalidateCache();
        letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        player.GetComponent<PlayerMove>().canmove = false;
        player.GetComponent<PlayerMove>().Stop();
        gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
        gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
        gamemanager.GetComponent<chatmanager>().CallDialogue(8);
    }

    public void DieDisapper()
    {
        StartCoroutine(DieDisapper_co());

    }

    IEnumerator DieDisapper_co()
    {
        GameObject curportal = Instantiate(portal, transform.position, Quaternion.identity);
        curportal.transform.position = new Vector3(0, 0, 0);
        curportal.transform.localScale = new Vector3(0, 0, 0);
        curportal.transform.DOScale(4, 0.5f).SetEase(Ease.OutQuart);

        yield return new WaitForSeconds(1.6f);
        GetComponent<SpriteRenderer>().DOFade(0, 0.01f);

        curportal.transform.DOScale(0, 0.2f).SetEase(Ease.OutQuart);
    }
}
