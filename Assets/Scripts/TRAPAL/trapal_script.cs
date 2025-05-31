using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_script : MonoBehaviour
{
    public GameObject player;
    public GameObject lazer;
    public GameObject pointattack;
    public GameObject barrier;
    public GameObject gun;
    public GameObject eye;
    public GameObject eyeflash;
    public GameObject eyeslash;
    public float lazercool = 0;
    public float lazercooltime;
    public float pointattackcool = 0;
    public float pointattackcooltime;
    public float barriercool = 0;
    public float barriercooltime;
    public float guncool = 0;
    public float guncooltime;
    public float gunrandomx1;
    public float gunrandomx2;
    public int gunmaxint;
    public int gunint;
    public float eyecool = 0;
    public float eyecooltime;
    public int randomeyeint;
    public float eyerandomx;
    public float eyerandomy;

    private void FixedUpdate()
    {
        lazercool++;
        pointattackcool++;
        barriercool++;
        guncool++;
        eyecool++;

        if (lazercool >= lazercooltime)
        {
            lazercool = 0;
            lazer.GetComponent<trapal_lazer>().LazerStart();
        }

        if (pointattackcool >= pointattackcooltime)
        {
            pointattackcool = 0;
            Instantiate(pointattack, new Vector3(Random.Range(-15, 16), 0, 0), Quaternion.identity);
        }

        if (barriercool >= barriercooltime)
        {
            barriercool = 0;
            barrier.GetComponent<trapal_barreirtext>().StartBarrier();
        }

        if (guncool >= guncooltime)
        {
            guncool = 0;
            gunint = Random.Range(3, gunmaxint);
            StartCoroutine(GunSpawn());
        }

        if (eyecool >= eyecooltime)
        {
            eyecool = 0;
            randomeyeint = Random.Range(1, 4);
            eye.GetComponent<Animator>().SetBool("ready", true);
            StartCoroutine(Eye());
        }
    }

    IEnumerator GunSpawn()
    {
        for (int i = 0; i < gunint; i++)
        {
            yield return new WaitForSeconds(1f);
            GameObject currentgun = Instantiate(gun, new Vector3(Random.Range(gunrandomx1, gunrandomx2), 21, 0), Quaternion.identity);
            currentgun.GetComponent<trapal_gun>().player = player;
        }
    }

    IEnumerator Eye()
    {
        float x = Random.Range(-eyerandomx, eyerandomx);
        float y = Random.Range(-eyerandomy, eyerandomy);
        Vector3 wherespwan = new Vector3(x, y + 3, 0);
        GameObject currenteye1 = Instantiate(eyeslash, wherespwan, Quaternion.identity);
        currenteye1.GetComponent<trapal_eyeslash>().player = player.transform;
        x = Random.Range(-eyerandomx, eyerandomx);
        y = Random.Range(-eyerandomy, eyerandomy);
        wherespwan = new Vector3(x, y + 3, 0);
        GameObject currenteye2 = Instantiate(eyeslash, wherespwan, Quaternion.identity);
        currenteye2.GetComponent<trapal_eyeslash>().player = player.transform;
        x = Random.Range(-eyerandomx, eyerandomx);
        y = Random.Range(-eyerandomy, eyerandomy);
        wherespwan = new Vector3(x, y + 3, 0);
        GameObject currenteye3 = Instantiate(eyeslash, wherespwan, Quaternion.identity);
        currenteye3.GetComponent<trapal_eyeslash>().player = player.transform;


        yield return new WaitForSeconds(1.5f);
        eye.GetComponent<Animator>().SetBool("ready", false);
        if (randomeyeint == 1)
        {
            Instantiate(eyeflash, eye.transform.position, Quaternion.identity);
            currenteye1.GetComponent<trapal_eyeslash>().Startslash(1);
            currenteye2.GetComponent<trapal_eyeslash>().Startslash(1);
            currenteye3.GetComponent<trapal_eyeslash>().Startslash(1);
        }

        else if (randomeyeint == 2)
        {
            Instantiate(eyeflash, eye.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            Instantiate(eyeflash, transform.position, Quaternion.identity);
            currenteye1.GetComponent<trapal_eyeslash>().Startslash(2);
            currenteye2.GetComponent<trapal_eyeslash>().Startslash(2);
            currenteye3.GetComponent<trapal_eyeslash>().Startslash(2);
        }

        else if (randomeyeint == 3)
        {
            Instantiate(eyeflash, eye.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            Instantiate(eyeflash, eye.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            Instantiate(eyeflash, eye.transform.position, Quaternion.identity);
            currenteye1.GetComponent<trapal_eyeslash>().Startslash(3);
            currenteye2.GetComponent<trapal_eyeslash>().Startslash(3);
            currenteye3.GetComponent<trapal_eyeslash>().Startslash(3);
        }
    }

}
