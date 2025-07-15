using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defensescript : MonoBehaviour
{
    public GameObject player;

    public bool defense;
    public bool parry;
    public bool dodge;

    public string parryanimationtrigger;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "client")
        {
            if (parry)
            {
                player.GetComponent<Animator>().SetTrigger(parryanimationtrigger);
            }
        }
    }
}
