using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapalhaloturnupdate : MonoBehaviour
{
    public float turnspeed;
    public float currentangle;

    private void Update()
    {
        currentangle = currentangle + turnspeed;

        transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, currentangle); 
    }
}
