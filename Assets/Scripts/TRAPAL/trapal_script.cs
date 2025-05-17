using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_script : MonoBehaviour
{
    public GameObject lazer;
    public GameObject pointattack;
    public float lazercool = 0;
    public float lazercooltime;
    public float pointattackcool = 0;
    public float pointattackcooltime;

    private void FixedUpdate()
    {
        lazercool++;
        pointattackcool++;
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
    }


}
