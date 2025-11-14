using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_trapal_attack1_trigger : MonoBehaviour
{
    public GameObject player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "client")
        {
            player.GetComponent<skillfunction>().trapal_attack1_recycle = true;
        }
    }
}
