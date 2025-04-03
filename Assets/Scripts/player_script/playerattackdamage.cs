using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattackdamage : MonoBehaviour
{
    public GameObject player;
    public float damagenum;
    public int damage;
    public bool slash;
    public bool penetrate;
    public bool blow;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "client")
        {
            damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenum);
            Debug.Log(collision.name);
            collision.GetComponent<boss_hpbar>().Damage(damage);
        }
        
    }
}
