using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;
using UnityEngine.SceneManagement;

public class tocounselmanager : MonoBehaviour
{
    public GameObject box;
    public GameObject box2;
    public GameObject particle;
    public GameObject fbutton;
    public GameObject player;
    public GameObject cammanager;

    public bool cango = false;

    public void Start()
    {
        transform.localScale = new Vector3(0, 64, 1);
        transform.DOScaleX(1, 0.6f).SetEase(Ease.InQuart);
        StartCoroutine(SetParticle());

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fbutton.SetActive(true);
            cango = true;
        }
        else
        {
            fbutton.SetActive(false);
            cango = false;
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("fbutton"))
        {
            if (cango)
            {
                player.GetComponent<PlayerMove>().canmove = false;

                cango = false;

                StartCoroutine(GoTOCounsel());
            }
        }
    }

    IEnumerator GoTOCounsel()
    {
        GameObject currentbox1 = Instantiate(box2, transform.position, Quaternion.identity);
        currentbox1.transform.localScale = new Vector3(1, 128, 1);
        currentbox1.transform.DOScaleX(100f, 10f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("mirrorselect");
    }

    public void Spawn()
    {
        StartCoroutine(Spawn_co());
    }

    IEnumerator SetParticle()
    {
        yield return new WaitForSeconds(0.7f);
        particle.SetActive(true);
        InvokeRepeating("Spawn", 0f, 0.4f);
    }

    IEnumerator Spawn_co()
    {
        GameObject currentbox = Instantiate(box, transform.position, Quaternion.identity);
        currentbox.transform.DOScaleX(5f, 5f).SetEase(Ease.OutQuart);
        currentbox.GetComponent<SpriteRenderer>().DOFade(0f, 5f);//.SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(5.1f);
        Destroy(currentbox);
    }
}
