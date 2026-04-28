using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

[ExecuteAlways] // 에디터 상태에서도 Update() 실행
public class cronometer_script : MonoBehaviour
{
    public Camera mainCam;
    public float targetangle;
    public GameObject attackcore_;
    public GameObject cammanager;
    public GameObject player;

    public Transform middle;
    public Transform hourHand;   // 시침 (부모)
    public Transform minuteHand; // 분침 (시침의 자식)
    public Transform secondHand; // 초침 (분침의 자식)

    public GameObject effect;
    public GameObject effect2;

    public float hourRotationTime = 86400f;

    [Range(0f, 360f)]
    public float hourAngle = 0f;

    public bool normaltime;

    public playerstatus playerplayerstatus;

    public GameObject[] rings;
    public GameObject[] lines;
    public GameObject pin;
    public GameObject bossname;
    public GameObject where;

    public bool tutorial = false;


    public void Start()
    {
        player = battalemanager.Instance.player;
        playerplayerstatus  = player.GetComponent<playerstatus>();

        battalemanager.Instance.cronometer = gameObject;
        attackcore.attackcoreInstance.cronometer = gameObject;
        attackcore_ = attackcore.attackcoreInstance.gameObject;
    }

    void Update()
    {
        if (normaltime)
        {
            float hourSpeed = 360f / hourRotationTime;


            float delta = Application.isPlaying ? Time.deltaTime : (1f / 60f);

            hourAngle += hourSpeed * delta;
        }
        
        hourHand.eulerAngles = new Vector3(0, 0, -hourAngle);
        minuteHand.eulerAngles = new Vector3(0, 0, -(hourAngle * 12f));
        secondHand.eulerAngles = new Vector3(0, 0, -(hourAngle * 720f));
    }

    public void BattleStart()
    {
        FadeIn();
        StartCoroutine(BattleStart_co());
    }

    IEnumerator BattleStart_co()
    {
        player = battalemanager.Instance.player;
        playerplayerstatus = player.GetComponent<playerstatus>();


        foreach (GameObject halo in rings)
        {
            halo.transform.localScale = new Vector3(0, 0, 1);
            halo.GetComponent<trapalhaloturnupdate>().turnspeed = UnityEngine.Random.Range(-2f, 2f);
            float i = UnityEngine.Random.Range(0.4f, 1.1f);
            halo.transform.DOScale(new Vector3(i, i, 1), 2.5f).SetUpdate(true).SetId("turn").SetEase(Ease.OutQuad);
        }

        Time.timeScale = 0f;
        float currentAngle = hourHand.eulerAngles.z;
        targetangle = (playerplayerstatus.lifecount / playerplayerstatus.maxlifecount) * 360;
        float endAngle = targetangle + (360f * 15f) + 90f ;

        DOTween.To(
            () => hourAngle,       
            x => hourAngle = x,    
            endAngle,              
            2.5f                     
        ).SetEase(Ease.InCubic).SetId("turn").SetUpdate(true);

        soundmanager.instance.SoundPlay("battlestart");
        soundmanager.instance.SoundPlay("clocking");

        yield return new WaitForSecondsRealtime(2.5f);

        soundmanager.instance.SoundPlay("clockbell");

        Instantiate(effect, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibTimeIgnore();

        DOTween.Kill("turn");

        foreach (GameObject halo in rings)
        {
            float i = UnityEngine.Random.Range(0.8f, 2.1f);
            halo.transform.DOScale(new Vector3(i, i, 1), 0.1f).SetUpdate(true).SetId("turn").SetEase(Ease.OutQuad);
            halo.GetComponent<battletitleflash>().Flash();
            halo.GetComponent<SpriteRenderer>().DOFade(0, 3f).SetUpdate(true);
        }
        pin.GetComponent<SpriteRenderer>().DOFade(0, 3f).SetUpdate(true);
        foreach (GameObject line in lines)
        {
            line.SetActive(true);
            line.GetComponent<SpriteRenderer>().DOFade(0, 3f).SetUpdate(true);
        }
        bossname.SetActive(true);
        bossname.GetComponent<TMP_Text>().DOFade(0, 3f).SetUpdate(true).SetEase(Ease.InQuart);
        where.SetActive(true);
        where.GetComponent<TMP_Text>().DOFade(0, 3f).SetUpdate(true).SetEase(Ease.InQuart);

        DOTween.To(
            () => hourAngle,       
            x => hourAngle = x,
            targetangle + (360f * 15f),
            3f                     
        ).SetEase(Ease.OutQuart).SetId("turn").SetUpdate(true);

        yield return new WaitForSecondsRealtime(3f);

        soundmanager.instance.SoundPlay("clockbell");

        Instantiate(effect, transform.position, Quaternion.identity);
        Instantiate(effect2, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibTimeIgnore();

        yield return new WaitForSecondsRealtime(0.1f);

        attackcore_.GetComponent<attackcore>().BattleStart();
        if (tutorial)
        {
            uimanager.Instance.TutorialGo();
        }
        
        FadeOut();
    }

    public void FadeIn()
    {
        DOTween.Kill("fade");
        pin.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true);
        foreach (GameObject halo in rings)
        {
            halo.GetComponent<SpriteRenderer>().DOFade(0.5f, 0.5f).SetUpdate(true).SetId("fade");

            halo.transform.localScale = new Vector3(0, 0, 1);
            halo.GetComponent<trapalhaloturnupdate>().turnspeed = UnityEngine.Random.Range(-2f, 2f);
            float i = UnityEngine.Random.Range(0.4f, 1.1f);
            halo.transform.DOScale(new Vector3(i, i, 1), 2.5f).SetUpdate(true).SetId("turn").SetEase(Ease.OutQuad);
        }
        Vector3 camPos = mainCam.transform.position;
        transform.position = new Vector3(camPos.x, camPos.y, transform.position.z);
        middle.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true).SetId("fade"); 
        hourHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true).SetId("fade");
        minuteHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true).SetId("fade");
        secondHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true).SetId("fade");
    }

    public void FadeOut()
    {
        DOTween.Kill("fade");
        pin.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true);
        foreach (GameObject halo in rings)
        {
            halo.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade");
        }
        middle.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade");
        hourHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade");
        minuteHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade");
        secondHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade");

    }

    public void WhenLifeCoutDown()
    {
        FadeIn();
        StartCoroutine(WhenLifeCoutDown_co());
    }

    public void WhenLifeCoutDownEnd()
    {
        
        FadeIn();
        StartCoroutine(WhenLifeCoutDownEnd_co());
    }

    IEnumerator WhenLifeCoutDown_co()
    {
        player = battalemanager.Instance.player;
        playerplayerstatus = player.GetComponent<playerstatus>();

        battalemanager.Instance.gameObject.GetComponent<PauseManager>().ispause = true;
        targetangle = 360 - (((float)playerplayerstatus.lifecount / (float)playerplayerstatus.maxlifecount) * 360);

        DOTween.To(
            () => hourAngle,
            x => hourAngle = x,
            targetangle + 360 + 30,
            1.5f
        ).SetEase(Ease.InCubic).SetId("turn").SetUpdate(true);

        yield return new WaitForSecondsRealtime(1.5f);
        Instantiate(effect, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibTimeIgnore();

        DOTween.Kill("turn");

        DOTween.To(
            () => hourAngle,
            x => hourAngle = x,
            targetangle + 360,
            1f
        ).SetEase(Ease.OutQuart).SetId("turn").SetUpdate(true);

        yield return new WaitForSecondsRealtime(1f);
        

        Instantiate(effect, transform.position, Quaternion.identity);
        Instantiate(effect2, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibTimeIgnore();

        FadeOut();
        Time.timeScale = 1f;
        battalemanager.Instance.gameObject.GetComponent<PauseManager>().ispause = false;
    }

    IEnumerator WhenLifeCoutDownEnd_co()
    {
        battalemanager.Instance.gameObject.GetComponent<PauseManager>().ispause = true;
        targetangle = 360 - (((float)playerplayerstatus.lifecount / (float)playerplayerstatus.maxlifecount) * 360);

        DOTween.To(
            () => hourAngle,
            x => hourAngle = x,
            targetangle + 360 + 30,
            1.5f
        ).SetEase(Ease.InCubic).SetId("turn").SetUpdate(true);

        yield return new WaitForSecondsRealtime(1.5f);
        Instantiate(effect, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibTimeIgnore();

        DOTween.Kill("turn");

        DOTween.To(
            () => hourAngle,
            x => hourAngle = x,
            targetangle + 360,
            1f
        ).SetEase(Ease.OutQuart).SetId("turn").SetUpdate(true);

        yield return new WaitForSecondsRealtime(1f);
        player.GetComponent<skillfunction>().GameOver();
        uimanager.Instance.gameover.SetActive(true);
        uimanager.Instance.CloseFightUi();
        uimanager.Instance.CloseFightUi();
        battalemanager.Instance.gameObject.GetComponent<soundmanager>().BGMStop();

        Instantiate(effect, transform.position, Quaternion.identity);
        Instantiate(effect2, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibTimeIgnore();
    }

    public void RestartTurn()
    {
        DOTween.To(
            () => hourAngle,
            x => hourAngle = x,
            0f,
            3f
        ).SetEase(Ease.InCubic).SetId("turn").SetUpdate(true);
    }




    [ContextMenu("Force Stop Battle (Editor)")]
    void ForceStopBattle()
    {
        try
        {
            DOTween.Kill("turn", complete: false);
            DOTween.KillAll(complete: false);
            StopAllCoroutines();
            hourAngle = 0f;
            Debug.Log("강제 정지 완료: DOTween 및 코루틴 종료.");
            Time.timeScale = 1;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("ForceStopBattle 중 예외: " + ex);
        }
    }

}

