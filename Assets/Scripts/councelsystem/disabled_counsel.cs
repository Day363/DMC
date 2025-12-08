using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class disabled_counsel : MonoBehaviour
{
    public GameObject gamemanager;
    public GameObject cammanager;
    public bool firstmet;
    public GameObject player;
    public GameObject attackcore;
    public GameObject letterbox;
    public GameObject chat;
    public GameObject campos;
    public GameObject chatpre;
    public GameObject canvus;
    public GameObject letd;
    public GameObject cursor1;
    public GameObject cursor2;
    public GameObject balancebar;
    public GameObject playerstatus;
    public GameObject counselmirror;
    public GameObject background;
    public boss_hpbar bosshp;
    public GameObject counselcollier;
    public int cycleint_;
    public int cycleint;
    public GameObject cutsceneuidisappear;
    public GameObject itemgive;

    //Ä³½Ì
    PlayerMove playerPlayerMove;
    CameraManager cammanagerCameraManager;

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

    public void Start()
    {
        gamemanager = battalemanager.Instance.gameObject;

        boss_hpbar.Die += WhenDie;
        playerPlayerMove = player.GetComponent<PlayerMove>();
        cammanagerCameraManager = cammanager.GetComponent<CameraManager>();

        gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
        GetComponent<boss_hpbar>().canhit = false;
        balancebar.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 10)
        {
            firstmet = true;
            
            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanagerCameraManager.LookCounsel(campos);
            cammanagerCameraManager.CinemachineInvalidateCache();
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            playerPlayerMove.canmove = false;
            playerPlayerMove.Stop();
            gamemanager.GetComponent<chatmanager>().CallDialogue(1);

        }
    }

    public void Startcutscene()
    {
        cutsceneuidisappear.SetActive(false);
        itemgive.SetActive(false);
        player.GetComponent<playerstatus>().CutSceneUiDisappear();
        cursor1.SetActive(false);
        cursor2.SetActive(false);
        playerstatus.SetActive(false);
        playerPlayerMove.canmove = false;
        player.transform.position = new Vector3(-5.43f, player.transform.position.y, 1);
        letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        GetComponent<Animator>().SetTrigger("start");
    }

    public void Startcutscene2()
    {
        cutsceneuidisappear.SetActive(false);
        player.GetComponent<playerstatus>().CutSceneUiDisappear();
        cursor1.SetActive(false);
        cursor2.SetActive(false);
        playerstatus.SetActive(false);
        playerPlayerMove.canmove = false;
        player.transform.position = new Vector3(-5.43f, player.transform.position.y, 1);
        player.GetComponent<SpriteRenderer>().DOFade(0, 0f);
        letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        GetComponent<Animator>().SetTrigger("start");
    }

    public void Lookplayer()
    {
        GetComponent<boss_hpbar>().canhit = true;
        balancebar.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GetComponent<disabled_rushmanage>().canrotate = true;
        playerPlayerMove.canmove = true;
        cursor1.SetActive(true);
        cursor2.SetActive(true);
        playerstatus.SetActive(true);
        attackcore.GetComponent<attackcore>().SetCronometer();
        GetComponent<cutscenemanager>().cameraset = false;
        cammanagerCameraManager.LookPlayer();
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
        cutsceneuidisappear.SetActive(true);
        player.GetComponent<playerstatus>().CutSceneUiAppear();
    }

    public void LookPlayer2()
    {
        player.GetComponent<SpriteRenderer>().DOFade(1, 0f);
        playerPlayerMove.canmove = true;
        cursor1.SetActive(true);
        cursor2.SetActive(true);
        playerstatus.SetActive(true);
        GetComponent<cutscenemanager>().cameraset = false;
        cammanagerCameraManager.LookPlayer();
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
    }

    public void SpawnText()
    {
        StartCoroutine(SpawnText_co());

    }

    IEnumerator SpawnText_co()
    {
        GameObject currentchat = Instantiate(chatpre, canvus.transform);
        currentchat.GetComponent<RectTransform>().localPosition = new Vector3(0, 30, 0);
        letd = currentchat;
        currentchat.transform.localScale = new Vector3(2.4f, 2.4f, 2.4f);
        TMP_Text tmp = currentchat.transform.GetChild(0).GetComponent<TMP_Text>();
        TextEffectManager shaker = tmp.GetComponent<TextEffectManager>();
        shaker.SetText("<shake=0.5,20>Á¦¹ß Á×¿©</shake>");
        tmp.color = new Color(0.5f, 0, 0, 1f);
        tmp.ForceMeshUpdate();
        int totalvisible = tmp.textInfo.characterCount;
        tmp.maxVisibleCharacters = 0;
        int visible = 0;
        while (visible < totalvisible)
        {
            visible++;
            tmp.maxVisibleCharacters = visible;
            if (shaker != null)
                shaker.CheckEvents(visible);
            float wait = 0.3f;
            yield return new WaitForSeconds(wait);
        }
    }

    public void letdd()
    {
        Destroy(letd);
    }

    public void ResetAll()
    {
        gamemanager.SetActive(false);
    }

    public void WhenDie()
    {

    }

    public void End()
    {
        gameObject.
        gameObject.SetActive(false);
    }

    public void SpawnCounsel()
    {

        StartCoroutine(SpawnCounsel_co());
    }

    IEnumerator SpawnCounsel_co()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject currentcounselmirror = Instantiate(counselmirror, new Vector3(transform.position.x, 30f, 0), Quaternion.identity);
        tocounselmanager currentcounselmirrortocounselmanager = currentcounselmirror.GetComponent<tocounselmanager>();
        currentcounselmirrortocounselmanager.player = player;
        currentcounselmirrortocounselmanager.cammanager = cammanager;
    }
}
