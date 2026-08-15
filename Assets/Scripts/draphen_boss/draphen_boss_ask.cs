using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draphen_boss_ask : MonoBehaviour
{
    public GameObject player;
    public GameObject fbutton;
    public bool whilein;
    public bool trigger = true;

    public void OnTriggerStay2D(Collider2D collision)
    {
        fbutton.SetActive(true);
        whilein = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        fbutton.SetActive(false);
        whilein = false;
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("fbutton") && trigger && whilein)
        {
            trigger = false;
            Dialogue();
        }
    }

    public void Dialogue()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


    }
}
