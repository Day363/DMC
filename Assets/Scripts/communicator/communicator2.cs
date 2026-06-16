using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class communicator2 : MonoBehaviour
{
    public static Action OnDissolve;

    public GameObject player;
    public GameObject wind;
    public GameObject effectpos;
    public GameObject effectpos2;
    public GameObject plainslash1;
    public GameObject plainslash2;
    public GameObject plainslash3;
    public GameObject plainslash4;
    public GameObject plainslash;
    public GameObject plainprephep;
    public GameObject mainslashcore;
    public int direction = 1;

    GameObject curslash;

    public float moveSpeed;
    public float stopDistance;

    public bool walk;

    Rigidbody2D rb;
    Animator animator;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //StartWalk();
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

        if (UnityEngine.Random.Range(0, 75) == 0)
        {
            animator.SetTrigger("smoke");
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
            direction = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            direction = -1;
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
        //BoxTrue();
        if (direction == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(30f * Vector2.left, ForceMode2D.Impulse);
        }
        else if (direction == -1)
        {
            GetComponent<Rigidbody2D>().AddForce(30f * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void MoveForwardTo()
    {
        //BoxTrue();
        if (direction == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(65f * Vector2.left, ForceMode2D.Impulse);
        }
        else if (direction == -1)
        {
            GetComponent<Rigidbody2D>().AddForce(65f * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void MoveOverTo()
    {
        //BoxFlase();
        if (direction == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(110f * Vector2.left, ForceMode2D.Impulse);
        }
        else if (direction == -1)
        {
            GetComponent<Rigidbody2D>().AddForce(110f * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void MoveOverTo2()
    {
        //BoxFlase();
        if (direction == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(200f * Vector2.left, ForceMode2D.Impulse);
        }
        else if (direction == -1)
        {
            GetComponent<Rigidbody2D>().AddForce(200f * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void MovebackTo()
    {
        //BoxTrue();
        if (direction == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(50f * Vector2.right, ForceMode2D.Impulse);
        }
        else if (direction == -1)
        {
            GetComponent<Rigidbody2D>().AddForce(50f * Vector2.left, ForceMode2D.Impulse);
        }
    }

    public void Disappear()
    {
        DOTween.Kill("communicatorappear");
        GetComponent<SpriteRenderer>().material.DOFloat(1, "_Dissolve", 0.5f).SetId("communicatorappear");
        EndAfterImage();
        OnDissolve?.Invoke();
        
    }

    public void Appear()
    {
        DOTween.Kill("communicatorappear");
        GetComponent<SpriteRenderer>().material.DOFloat(0, "_Dissolve", 0.5f).SetId("communicatorappear");
        AfterImage();
        
    }

    public void MoveBackToPlayer()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.position = new Vector3(player.transform.position.x - 13f, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.position = new Vector3(player.transform.position.x + 13f, transform.position.y, transform.position.z);
        }
    }

    public void AttackAndWithNextAttack()
    {
        int i = UnityEngine.Random.Range(0, 4);
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

    public void AttackAndWithNextAttackPhase2()
    {
        int i = UnityEngine.Random.Range(0, 4);
        if (i == 0)
        {
            animator.SetTrigger("phase2_attack1");
        }
        else if (i == 1)
        {
            animator.SetTrigger("phase2_attack2");
        }
        else if (i == 2)
        {
            animator.SetTrigger("phase2_attack3");
        }
        else if (i == 3)
        {
            animator.SetTrigger("phase2_attack4");
        }
    }

    public void PlainSlashTween()
    {
        plainslashcore slash = plainslash1.GetComponent<plainslashcore>();
        plainslashcore slash2 = plainslash2.GetComponent<plainslashcore>();
        plainslashcore slash3 = plainslash3.GetComponent<plainslashcore>();
        plainslashcore slash4 = plainslash4.GetComponent<plainslashcore>();

        DOTween.To(() => slash.time, x => slash.time = x, 0.01f, 2.5f).From(0.3f);
        DOTween.To(() => slash2.time, x => slash2.time = x, 0.01f, 2.5f).From(0.3f);
        DOTween.To(() => slash3.time, x => slash3.time = x, 0.01f, 2.5f).From(0.3f);
        DOTween.To(() => slash4.time, x => slash4.time = x, 0.01f, 2.5f).From(0.3f);
    }

    public void PlainSlashSpawn()
    {
        curslash = Instantiate(plainprephep, plainslash.transform.position, Quaternion.Euler(71.37f, 0, 0));
    }

    public void SwingSlash()
    {
        curslash.transform.DOMoveX((transform.position.x + 100) * -transform.localScale.x, 3.5f);
        Destroy(curslash, 4f);
    }

    public void MainSlashCoreOn()
    {
        mainslashcore.SetActive(true);
    }
}
