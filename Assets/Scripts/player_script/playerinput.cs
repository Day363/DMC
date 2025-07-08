using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerinput : MonoBehaviour
{
    public float h = 0;

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            GetComponent<PlayerMove>().LookLeft();
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            GetComponent<PlayerMove>().LookRight();
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            GetComponent<PlayerMove>().Stop();
        }

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<PlayerMove>().Jump();
        }

        h = Input.GetAxisRaw("Horizontal");
    }
}
