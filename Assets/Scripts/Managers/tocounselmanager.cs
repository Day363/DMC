using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class tocounselmanager : MonoBehaviour
{
    public GameObject box;
    public GameObject box2;
    public GameObject particle;
    public GameObject fbutton;
    public GameObject player;
    public GameObject cammanager;
    public GameObject counselcollider;
    public GameObject background;

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
        player.transform.position = new Vector3(-20, -2, 0);
        GameObject currentbox2 = Instantiate(box2, player.transform.position, Quaternion.identity);
        currentbox2.transform.localScale = new Vector3(100, 64, 1);
        currentbox2.GetComponent<SpriteRenderer>().DOFade(0f, 2.5f).SetEase(Ease.OutQuart);

        background.SetActive(false);

        cammanager.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = counselcollider.GetComponent<PolygonCollider2D>();
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerMove>().canmove = true;
        yield return new WaitForSeconds(5f);
        Destroy(currentbox2);
        Destroy(currentbox1);
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
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
