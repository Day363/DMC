using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    
    public float jumpPower;
    public bool isJump = false;
    public bool canmove = true;
    public int dir = 1;

    public Rigidbody2D rigid;

    public float knockbackDecay = 10f;
    public Vector3 knockback = Vector3.zero;

    void Awake()
    {
        if (canmove)
        {
            rigid = GetComponent<Rigidbody2D>();
            isJump = false;
        }
        
    }

    public void LookLeftenforce()
    {
        
        transform.localScale = new Vector3(-1, 1, 1);
        dir = -1;
        

    }

    public void LookRightenforce()
    {
        
        transform.localScale = new Vector3(1, 1, 1);
        dir = 1;
        

    }

    public void LookLeft()
    {
        if (canmove)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            dir = -1;
        }
        
    }

    public void LookRight()
    {
        if (canmove)
        {
            transform.localScale = new Vector3(1, 1, 1);
            dir = 1;
        }
        
    }

    public void Jump()
    {
        if (canmove)
        {
            if (!isJump)
            {
                isJump = true;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                GetComponent<Animator>().SetBool("jumpend", false);
                GetComponent<Animator>().SetBool("jump", true);
            }
        }
        
    }

    public void Stop()
    {
        GetComponent<Animator>().SetBool("running", false);
        GetComponent<Animator>().SetBool("idle", true);
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    void FixedUpdate()
    {
        if (canmove)
        {
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

        if (canmove)
        {
            rigid.velocity = new Vector3(GetComponent<playerinput>().h * GetComponent<playerstatus>().speed, rigid.velocity.y, 0);

        }
    }

   

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Floor") {
            isJump = false;
            GetComponent<Animator>().SetBool("jump", false);
            GetComponent<Animator>().SetBool("jumpend", true);
            GetComponent<Animator>().SetBool("knockback", false);
        }
    }

    public void Stiff(int second)
    {
        canmove = false;
        GetComponent<Animator>().SetBool("stiffness", true);
        StartCoroutine(Waitforstiff(second));
    }

    IEnumerator Waitforstiff(int csecond)
    {
        yield return new WaitForSeconds(csecond);
        GetComponent<Animator>().SetBool("stiffness", false);
        canmove = true;
    }

}