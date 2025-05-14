using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapalhaloturn : MonoBehaviour
{
    public float turnspeed;
    public float currentangle;

    private void FixedUpdate()
    {
        currentangle = currentangle + turnspeed;
        transform.rotation = Quaternion.Euler(0, 0, currentangle);
    }
}
