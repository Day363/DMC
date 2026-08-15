using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Playables;
using UnityEngine.UI;
using DG.Tweening;

public class draphen_script : MonoBehaviour
{
    public bool firstmet;
    public GameObject player;
    public GameObject boss;
    public GameObject fadeout;

    public GameObject fbutton;
    public bool whilein;
    public bool trigger = true;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fbutton.SetActive(true);
            whilein = true;
        }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fbutton.SetActive(false);
            whilein = false;
        }
            
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("fbutton") && trigger && whilein)
        {
            trigger = false;
            GetComponent<PlayableDirector>().Play();
            battalemanager.Instance.currentenemys.Add(boss);
        }
    }

    public void PlayerThrow()
    {
        player.GetComponent<PlayerMove>().canmove = false;

        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 10), ForceMode2D.Impulse);
    }

    public void End()
    {
        fadeout.GetComponent<Image>().DOFade(1, 1.5f);
        StartCoroutine(End_co());
    }

    IEnumerator End_co()
    {
        yield return new WaitForSeconds(2f);
        boss.SetActive(true);
        player.GetComponent<PlayerMove>().canmove = true;
        fadeout.GetComponent<Image>().DOFade(0, 1f);
        gameObject.SetActive(false);
    }

    //public void Update()
    //{
    //    if (!firstmet && Vector3.Distance(transform.position, player.transform.position) < 12)
    //    {
    //        firstmet = true;
    //        GetComponent<PlayableDirector>().Play();
    //        player.GetComponent<PlayerMove>().canmove = false;
    //    }
    //}
}
