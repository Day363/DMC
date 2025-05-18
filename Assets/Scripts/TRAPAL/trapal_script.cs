using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_script : MonoBehaviour
{
    public GameObject lazer;
    public GameObject pointattack;
    public GameObject barrier;
    public float lazercool = 0;
    public float lazercooltime;
    public float pointattackcool = 0;
    public float pointattackcooltime;
    public float barriercool = 0;
    public float barriercooltime;

    private void FixedUpdate()
    {
        lazercool++;
        pointattackcool++;
        barriercool++;

        if (lazercool >= lazercooltime)
        {
            lazercool = 0;
            lazer.GetComponent<trapal_lazer>().LazerStart();
        }

        if (pointattackcool >= pointattackcooltime)
        {
            pointattackcool = 0;
            Instantiate(pointattack, new Vector3(Random.Range(-15, 16), 0, 0), Quaternion.identity);
        }

        if (barriercool >= barriercooltime)
        {
            barriercool = 0;
            barrier.GetComponent<trapal_barreirtext>().StartBarrier();
        }
    }


}
