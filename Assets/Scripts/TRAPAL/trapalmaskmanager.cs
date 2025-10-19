using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapalmaskmanager : MonoBehaviour
{
    public GameObject mask;
    public GameObject currentmask;
    public int randomint;
    public int intrange;
    public float minsize;
    public float maxsize;
    public float sizex;
    public float sizey;
    public float minpos;
    public float maxpos;
    public float posx;
    public float posy;
    public Vector3 pos;
    public Vector3 scale;

    public bool going = true;

    private void FixedUpdate()
    {
        if (going)
        {
            sizex = Random.Range(minsize, maxsize);
            sizey = Random.Range(minsize, maxsize);
            scale = new Vector3(sizex, sizey, 1);
            posx = Random.Range(minpos, maxpos);
            posy = Random.Range(minpos, maxpos);
            pos = new Vector3(posx, posy, 0);
            randomint = Random.Range(1, 101);
            if (randomint <= intrange)
            {
                currentmask = Instantiate(mask, pos, Quaternion.identity);
                currentmask.transform.localScale = scale;
                StartCoroutine(Maskdisappear(currentmask));
            }
        }
        
        
    }

    IEnumerator Maskdisappear(GameObject mask)
    {
        float randint = Random.Range(0.1f, 0.3f);
        yield return new WaitForSeconds(randint);
        Destroy(mask);
    }
}
