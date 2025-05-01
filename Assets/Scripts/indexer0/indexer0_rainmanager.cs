using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_rainmanager : MonoBehaviour
{
    public GameObject rain1;
    public GameObject rain2;
    public bool rain = false;
    public Vector2 endpos1;
    public Vector2 endpos2;
    public Vector3 whereshoot;
   

    public void FixedUpdate()
    {
        if (rain)
        {
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain1, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain2, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain1, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain2, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain1, whereshoot, Quaternion.identity);
            whereshoot = new Vector3(Random.Range(endpos1.x, endpos2.x + 1), 35.5f, 0);
            Instantiate(rain2, whereshoot, Quaternion.identity);
        }
    }

    public void DoRain()
    {
        rain = true;
    }
}
