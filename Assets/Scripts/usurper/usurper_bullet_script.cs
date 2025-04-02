using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usurper_bullet_script : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed;
    public float movespeed;
    public float rotateamount;
    public float rotatespeeddown;
    public float movespeeddown;
    public Vector2 moveDir;
    public Quaternion effectrot;
    public bool canmove = true;
    public bool canrotate = true;

    public void FixedUpdate()
    {
        if (canmove)
        {
            rotateSpeed = rotateSpeed + rotatespeeddown;
            movespeed = movespeed + movespeeddown;
            if (movespeed <= 0)
            {
                canmove = false;
                GetComponent<Animator>().SetBool("shoot", true);
            }
            GetComponent<Rigidbody2D>().velocity = transform.up * movespeed;
        }

        if (canrotate)
        {
            moveDir = (Vector2)target.transform.position - GetComponent<Rigidbody2D>().position;
            moveDir.Normalize();
            rotateamount = Vector3.Cross(moveDir, transform.up).z;
            GetComponent<Rigidbody2D>().angularVelocity = rotateamount * rotateSpeed;
        }
    }

    public void StopRotate()
    {
        canrotate = false;
        rotateSpeed = 0;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        
    }

    public void Shoot()
    {
       
    }

    public void Destroyself()
    {
        Destroy(gameObject);
    }

}
