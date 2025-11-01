using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour
{
    public float perangle;
    public float currentangle = 0;

    public void FixedUpdate()
    {
        currentangle += perangle;
        transform.rotation = Quaternion.Euler(0, 0, currentangle);
    }
}
