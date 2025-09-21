using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemobject : MonoBehaviour
{
    public Rapport rapport;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<player_inventory>().rapportinv.Add(rapport);
            Destroy(gameObject);
        }
    }
}
