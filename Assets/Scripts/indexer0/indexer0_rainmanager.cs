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



    public void FixedUpdate()
    {
        indexerpos = indexer.transform.position;

        if (rain)
        {
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            if (whereshoot.x > indexerpos.x - 2  && whereshoot.x <indexerpos.x + 2)
            {
                whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 39.3f, 0);
            }
            Instantiate(rain1, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            if (whereshoot.x > indexerpos.x - 2 && whereshoot.x < indexerpos.x + 2)
            {
                whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 39.3f, 0);
            }
            Instantiate(rain2, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            if (whereshoot.x > indexerpos.x - 2 && whereshoot.x < indexerpos.x + 2)
            {
                whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 39.3f, 0);
            }
            Instantiate(rain3, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            if (whereshoot.x > indexerpos.x - 2 && whereshoot.x < indexerpos.x + 2)
            {
                whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 39.3f, 0);
            }
            Instantiate(rain4, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            if (whereshoot.x > indexerpos.x - 2 && whereshoot.x < indexerpos.x + 2)
            {
                whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 39.3f, 0);
            }
            Instantiate(rain2, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            if (whereshoot.x > indexerpos.x - 2 && whereshoot.x < indexerpos.x + 2)
            {
                whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 39.3f, 0);
            }
            Instantiate(rain3, whereshoot, Quaternion.identity);
        }

        if (land)
        {
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 0.26f, 0);
            Instantiate(weaponland[0], whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 0.26f, 0);
            Instantiate(weaponland[1], whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 0.26f, 0);
            Instantiate(weaponland[2], whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 0.26f, 0);
            Instantiate(weaponland[3], whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 0.26f, 0);
            Instantiate(weaponland[4], whereshoot, Quaternion.identity);
        }

        passivecool++;

        if (passivecool >= passivecooltime / (passiverainint + 10))
        {
            Debug.Log("ºñ");
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain2, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain3, whereshoot, Quaternion.identity);
            passivecool = 0;
        }
    }

    IEnumerator Rain1Start()
    {
        shaders[0].SetActive(true);
        impulsecam.GetComponent<raincam>().Start1Shake();
        yield return new WaitForSeconds(1f);
        rain = true;
        yield return new WaitForSeconds(11f);
        rain = false;
        yield return new WaitForSeconds(7f);
        indexer.GetComponent<Animator>().SetBool("rainend", true);
        passiverainint = 0;
    }

    IEnumerator Rain2Start()
    {
        shaders[1].SetActive(true);
        impulsecam.GetComponent<raincam>().Start2Shake();
        yield return new WaitForSeconds(1f);
        rain = true;
        yield return new WaitForSeconds(11f);
        rain = false;
        yield return new WaitForSeconds(7f);
        indexer.GetComponent<Animator>().SetBool("rainend", true);
        passiverainint = 0;
    }

    IEnumerator Rain3Start()
    {
        shaders[2].SetActive(true);
        impulsecam.GetComponent<raincam>().Start3Shake();
        yield return new WaitForSeconds(1f);
        rain = true;
        yield return new WaitForSeconds(11f);
        rain = false;
        yield return new WaitForSeconds(7f);
        indexer.GetComponent<Animator>().SetBool("rainend", true);
        passiverainint = 0;
    }

    IEnumerator Rain4Start()
    {
        shaders[3].SetActive(true);
        impulsecam.GetComponent<raincam>().Start4Shake();
        yield return new WaitForSeconds(1f);
        rain = true;
        yield return new WaitForSeconds(11f);
        rain = false;
        yield return new WaitForSeconds(7f);
        indexer.GetComponent<Animator>().SetBool("rainend", true);
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
