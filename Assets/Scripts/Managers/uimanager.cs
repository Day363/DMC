using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class uimanager : MonoBehaviour
{
    public static uimanager Instance;
    public static event Action OnUIReady;

    [Header("activewhenbattlestart")]
    public GameObject[] activewhenbattlestart;
    [Header("playerstatus")]
    public GameObject skillwaitprefap;
    public GameObject defense;
    public GameObject bullet;
    public GameObject cycle;
    public GameObject circum;
    public GameObject skillwait;
    public GameObject weaponimage;
    [Header("selectskill")]
    public GameObject skillselectui;
    public GameObject weaponlist;
    public GameObject skilllist;
    public GameObject skilllistviewport;
    public GameObject passiveview;
    public GameObject normalskillview;
    public GameObject arreyskillview;
    public GameObject skilllength;
    public GameObject skilllistset;
    public GameObject warning;
    public SkillQueUi skillQueUi;
    public GameObject waitskillslider;
    [Header("letterbox")]
    public GameObject letterbox;
    [Header("inv")]
    public GameObject invall;
    public GameObject iteminv1;
    public GameObject iteminv2;
    public GameObject itemimage;
    public GameObject itemname;
    public GameObject itemstroy;
    public GameObject itemdescription;
    public GameObject itemtag;
    public GameObject itemgiveui;
    public GameObject itemgiveselect;
    [Header("gameover")]
    public GameObject gameover;
    [Header("debug")]
    public GameObject debugall;
    public bool debugactive = false;
    [Header("playerscenestatus")]
    public GameObject playerscenestatus;
    [Header("tutorial")]
    public GameObject tutorialui;
    public GameObject glitch;
    public List<GameObject> glitchs = new List<GameObject> { };
    public GameObject tutorialuireal;
    public GameObject tutorialuireal2;
    public GameObject tutorialuireal3;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // ¾À À¯Áö
    }

    void Start()
    {
        OnUIReady?.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12) && debugactive)
        {
            debugactive = false;
            uimanager.Instance.debugall.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F12) && !debugactive)
        {
            debugactive = true;
            uimanager.Instance.debugall.SetActive(true);
        }
    }

    public void CloseFightUi()
    {
        foreach (GameObject gameobject in activewhenbattlestart)
        {
            gameobject.SetActive(false);
        }
    }

    public void ResetUi()
    {
        foreach (Transform ui in skilllistset.transform)
        {
            ui.GetComponent<skillbuttondisappear>().ButtonDisappearWhenUiReset();
        }
        foreach (GameObject gameobject in activewhenbattlestart)
        {
            gameobject.SetActive(false);
        }
        foreach (Transform ui in skilllength.transform)
        {
            Destroy(ui);
        }
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
        gameover.transform.GetChild(0).transform.GetComponent<gamerestart>().whilerestarting = false;
        gameover.transform.GetChild(0).GetComponent<Image>().DOFade(0.447f, 0);
        gameover.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().DOFade(1, 0);
        gameover.SetActive(false);
    }

    public void GoTutorial()
    {
        tutorialui.SetActive(false);
        for (int i = 0; i < 15; i++)
        {
            Vector3 pos = new Vector3(battalemanager.Instance.cameramanager.transform.position.x + UnityEngine.Random.Range(-15f, 15f), battalemanager.Instance.cameramanager.transform.position.y + UnityEngine.Random.Range(-9f, 9f), 0);
            Vector3 scale = new Vector3(100, UnityEngine.Random.Range(-3.5f, 3.5f), 0);
            GameObject currentglitch;
            currentglitch = Instantiate(glitch, pos, Quaternion.identity);
            currentglitch.transform.localScale = scale;
            currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
            glitchs.Add(currentglitch);
        }
        StartCoroutine(GoTutorial_co());
    }

    public void SkipTutorial()
    {
        tutorialui.SetActive(false);
        StartCoroutine(SkipTutorial_co());
    }

    IEnumerator GoTutorial_co()
    {
        soundmanager.instance.SFXStop();
        soundmanager.instance.BGMStop();

        string currentSceneName = SceneManager.GetActiveScene().name;

        yield return new WaitForSeconds(0.1f);

        battalemanager.Instance.DataSaveTo();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("tutorial", LoadSceneMode.Additive);
        yield return loadOperation;

        Scene newScene = SceneManager.GetSceneByName("tutorial");
        SceneManager.SetActiveScene(newScene);

        foreach (GameObject glitch in glitchs)
        {
            Destroy(glitch);
        }
        uimanager.Instance.ResetUi();
        battalemanager.Instance.cameramanager.GetComponent<CameraManager>().LookPlayer();
        yield return SceneManager.UnloadSceneAsync(currentSceneName);
        battalemanager.Instance.player.GetComponent<Animator>().SetTrigger("cutscene2");
    }

    IEnumerator SkipTutorial_co()
    {
        soundmanager.instance.SFXStop();
        soundmanager.instance.BGMStop();

        string currentSceneName = SceneManager.GetActiveScene().name;

        yield return new WaitForSeconds(0.1f);

        battalemanager.Instance.DataSaveTo();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("mirrorselect", LoadSceneMode.Additive);
        yield return loadOperation;

        Scene newScene = SceneManager.GetSceneByName("mirrorselect");
        SceneManager.SetActiveScene(newScene);

        uimanager.Instance.ResetUi();

        yield return SceneManager.UnloadSceneAsync(currentSceneName);
    }

    public void TutorialGo()
    {
        tutorialuireal.SetActive(true);
    }

    public void TutorialGo2()
    {
        tutorialuireal2.SetActive(true);
    }

    public void TutorialGo3()
    {
        tutorialuireal3.SetActive(true);
    }
}
