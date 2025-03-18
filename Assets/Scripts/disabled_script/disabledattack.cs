using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledattack : MonoBehaviour
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject player;
    public GameObject self;
    public int damage;
    public bool lightattack = false;
    public bool heavyattack = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!heavyattack && !lightattack)
        {
            if(collision.gameObject.tag == "playerattack")
            {
                self.GetComponent<PolygonCollider2D>().enabled = false;
            }
        }

        if (heavyattack)
        {
            heavyattack = false;
            player.GetComponent<playerhit>().StrongHit(damage, collision.transform);
            
        }

        if (lightattack)
        {
            lightattack = false;
            player.GetComponent<playerhit>().Hit(damage);
        }
    }
}
