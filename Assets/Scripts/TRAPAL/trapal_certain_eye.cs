using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class trapal_certain_eye : MonoBehaviour
{
    public static Action OnDie;

    public GameObject eye;
    public GameObject player;
    public GameObject trapal_arrow;

    public GameObject gamemanager;
    public GameObject cammanager;
    public GameObject attackcore;
    public trapal_passive tp;
    public GameObject trapal;

    public void Start()
    {
        eye = tp.eye2;
        GetComponent<normal_enemy_hp>().OnDeath += Whendie;
        trapal_passive.OnCertainEnd += Disappear;
    }

    public void Whendie()
    {
        OnDie?.Invoke();
        StartCoroutine(Whendie_co());
    }

    public void Disappear()
    {
        StartCoroutine(Disappear_co());
    }

    IEnumerator Disappear_co()
    {
        transform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(1.2f);
        trapal_passive.OnCertainEnd -= Disappear;
        Destroy(gameObject);
    }

    IEnumerator Whendie_co()
    {
        trapal_passive.OnCertainEnd -= Disappear;

        GameObject currentarrow = Instantiate(trapal_arrow, transform.position, transform.rotation);
        currentarrow.GetComponent<trapal_arrow>().trapal = trapal;
        currentarrow.GetComponent<trapal_arrow>().player = player;

        transform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(1.2f);

        if (tp.whilecertain24)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(1f, 7f), 0);
            GameObject currenteye = Instantiate(eye, pos, Quaternion.identity);
            trapal_certain_eye currenteyetrapal_certain_eye = currenteye.GetComponent<trapal_certain_eye>();
            currenteyetrapal_certain_eye.trapal = trapal;
            currenteyetrapal_certain_eye.tp = tp;
            currenteyetrapal_certain_eye.player = player;
            currenteyetrapal_certain_eye.gamemanager = gamemanager;
            currenteyetrapal_certain_eye.cammanager = cammanager;
            currenteyetrapal_certain_eye.attackcore = attackcore;
            currenteye.transform.localScale = Vector3.zero;
            float scale = UnityEngine.Random.Range(0.5f, 1.5f);
            currenteye.transform.DOScale(new Vector3(scale, scale, 1), 0.5f);
            currenteye.transform.GetChild(0).GetComponent<trapal_deny_eye>().centerObject = currenteye.transform.GetChild(1);
            currenteye.transform.GetChild(0).GetComponent<trapal_deny_eye>().player = player.transform;
            normal_enemy_hp currenteyenormal_enemy_hp = currenteye.GetComponent<normal_enemy_hp>();
            currenteyenormal_enemy_hp.gammanager = gamemanager;
            currenteyenormal_enemy_hp.cammanager = cammanager;
            currenteyenormal_enemy_hp.attackcore = attackcore;
            pos = new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(1f, 7f), 0);
            currenteye = Instantiate(eye, pos, Quaternion.identity);
            currenteyetrapal_certain_eye = currenteye.GetComponent<trapal_certain_eye>();
            currenteyetrapal_certain_eye.trapal = trapal;
            currenteyetrapal_certain_eye.tp = tp;
            currenteyetrapal_certain_eye.player = player;
            currenteyetrapal_certain_eye.gamemanager = gamemanager;
            currenteyetrapal_certain_eye.cammanager = cammanager;
            currenteyetrapal_certain_eye.attackcore = attackcore;
            currenteye.transform.localScale = Vector3.zero;
            scale = UnityEngine.Random.Range(0.5f, 1.5f);
            currenteye.transform.DOScale(new Vector3(scale, scale, 1), 0.5f);
            currenteye.transform.GetChild(0).GetComponent<trapal_deny_eye>().centerObject = currenteye.transform.GetChild(1);
            currenteye.transform.GetChild(0).GetComponent<trapal_deny_eye>().player = player.transform;
            currenteyenormal_enemy_hp = currenteye.GetComponent<normal_enemy_hp>();
            currenteyenormal_enemy_hp.gammanager = gamemanager;
            currenteyenormal_enemy_hp.cammanager = cammanager;
            currenteyenormal_enemy_hp.attackcore = attackcore;
            yield return new WaitForSeconds(1.2f);
        }
        

        Destroy(gameObject);
    }
}
