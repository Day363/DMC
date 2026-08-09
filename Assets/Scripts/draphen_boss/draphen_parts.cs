using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draphen_parts : MonoBehaviour
{
    public float speed;
    public bool turn;

    public void Start()
    {
        speed = Random.Range(30, 200);

        if (Random.Range(0, 2) == 0)
            speed *= -1;

        turn = true;
    }

    private void Update()
    {
        if (turn)
        {
            float z = transform.eulerAngles.z + speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, z);
        }
    }
}
