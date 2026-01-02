using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemobject : MonoBehaviour
{
    public bool israpport;
    public bool isitem;

    public Rapport rapport;
    public item item;
    public float pickupRadius;

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, pickupRadius);

        if (player.transform.tag == "Player")
        {
            if (israpport)
            {
                player.transform.GetComponent<player_inventory>().AddRapport(rapport);
                Destroy(gameObject);
            }
            else if (isitem)
            {
                player.transform.GetComponent<player_inventory>().ItemAdd(item);
                Destroy(gameObject);
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
