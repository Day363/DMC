using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class disabled_counsel : MonoBehaviour
{
    public GameObject gamemanager;
    public GameObject cammanager;
    public bool firstmet;
    public GameObject player;
    public GameObject letterbox;
    public GameObject chat;
    public GameObject campos;
    public GameObject chatpre;
    public GameObject canvus;
    public GameObject letd;

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

    public void Startcutscene()
    {
        player.transform.position = new Vector3(0, player.transform.position.y, 1);
        letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        GetComponent<Animator>().SetTrigger("start");
    }

    public void Lookplayer()
    {
        StartCoroutine(Lookplayer_co());
    }

    IEnumerator Lookplayer_co()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<cutscenemanager>().cameraset = false;
        cammanager.GetComponent<CameraManager>().LookPlayer();
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
        shaker.SetText("<shake=1,13>Á¦¹ß Á×¿©</shake>");
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
}
