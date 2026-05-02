using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class uicronometer : MonoBehaviour
{
    public GameObject cronometer1;
    public GameObject cronometer2;
    public GameObject cronometer3;
    public GameObject cronometer4;

    public GameObject effect1;
    public GameObject effect2;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    public GameObject buttontext1;
    public GameObject buttontext2;
    public GameObject buttontext3;

    public GameObject[] lines;

    public void Start()
    {
        buttontext1.GetComponent<stencilequelwithparent>().SetMaterial();
        buttontext2.GetComponent<stencilequelwithparent>().SetMaterial();
        buttontext3.GetComponent<stencilequelwithparent>().SetMaterial();
    }

    public void OnEnable()
    {
        Pause();
    }

    private Coroutine pauseCoroutine;

    public void Pause()
    {
        if (pauseCoroutine != null)
            StopCoroutine(pauseCoroutine);

        pauseCoroutine = StartCoroutine(Pause_co());
    }

    IEnumerator Pause_co()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        cronometer4.transform.DOKill();
        cronometer3.transform.DOKill();
        transform.DOKill();

        cronometer4.transform.DORotate(new Vector3(0, 0, -180), 1.5f).SetEase(Ease.OutQuart).SetUpdate(true);
        cronometer3.transform.DORotate(new Vector3(0, 0, -90), 1.5f).SetEase(Ease.OutQuart).SetUpdate(true);
        //transform.DOLocalMove(new Vector3(-323, 50, 0), 1.5f).SetEase(Ease.OutQuart).SetUpdate(true);
        transform.DOLocalMove(new Vector3(-315, 125, 0), 1.5f).SetEase(Ease.OutQuart).SetUpdate(true);

        yield return new WaitForSecondsRealtime(1.4f);

        foreach (GameObject line in lines)
        {
            line.SetActive(true);
        }

        
    }

    public void Restart()
    {
        Debug.Log("adaaferg54h");

        if (pauseCoroutine != null)
            StopCoroutine(pauseCoroutine);

        cronometer4.transform.DOKill();
        cronometer3.transform.DOKill();
        transform.DOKill();

        cronometer4.transform.localRotation = Quaternion.Euler(0, 0, 0);
        cronometer3.transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localPosition = new Vector3(0, 0, 0);

        DOTween.Kill("pauseuitween");
        LineFade();

        foreach (GameObject line in lines)
        {
            line.SetActive(false);
        }
    }

    public void Restart_slow()
    {
        if (pauseCoroutine != null)
            StopCoroutine(pauseCoroutine);

        cronometer4.transform.DOKill();
        cronometer3.transform.DOKill();
        transform.DOKill();

        cronometer4.transform.DOLocalRotate(new Vector3(0, 0, -90), 1.5f).SetEase(Ease.InQuart).SetUpdate(true);
        cronometer3.transform.DOLocalRotate(new Vector3(0, 0, -90), 1.5f).SetEase(Ease.InQuart).SetUpdate(true);

        StartCoroutine(Restart_slow_co());
    }

    IEnumerator Restart_slow_co()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        battalemanager.Instance.transform.GetComponent<PauseManager>().canpause = true;
        battalemanager.Instance.transform.GetComponent<PauseManager>().Continue();
    }

    public void PauseUnlockButton()
    {
        battalemanager.Instance.transform.GetComponent<PauseManager>().Continue();
    }

    public void ReturnToSelect()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName != "mirrorselect")
        {
            Debug.Log(currentSceneName);
            StartCoroutine(GoTOCounsel());
            battalemanager.Instance.transform.GetComponent<PauseManager>().Continue_slow();
        }
        
    }

    IEnumerator GoTOCounsel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        battalemanager.Instance.gameObject.GetComponent<soundmanager>().BGMStop();

        battalemanager.Instance.DataSaveTo();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("mirrorselect", LoadSceneMode.Additive);
        yield return loadOperation;

        Scene newScene = SceneManager.GetSceneByName("mirrorselect");
        SceneManager.SetActiveScene(newScene);

        uimanager.Instance.ResetUi();

       yield return SceneManager.UnloadSceneAsync(currentSceneName);
    }

    public void LineFade()
    {
        foreach (GameObject line in lines)
        {
            line.transform.GetChild(0).GetComponent<uiredflash>().UnFlash();
            line.transform.GetChild(1).GetComponent<uiredflash>().UnFlash();
        }
    }
    

}
