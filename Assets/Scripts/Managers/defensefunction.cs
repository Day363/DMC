using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defensefunction : MonoBehaviour
{
    public GameObject player;

    public bool defense;
    public bool counter;
    public bool evasion;
    public bool immobility;
    public bool offset;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemyattack")
        {
            if (defense)
            {

            }
            else if (counter)
            {

            }
            else if (evasion)
            {

            }
            else if (offset)
            {

            }
        }
    }
}
