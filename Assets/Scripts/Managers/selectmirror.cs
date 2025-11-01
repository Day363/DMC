using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using Cinemachine;
using TMPro;
using DG.Tweening;

public class selectmirror : MonoBehaviour
{
    public GameObject player;

    public Vector3 wheretospwan;

    public string[] whispers;
    public GameObject chatpre;
    public GameObject canvus;
    public bool whisper;
    public Coroutine repeatRoutine;
    public Vector2 whisperrangeX;
    public Vector2 whisperrangeY;

    public GameObject cammanager;
    public GameObject attackcore;
    public bool widercam;
    public Collider2D widecollider;

    public GameObject fbutton;
    public bool selectactive;

    public GameObject background0;
    public GameObject wall;
    public GameObject[] backgrounds;
    public GameObject client;

    public GameObject thisPack;

    public Light2D worldlight;
    public float worldlightint;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fbutton.SetActive(true);
            selectactive = true;
            whisper = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fbutton.SetActive(false);
            selectactive = false;
            whisper = false;
        }

    }

    public void FixedUpdate()
    {
        if (whisper && repeatRoutine == null)
        {
            repeatRoutine = StartCoroutine(CallFunctionRepeatedly());
        }
        if (!whisper && repeatRoutine != null)
        {
            StopCoroutine(repeatRoutine);
            repeatRoutine = null;
        }
    }

    IEnumerator CallFunctionRepeatedly()
    {
        while (true)
        {
            StartCoroutine(Whispering_co_co());         
            yield return new WaitForSeconds(Random.Range(2f, 5f)); 
        }
    }


    IEnumerator Whispering_co_co()
    {
        GameObject currentchat = Instantiate(chatpre, canvus.transform);
        currentchat.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        currentchat.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(whisperrangeX.x, whisperrangeX.y), Random.Range(whisperrangeY.x, whisperrangeY.y), 0);
        currentchat.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));
        currentchat.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        TMP_Text tmp = currentchat.transform.GetChild(0).GetComponent<TMP_Text>();
        TextEffectManager shaker = tmp.GetComponent<TextEffectManager>();
        shaker.SetText(whispers[Random.Range(0, whispers.Length)]);
        tmp.fontSize = Random.Range(2f, 4f);
        tmp.color = new Color(0.5f, 0, 0, Random.Range(0.2f, 0.5f));
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
            float wait = 0.15f;
            yield return new WaitForSeconds(wait);
        }
        yield return new WaitForSeconds(1.5f);
        tmp.DOFade(0, 1f);
        yield return new WaitForSeconds(1.5f);
        Destroy(currentchat);
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("fbutton") && selectactive)
        {
            player.transform.position = new Vector3(2.5f, -0.4f, 0);

            foreach (GameObject background in backgrounds)
            {
                background.SetActive(true);
            }

            player.transform.position = wheretospwan;
            cammanager.transform.position = player.transform.position;

            worldlight.intensity = worldlightint;

            if (widercam)
            {
                cammanager.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = widecollider;
                cammanager.GetComponent<CinemachineConfiner2D>().InvalidateCache();

            }

            if (wall  != null)
            {
                wall.SetActive(false);
            }
            

            background0.SetActive(false);
            
            client.SetActive(true);
            foreach (Transform mirror in thisPack.transform)
            {
                mirror.gameObject.SetActive(false);
            }

        }

    }
}
