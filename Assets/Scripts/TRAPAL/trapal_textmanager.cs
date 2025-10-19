using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_textmanager : MonoBehaviour
{
    public GameObject mask;
    public int randomint;
    public int intrange;
    public float minpos;
    public float maxpos;
    public float posx;
    public float posy;
    public Vector3 pos;

    public bool going = true;

    private void FixedUpdate()
    {
        if (going)
        {
            posx = Random.Range(minpos, maxpos);
            posy = Random.Range(minpos, maxpos);
            pos = new Vector3(posx, posy, 0);
            randomint = Random.Range(1, 101);
            if (randomint <= intrange)
            {
                Instantiate(mask, pos, Quaternion.identity);
            }
        }
        
    }
}
