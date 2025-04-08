using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usurper_backgroundflow : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);
    }
}
