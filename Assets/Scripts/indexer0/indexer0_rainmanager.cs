using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_rainmanager : MonoBehaviour
{
    public GameObject rain1;
    public GameObject rain2;
    public GameObject rain3;
    public GameObject rain4;
    public GameObject[] shaders;
    public bool rain = false;
    public Vector2 endpos1;
    public Vector2 endpos2;
    public Vector3 whereshoot;
    public int raincount = 0;
   

    public void FixedUpdate()
    {
        if (rain)
        {
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain1, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain2, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain3, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain4, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain2, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain3, whereshoot, Quaternion.identity);
        }
    }

    IEnumerator Rain1Start()
    {
        shaders[0].SetActive(true);
        yield return new WaitForSeconds(3.5f);
        rain = true;
    }

    IEnumerator Rain2Start()
    {
        shaders[1].SetActive(true);
        yield return new WaitForSeconds(3f);
        rain = true;
    }

    IEnumerator Rain3Start()
    {
        shaders[2].SetActive(true);
        yield return new WaitForSeconds(2.5f);
        rain = true;
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
    }
}
