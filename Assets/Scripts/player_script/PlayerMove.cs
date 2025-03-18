using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public bool isJump = false;
    public bool canmove = true;
    public int dir = 1;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        isJump = false;
    }

    void Update()
    {
        if (canmove)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                dir = -1;
            }

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                dir = 1;
            }

            //Jump
            if (Input.GetButtonDown("Jump") && !isJump)
            {
                isJump = true;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                GetComponent<Animator>().SetBool("jumpend", false);
                GetComponent<Animator>().SetBool("jump", true);
            }

            //stopspeed
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            }

            if (GetComponent<Rigidbody2D>().velocity.normalized.x == 0)
            {
                GetComponent<Animator>().SetBool("running", false);
                GetComponent<Animator>().SetBool("idle", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("running", true);
                GetComponent<Animator>().SetBool("idle", false);
            }
        }
    }


    void FixedUpdate()      
    {
        if (canmove)
        {
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            

            if (rigid.velocity.x > maxSpeed)
            {
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            }

            else if (rigid.velocity.x < maxSpeed * (-1))
            {
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            }
        }
    }



    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Floor") {
            isJump = false;
            GetComponent<Animator>().SetBool("jump", false);
            GetComponent<Animator>().SetBool("jumpend", true);
            GetComponent<Animator>().SetBool("knockback", false);
            canmove = true;
        }
    }

}