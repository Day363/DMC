using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteAlways] // 에디터 상태에서도 Update() 실행
public class cronometer_script : MonoBehaviour
{
    public Camera mainCam;
    public float targetangle;
    public GameObject attackcore;
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

    public void Start()
    {
        playerplayerstatus  = player.GetComponent<playerstatus>();
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

        yield return new WaitForSecondsRealtime(2.5f);
        Instantiate(effect, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibTimeIgnore();

        DOTween.Kill("turn");

        DOTween.To(
            () => hourAngle,       
            x => hourAngle = x,
            targetangle + (360f * 15f),
            1f                     
        ).SetEase(Ease.OutQuart).SetId("turn").SetUpdate(true);

        yield return new WaitForSecondsRealtime(1f);

        Instantiate(effect, transform.position, Quaternion.identity);
        Instantiate(effect2, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibTimeIgnore();

        yield return new WaitForSecondsRealtime(0.1f);

        attackcore.GetComponent<attackcore>().BattleStart();
        FadeOut();
    }

    public void FadeIn()
    {
        Vector3 camPos = mainCam.transform.position;
        transform.position = new Vector3(camPos.x, camPos.y, transform.position.z);
        middle.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true); 
        hourHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true);
        minuteHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true);
        secondHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true);
    }

    public void FadeOut()
    {
        middle.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true);
        hourHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true);
        minuteHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true);
        secondHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true);

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
        Time.timeScale = 0f;
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
    }

    IEnumerator WhenLifeCoutDownEnd_co()
    {
        Time.timeScale = 0f;
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

        Instantiate(effect, transform.position, Quaternion.identity);
        Instantiate(effect2, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibTimeIgnore();
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
            #if UNITY_EDITOR
            #endif
            Debug.Log("강제 정지 완료: DOTween 및 코루틴 종료.");
            Time.timeScale = 1;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("ForceStopBattle 중 예외: " + ex);
        }
    }

}

