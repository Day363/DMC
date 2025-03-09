using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float movespeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * movespeed * Time.deltaTime, dir.y * movespeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
}
