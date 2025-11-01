using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteAlways] // 에디터 상태에서도 Update() 실행
public class cronometer_script : MonoBehaviour
{
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

    void FixedUpdate()
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

    [ContextMenu("BattleStart")]
    public void BattleStart()
    {
        FadeIn();
        StartCoroutine(BattleStart_co());
    }

    IEnumerator BattleStart_co()
    {
        float currentAngle = hourHand.eulerAngles.z;

        float endAngle = targetangle + (360f * 15f) + 90f ;

        DOTween.To(
            () => hourAngle,       
            x => hourAngle = x,    
            endAngle,              
            2.5f                     
        ).SetEase(Ease.InCubic).SetId("turn");

        yield return new WaitForSeconds(2.5f);
        Instantiate(effect, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibration0_5();

        DOTween.Kill("turn");

        DOTween.To(
            () => hourAngle,       
            x => hourAngle = x,
            targetangle + (360f * 15f),
            1f                     
        ).SetEase(Ease.OutQuart).SetId("turn");

        yield return new WaitForSeconds(0.78f);

        Instantiate(effect, transform.position, Quaternion.identity);
        Instantiate(effect2, transform.position, Quaternion.identity);
        cammanager.GetComponent<CameraManager>().CamVibration0_5();

        attackcore.GetComponent<attackcore>().BattleStart();
        FadeOut();
    }

    public void FadeIn()
    {
        transform.position = player.transform.position;
        middle.GetComponent<SpriteRenderer>().DOFade(1, 0.5f); 
        hourHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        minuteHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        secondHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
    }

    public void FadeOut()
    {
        middle.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        hourHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        minuteHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        secondHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
    }


}

