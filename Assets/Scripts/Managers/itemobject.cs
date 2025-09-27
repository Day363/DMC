using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemobject : MonoBehaviour
{
    public Rapport rapport;
    public float pickupRadius;

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, pickupRadius);

        if (player.transform.tag == "Player")
        {
            player.transform.GetComponent<player_inventory>().rapportinv.Add(rapport);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
