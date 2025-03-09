using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecttime : MonoBehaviour
{
    public GameObject fbutton;
    public GameObject selection;
    public GameObject player;
    public bool selectactive = false;
    public bool s1active = false;

    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fbutton.SetActive(true);
            selectactive = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fbutton.SetActive(false);
            selectactive = false;
        }
        
    }

    void Update()
    {
        if (Input.GetButtonDown("fbutton") && selectactive)
        {
            if (!s1active)
            {
                selection.SetActive(true);
                s1active = true;
                player.GetComponent<PlayerMove>().canmove = false;

            }
            else
            {
                selection.SetActive(false);
                s1active = false;
                player.GetComponent<PlayerMove>().canmove = true;
            }
            
        }
        
    }

}
