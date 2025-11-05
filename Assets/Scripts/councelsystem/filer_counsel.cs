using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using TMPro;

public class filer_counsel : MonoBehaviour
{
    public GameObject counselcam;
    public bool firstmet;
    public GameObject letterbox;
    public GameObject gamemanager;
    public GameObject chat;
    public GameObject cammanager;
    public GameObject player;
    public GameObject campos;

    public string[] itemfailstring;

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
            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanager.GetComponent<CameraManager>().LookCounsel(campos);
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

    public void ItemFail()
    {
        int i = Random.Range(0, itemfailstring.Length);
        StartCoroutine(Chat(itemfailstring[i]));
    }

    IEnumerator Chat(string dialogue)
    {
        chat.SetActive(true);
        chat.transform.localScale = new Vector3(2.4f, 0, 2.4f);
        TMP_Text tmp = chat.transform.GetChild(0).GetComponent<TMP_Text>();
        TextEffectManager shaker = tmp.GetComponent<TextEffectManager>();
        if (shaker != null)
            shaker.SetText(dialogue);
        else
            tmp.text = dialogue;
        tmp.ForceMeshUpdate();
        int totalvisible = tmp.textInfo.characterCount;
        tmp.maxVisibleCharacters = 0;
        int visible = 0;

        chat.transform.DOScaleY(2.4f, 0.7f);
        yield return new WaitForSeconds(1f);

        while (visible < totalvisible)
        {
            visible++;
            tmp.maxVisibleCharacters = visible;

            if (shaker != null)
                shaker.CheckEvents(visible);

            char c = GetPrintedCharAtIndex(tmp, visible - 1);
            float wait = 0.05f;
            if (IsPunctuation(c)) wait += 0.25f;

            yield return new WaitForSeconds(wait);
        }
        chat.transform.DOScaleY(0, 0.7f);
        yield return new WaitForSeconds(0.8f);
        chat.SetActive(false);
    }

    char GetPrintedCharAtIndex(TMP_Text t, int visibleIndex)
    {
        if (visibleIndex < 0 || visibleIndex >= t.textInfo.characterCount) return '\0';
        var ci = t.textInfo.characterInfo[visibleIndex];
        return ci.character;
    }

    bool IsPunctuation(char c)
    {
        return c == '.' || c == ',' || c == '!' || c == '?' || c == ';' || c == ':' || c == '¡¦';
    }
}
