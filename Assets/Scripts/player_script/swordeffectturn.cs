using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordeffectturn : MonoBehaviour
{
    public float rot = 0;
    public float turnspeed;
    private void Update()
    {
        rot += turnspeed;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
}
