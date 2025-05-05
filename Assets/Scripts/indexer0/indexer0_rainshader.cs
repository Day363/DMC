using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_rainshader : MonoBehaviour
{
    public float targetrainpower;
    public float rainpower = 0;
    public int dividenum;
    public bool rain = true;
    public bool rainend = false;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_rainpower", 0);
    }

    private void FixedUpdate()
    {
        if (rainpower <= targetrainpower && rain)
        {
            rainpower = targetrainpower / dividenum + rainpower;
            GetComponent<SpriteRenderer>().material.SetFloat("_rainpower", rainpower);
        }

        if (rainpower >= targetrainpower && rain)
        {
            rain = false;
            StartCoroutine(Raintime());
        }

        if (rainend)
        {
            rainpower = rainpower - targetrainpower / dividenum;
            GetComponent<SpriteRenderer>().material.SetFloat("_rainpower", rainpower);
            if (rainpower < 0)
            {
                gameObject.SetActive(false);
            }
        }

    }
    

    IEnumerator Raintime()
    {
        yield return new WaitForSeconds(5f);
        RainEnd();
    }

    public void RainEnd()
    {
        rainend = true;
    }
}
