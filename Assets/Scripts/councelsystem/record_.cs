using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class record_ : MonoBehaviour
{
    public GameObject fbutton;
    public bool available;
    public GameObject gamemanager;
    public GameObject chat;

    public void Start()
    {
        gamemanager = battalemanager.Instance.gameObject;
        battalemanager.Instance.currentenemys.Add(gameObject);
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (fbutton != null)
            {
                fbutton.SetActive(true);
            }
            available = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && available)
        {
            fbutton.SetActive(false);
            available = false;
        }
    }

    public void Update()
    {

        if (available && Input.GetButtonDown("fbutton"))
        {
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            gamemanager.GetComponent<chatmanager>().CallDialogue(12);
        }

    }
}
