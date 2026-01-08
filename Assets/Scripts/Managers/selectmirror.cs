using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using Cinemachine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class selectmirror : MonoBehaviour
{
    public GameObject player;
    public GameObject fadeout;
    public string scenename;

    public string[] whispers;
    public GameObject chatpre;
    public GameObject canvus;
    public bool whisper;
    public Coroutine repeatRoutine;
    public Vector2 whisperrangeX;
    public Vector2 whisperrangeY;

    public GameObject cammanager;
    public GameObject attackcore;

    public GameObject fbutton;
    public bool selectactive;

    public void Start()
    {
        player = battalemanager.Instance.player;
        fadeout = battalemanager.Instance.fadeout;
        cammanager = battalemanager.Instance.cameramanager;
        attackcore = battalemanager.Instance.attackcore;
    }

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
            StartCoroutine(Fadeout());
            fadeout.GetComponent<Image>().DOFade(1, 1.5f);
        }
    }

    IEnumerator Fadeout()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        yield return new WaitForSeconds(2f);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Additive);
        yield return loadOperation;

        Scene newScene = SceneManager.GetSceneByName(scenename);
        SceneManager.SetActiveScene(newScene);

        uimanager.Instance.ResetUi();

        yield return SceneManager.UnloadSceneAsync(currentSceneName);
    }
}
