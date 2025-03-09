using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabled_bomb : MonoBehaviour
{
    public int time;
    public int cooltime = 300;

    public void Update()
    {
        time = time + 1;

        if (time == cooltime)
        {
            Destroy(gameObject);
        }
    }
}
