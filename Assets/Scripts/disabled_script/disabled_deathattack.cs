using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabled_deathattack : MonoBehaviour
{
    public GameObject player;
    public GameObject disabled;
    public GameObject attackcore;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<playerstatus>().maxbalance / 2 > collision.gameObject.GetComponent<playerstatus>().currentbalance)
            {
                disabled.GetComponent<disabledcam>().Deathattack();
                disabled.GetComponent<Animator>().SetBool("2phase_deathattack_success", true);
                Color color = player.GetComponent<SpriteRenderer>().color;
                color.a = 0f;
                player.GetComponent<SpriteRenderer>().color = color;
                player.GetComponent<PlayerMove>().canmove = false;
                attackcore.GetComponent<attackcore>().canattack = false;
                attackcore.SetActive(false);
            }
        }
        
    }
}
