using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_lazer : MonoBehaviour
{
    public GameObject lazer;
    public int dir;
    public float time;
    public float currentang;
    public float speed;
    public bool avail;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (avail)
        {
            currentang = currentang + speed * dir;
            transform.rotation = Quaternion.Euler(0, 0, currentang);
            lazer.transform.rotation = Quaternion.Euler(0, 0, currentang + 180);

        }
       
    }

    public void LazerStart()
    {
        lazer.SetActive(true);
        lazer.GetComponent<Animator>().SetBool("end", false);
        lazer.GetComponent<Animator>().SetBool("start", true);

        dir = Random.Range(1, 3);
        if (dir == 1)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }

        time = Random.Range(5, 15);

        speed = Random.Range(0.3f, 1.3f);

        avail = true;
        StartCoroutine(Endtime());
    }

    IEnumerator Endtime()
    {
        yield return new WaitForSeconds(time);
        avail = false;
        lazer.GetComponent<Animator>().SetBool("end", true);
        lazer.GetComponent<Animator>().SetBool("start", false);
        yield return new WaitForSeconds(1);
        lazer.SetActive(false);
    }
}
