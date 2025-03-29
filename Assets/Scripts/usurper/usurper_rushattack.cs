using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usurper_rushattack : MonoBehaviour
{
    public GameObject player;
    public GameObject self;
    public GameObject rushhitbox;
    public GameObject usurper;
    public int damage;
    public bool lightattack = false;
    public bool heavyattack = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerattack")
        {
            usurper.GetComponent<Animator>().SetBool("rushend", true);
            self.GetComponent<PolygonCollider2D>().enabled = false;
            rushhitbox.SetActive(false);
        }
    }
    
}
