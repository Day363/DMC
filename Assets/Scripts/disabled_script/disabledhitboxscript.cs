using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledhitboxscript : MonoBehaviour
{
    public int direction = 0;

    private void Update()
    {
        if (direction == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
