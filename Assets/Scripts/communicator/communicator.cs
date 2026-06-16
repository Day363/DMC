using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.InputManagerEntry;

public class communicator : MonoBehaviour
{
    public GameObject player; 
    public GameObject box;
    public GameObject pierece;
    public GameObject wind;
    public GameObject effectpos;
    public GameObject effectpos2;

    public float moveSpeed;
    public float stopDistance;

    public bool walk;

    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartWalk();
    }

    public void FixedUpdate()
    {
        if (!walk) return;

        LookPlayer();

        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

        if (distance > stopDistance)
        {
            float dir = Mathf.Sign(player.transform.position.x - transform.position.x);
            rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            EndWalk();
        }
    }

    public void SmokeChance()
    {
        
        if (Random.Range(0, 75) == 0)
        {
            animator.SetTrigger("smoke");
        }
    }

    IEnumerator TestAttackReady()
    {
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("ready");
    }


    public void AttackAndWithNextAttack()
    {
        int i = Random.Range(0, 4);
        if (i == 0)
        {
            animator.SetTrigger("attack1");
        }
        else if (i == 1)
        {
            animator.SetTrigger("attack2");
        }
        else if (i == 2)
        {
            animator.SetTrigger("attack3");
        }
        else if (i == 3)
        {
            animator.SetTrigger("attack4");
        }
    }

    public void StartWalk()
    {
        walk = true;
        animator.SetBool("walk", true);
    }

    public void EndWalk()
    {
        walk = false;
        animator.SetBool("walk", false);
    }

    public void LookPlayer()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void AfterImage()
    {
        GetComponent<afterimagetest>().StartGenerate();
        //GetComponent<SpriteRenderer>().material = glitchmat;
    }

    public void EndAfterImage()
    {
        GetComponent<afterimagetest>().EndGenerate();
        //GetComponent<SpriteRenderer>().material = normalmat;

    }

    public void MoveLittleForwardTo()
    {
        BoxTrue();
        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(30f * Vector2.left, ForceMode2D.Impulse);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(30f * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void MoveForwardTo()
    {
        BoxTrue();
        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(50f * Vector2.left, ForceMode2D.Impulse);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(50f * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void MoveOverTo()
    {
        BoxFlase();
        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(100f * Vector2.left, ForceMode2D.Impulse);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(100f * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void MovebackTo()
    {
        BoxTrue();
        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(40f * Vector2.right, ForceMode2D.Impulse);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(40f * Vector2.left, ForceMode2D.Impulse);
        }
    }

    public void Pierece()
    {
        GameObject curp = Instantiate(pierece, effectpos.transform);
        curp.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void BoxTrue()
    {
        box.SetActive(true);
    }

    public void BoxFlase()
    {
        box.SetActive(false);
    }

    public void SpawnWind()
    {
        GameObject curwind = Instantiate(wind, effectpos.transform);
        curwind.transform.localPosition = Vector3.zero;
        GameObject curwind2 = Instantiate(wind, effectpos2.transform);
        curwind2.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        curwind2.transform.localPosition = Vector3.zero;
    }
}
