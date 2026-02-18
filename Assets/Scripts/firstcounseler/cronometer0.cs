using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cronometer0 : MonoBehaviour
{
    public float targetangle;

    public Transform middle;
    public Transform hourHand;   // 시침 (부모)
    public Transform minuteHand; // 분침 (시침의 자식)
    public Transform secondHand; // 초침 (분침의 자식)
    public GameObject[] etcs;

    public GameObject effect;
    public GameObject effect2;

    public float hourRotationTime = 86400f;

    [Range(0f, 360f)]
    public float hourSpeed;
    [Range(0f, 360f)]
    public float hourAngle = 0f;

    public bool normaltime;


    void Update()
    {
        if (normaltime)
        {

            hourAngle += hourSpeed;
        }

        hourHand.eulerAngles = new Vector3(0, 0, -hourAngle);
        minuteHand.eulerAngles = new Vector3(0, 0, -(hourAngle * 3f));
        secondHand.eulerAngles = new Vector3(0, 0, -(hourAngle * 9f));
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
        float endAngle = targetangle + (360f * 15f) + 90f;

        DOTween.To(
            () => hourAngle,
            x => hourAngle = x,
            endAngle,
            2.5f
        ).SetEase(Ease.InCubic).SetId("turn").SetUpdate(true);

        yield return new WaitForSecondsRealtime(2.5f);
        Instantiate(effect, transform.position, Quaternion.identity);

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
        yield return new WaitForSecondsRealtime(0.1f);
    }

    public void FadeIn()
    {
        DOTween.Kill("fade0");
        middle.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true).SetId("fade0");
        hourHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true).SetId("fade0");
        minuteHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true).SetId("fade0");
        secondHand.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true).SetId("fade0");
        foreach (GameObject etc in etcs)
        {
            etc.GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true).SetId("fade0");
        }
    }

    public void FadeOut()
    {
        DOTween.Kill("fade0");
        middle.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade0");
        hourHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade0");
        minuteHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade0");
        secondHand.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade0");
        foreach (GameObject etc in etcs)
        {
            etc.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetUpdate(true).SetId("fade0");
        }
    }

    public void WhenLifeCoutDown()
    {
        StartCoroutine(WhenLifeCoutDown_co());
    }

    IEnumerator WhenLifeCoutDown_co()
    {
        Time.timeScale = 0f;

        DOTween.To(
            () => hourAngle,
            x => hourAngle = x,
            targetangle + 360 + 30,
            1.5f
        ).SetEase(Ease.InCubic).SetId("turn").SetUpdate(true);

        yield return new WaitForSecondsRealtime(1.5f);
        Instantiate(effect, transform.position, Quaternion.identity);

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

        Time.timeScale = 1f;
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

}
