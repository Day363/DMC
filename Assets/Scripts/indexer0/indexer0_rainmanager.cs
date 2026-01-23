using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_rainmanager : MonoBehaviour
{
    public GameObject indexer;
    public GameObject impulsecam;
    public GameObject rain1;
    public GameObject rain2;
    public GameObject rain3;
    public GameObject rain4;
    public GameObject rain5;
    public GameObject rain6;
    public GameObject[] rain_;
    public GameObject[] shaders;
    public GameObject[] weaponland;
    public bool rain = false;
    public Vector2 endpos1;
    public Vector2 endpos2;
    public Vector3 whereshoot;
    public Vector3 indexerpos;
    public int raincount = 0;
    public bool land = false;
    public int passiverainint = 1;
    public int passivecooltime;
    public int passivecool = 0;
    public Coroutine raincorutine;
    public Coroutine raincorutine2;
    public Coroutine raincorutine3;
    public Coroutine raincorutine4;

    public void FixedUpdate()
    {
        if (rain)
        {
            if (raincorutine  == null)
            {
                raincorutine = StartCoroutine(Rain());
                raincorutine2 = StartCoroutine(Rain());
                raincorutine3 = StartCoroutine(Rain());
                raincorutine4 = StartCoroutine(Rain());
            }
            
        }
        else
        {
            if (raincorutine != null)
            {
                StopCoroutine(raincorutine);
                StopCoroutine(raincorutine2);
                StopCoroutine(raincorutine3);
                StopCoroutine(raincorutine4);
                raincorutine = null;
                raincorutine2 = null;
                raincorutine3 = null;
                raincorutine4 = null;
            }
        }
    }

    public void Land()
    {
        StartCoroutine(Land_co());
    }

    IEnumerator Land_co()
    {
        for (int i = 0; i < 32; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-50f, 50f), -1.8f, 1);

            GameObject currain = Instantiate(weaponland[Random.Range(0, weaponland.Length)], pos, Quaternion.identity);
            currain.GetComponent<enemyattack>().player = battalemanager.Instance.player;
            currain.GetComponent<enemyattack>().enemy = battalemanager.Instance.currentenemy;
            yield return null;
        }
    }

    IEnumerator Rain()
    {
        while (true)
        {
            yield return null;
            Vector3 pos = new Vector3(Random.Range(-50f, 50f), -1.8f, 1);
            if (Vector3.Distance(indexer.transform.position, pos) < 3)
            {
                pos = new Vector3(pos.x, 2.13f, 1);
            }
            
            GameObject currain = Instantiate(rain_[Random.Range(0, rain_.Length)], pos, Quaternion.identity);
            currain.GetComponent<enemyattack>().player = battalemanager.Instance.player;
            currain.GetComponent<enemyattack>().enemy = battalemanager.Instance.currentenemy;
        }
    }

    IEnumerator Rain1Start()
    {
        shaders[0].SetActive(true);
        impulsecam.GetComponent<CameraManager>().LimitlessShake(3, 5, 18);
        yield return new WaitForSeconds(1f);
        rain = true;
        yield return new WaitForSeconds(11f);
        rain = false;
        yield return new WaitForSeconds(7f);
        indexer.GetComponent<Animator>().SetTrigger("rainend");
        passiverainint = 0;
    }

    IEnumerator Rain2Start()
    {
        shaders[1].SetActive(true);
        impulsecam.GetComponent<CameraManager>().LimitlessShake(7, 10, 18);
        yield return new WaitForSeconds(1f);
        rain = true;
        yield return new WaitForSeconds(11f);
        rain = false;
        yield return new WaitForSeconds(7f);
        indexer.GetComponent<Animator>().SetTrigger("rainend");
        passiverainint = 0;
    }

    IEnumerator Rain3Start()
    {
        shaders[2].SetActive(true);
        impulsecam.GetComponent<CameraManager>().LimitlessShake(13, 25, 18);
        yield return new WaitForSeconds(1f);
        rain = true;
        yield return new WaitForSeconds(11f);
        rain = false;
        yield return new WaitForSeconds(7f);
        indexer.GetComponent<Animator>().SetTrigger("rainend");
        passiverainint = 0;
    }

    IEnumerator Rain4Start()
    {
        shaders[3].SetActive(true);
        impulsecam.GetComponent<CameraManager>().LimitlessShake(30, 50, 18);
        yield return new WaitForSeconds(1f);
        rain = true;
        yield return new WaitForSeconds(11f);
        rain = false;
        yield return new WaitForSeconds(7f);
        indexer.GetComponent<Animator>().SetTrigger("rainend");
        passiverainint = 0;
    }

    public void DoRain()
    {
        if (raincount == 0)
        {
            StartCoroutine(Rain1Start());
            raincount = raincount + 1;
        }

        else if (raincount == 1)
        {
            StartCoroutine(Rain2Start());
            raincount = raincount + 1;
        }

        else if (raincount == 2)
        {
            StartCoroutine(Rain3Start());
            raincount = raincount + 1;
        }

        else if (raincount == 3)
        {
            StartCoroutine(Rain4Start());
            raincount = raincount + 1;
        }
    }
}
