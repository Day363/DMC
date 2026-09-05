using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draphen_circle : MonoBehaviour
{
    public float speed;
    public float curanagle;

    public void Start()
    {
        speed = Random.Range(-1f, 1f);
    }

    public void FixedUpdate()
    {
        curanagle += speed;
        transform.rotation = Quaternion.Euler(0, 0, curanagle);
    }
}
