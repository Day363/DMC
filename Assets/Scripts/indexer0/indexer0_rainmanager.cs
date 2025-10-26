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

    public void FixedUpdate()
    {
        if (rain)
        {
            if (raincorutine  == null)
            {
                raincorutine = StartCoroutine(Rain());
                raincorutine2 = StartCoroutine(Rain());
            }
            
        }
        else
        {
            if (raincorutine != null)
            {
                StopCoroutine(raincorutine);
                StopCoroutine(raincorutine2);
                raincorutine = null;
                raincorutine2 = null;
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

            Instantiate(weaponland[Random.Range(0, weaponland.Length)], pos, Quaternion.identity);
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator Rain()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            Vector3 pos = new Vector3(Random.Range(-50f, 50f), -1.8f, 1);
            if (Vector3.Distance(indexer.transform.position, pos) < 3)
            {
                pos = new Vector3(pos.x, 2.13f, 1);
            }
            
            Instantiate(rain_[Random.Range(0, rain_.Length)], pos, Quaternion.identity);
        }
    }

    IEnumerator Rain1Start()
    {
        shaders[0].SetActive(true);
        impulsecam.GetComponent<CameraManager>().LimitlessShake(6, 1, 18);
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
        impulsecam.GetComponent<CameraManager>().LimitlessShake(11, 2, 18);
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
        impulsecam.GetComponent<CameraManager>().LimitlessShake(18, 3, 18);
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
        impulsecam.GetComponent<CameraManager>().LimitlessShake(30, 4, 18);
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
