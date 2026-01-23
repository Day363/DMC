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
    public GameObject face;
    public GameObject face2;
    public GameObject fbutton;
    public GameObject tocounsel;
    public GameObject itemgive;

    public string[] itemfailstring;
    public GameObject[] etchings;
    public List<GameObject> curetchings = new List<GameObject> { };
    public GameObject lazer2_small;
    public GameObject curlazer_;

    public void Start()
    {
        StartCoroutine(Cammove());
        gamemanager = battalemanager.Instance.gameObject;
        gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
        letterbox = battalemanager.Instance.letterbox;
        gamemanager = battalemanager.Instance.gameObject;
        player = battalemanager.Instance.player;
        battalemanager.Instance.currentenemy = gameObject;
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 13)
        {
            firstmet = true;
            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanager.GetComponent<CameraManager>().LookCounsel(campos);
            uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<PlayerMove>().Stop();
            GetComponent<filer_textflow>().yelling = false;
            gamemanager.GetComponent<chatmanager>().CallDialogue(0);
            
        }
    }

    public void Tocousel()
    {
        StartCoroutine(Toconsel_co());
    }

    IEnumerator Toconsel_co()
    {
        yield return new WaitForSeconds(3f);
        GameObject currentcounselmirror = Instantiate(tocounsel, new Vector3(transform.position.x, 30f, 0), Quaternion.identity);
        tocounselmanager currentcounselmirrortocounselmanager = currentcounselmirror.GetComponent<tocounselmanager>();
        currentcounselmirrortocounselmanager.player = player;
        currentcounselmirrortocounselmanager.cammanager = cammanager;
    }

    public void Fkill()
    {
        Destroy(fbutton);
        GetComponent<itemgive>().enabled = false;
        itemgive.GetComponent<itemgive>().available = false;
    }

    public void LetterBoxIn()
    {
        uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
    }

    public void LetterBoxOut()
    {
        uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
    }

    public void LookCam()
    {
        cammanager.GetComponent<CameraManager>().LookCounsel(campos);
        player.GetComponent<PlayerMove>().canmove = false;
        player.GetComponent<PlayerMove>().Stop();
    }

    public void LookPlayer()
    {
        cammanager.GetComponent<CameraManager>().LookPlayer();
        player.GetComponent<PlayerMove>().canmove = true;
    }

    public void StopYelling()
    {
        GetComponent<filer_textflow>().yelling = false;
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

    public void ItemSuccessFile()
    {
        campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
        cammanager.GetComponent<CameraManager>().LookCounsel(campos);
        uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        player.GetComponent<PlayerMove>().canmove = false;
        player.GetComponent<PlayerMove>().Stop();
        GetComponent<filer_textflow>().yelling = false;
        gamemanager.GetComponent<chatmanager>().CallDialogue(9);
    }

    public void ItemSuccessBulb()
    {
        campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
        cammanager.GetComponent<CameraManager>().LookCounsel(campos);
        uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        player.GetComponent<PlayerMove>().canmove = false;
        player.GetComponent<PlayerMove>().Stop();
        GetComponent<filer_textflow>().yelling = false;
        gamemanager.GetComponent<chatmanager>().CallDialogue(10);
    }

    public void ItemSuccessEtching()
    {
        campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
        cammanager.GetComponent<CameraManager>().LookCounsel(campos);
        uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        player.GetComponent<PlayerMove>().canmove = false;
        player.GetComponent<PlayerMove>().Stop();
        GetComponent<filer_textflow>().yelling = false;
        gamemanager.GetComponent<chatmanager>().CallDialogue(11);
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

    public void SpawnEtching()
    {
        StartCoroutine(SpawnEtching_co());
    }

    IEnumerator SpawnEtching_co()
    {
        float x = 92f;
        float y = 0.8f;

        GameObject curlazer = Instantiate(lazer2_small, new Vector3(92.5f, 0.8f, 0), Quaternion.Euler(0, -70, 0));
        curlazer.GetComponent<lazer2lookat>().look = false;
        curlazer.GetComponent<lazer2lookat>().isshoot = false;
        curlazer.GetComponent<lazer2lookat>().canwarning = false;
        curlazer.GetComponent<lazer2lookat>().canshoot = false;
        curlazer.GetComponent<lazer2lookat>().player = player;
        curlazer.GetComponent<lazer2lookat>().cammanager = cammanager;
        curlazer_ = curlazer;

        for (int i = 0; i <= 30; i++)
        {
            GameObject curetching = Instantiate(etchings[Random.Range(0, 24)], new Vector3(x, y, 0), Quaternion.identity);
            curetchings.Add(curetching);
            x = x - 1;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void Range()
    {
        curlazer_.GetComponent<lazer2lookat>().Charge();
    }

    public void Shoot()
    {
        curlazer_.GetComponent<lazer2lookat>().Shoot2();
        foreach (GameObject etching in curetchings)
        {
            etching.GetComponent<etchingdestroy>().DestroySelf();
        }
    }

    public void DestroyFace()
    {
        face.SetActive(false);
        face2.SetActive(false);
    }
}
