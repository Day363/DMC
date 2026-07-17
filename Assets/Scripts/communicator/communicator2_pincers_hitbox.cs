using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class communicator2_pincers_hitbox : MonoBehaviour
{
    public bool hit;
    public GameObject communicator2;
    public GameObject communicator1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hit)
        {
            hit = false;
            communicator2.GetComponent<Animator>().SetTrigger("focus2_trigger");
            
        }
    }
}
