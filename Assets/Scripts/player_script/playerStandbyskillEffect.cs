using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStandbyskillEffect : MonoBehaviour
{
    public GameObject effectpos;
    public GameObject[] effects;



    public void Effect1()
    {
        Debug.Log("r");
        GameObject cureffect = Instantiate(effects[0], effectpos.transform.position, effectpos.transform.rotation);
        if (GetComponent<PlayerMove>().dir == -1)
        {
            cureffect.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Effect1opposite()
    {
        GameObject cureffect = Instantiate(effects[0], effectpos.transform.position, effectpos.transform.rotation);
        cureffect.transform.localScale = new Vector3(-1, 1, 1);
        if (GetComponent<PlayerMove>().dir == -1)
        {
            cureffect.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
