using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class communicator2 : MonoBehaviour
{
    public static Action OnDissolve;

    public GameObject communicator;
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
    public GameObject playerpos;
    public GameObject campos;
    public GameObject smokeeffect;
    public int direction = 1;

    GameObject curslash;

    public float moveSpeed;
    public float stopDistance;

    public bool walk;
    public bool playerfix;

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
        if (playerfix)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.MovePosition(playerpos.transform.position);
        }


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

    public void Smokeoff()
    {
        smokeeffect.SetActive(false);
    }

    public void Camvib()
    {
        battalemanager.Instance.cameramanager.GetComponent<CameraManager>().CamVibration1();
    }

    public void ThrowCam()
    {
        battalemanager.Instance.cameramanager.GetComponent<CameraManager>().LookCounsel(campos);
    }

    public void PlayerFix()
    {
        if (direction == 1)
        {
            transform.position = new Vector3(player.transform.position.x + 1.5f,  transform.position.y, 0);
        }
        else if (direction == -1)
        {
            transform.position = new Vector3(player.transform.position.x - 1.5f, transform.position.y, 0);
        }
        

        playerfix = true;
        player.GetComponent<Animator>().SetTrigger("caught");
        player.GetComponent<SpriteRenderer>().sortingLayerName = "enemy";
        player.GetComponent<SpriteRenderer>().sortingOrder = -1;
        player.GetComponent<PlayerMove>().canmove = false;
    }

    public void Throw()
    {
        battalemanager.Instance.cameramanager.GetComponent<CameraManager>().LookCounsel(campos);
        player.GetComponent<PlayerMove>().canmove = false;
        playerfix = false;
        player.GetComponent<Animator>().SetTrigger("caught2");
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; 
        rb.AddForce(Vector2.left * 500f, ForceMode2D.Impulse);

        player.GetComponent<SpriteRenderer>().sortingLayerName = "player";
        player.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void ThrowEnd()
    {
        communicator.GetComponent<Animator>().SetTrigger("ready");
        GetComponent<Animator>().SetTrigger("ready");
        StartCoroutine(LookPlayerCam());
    }

    IEnumerator LookPlayerCam()
    {
        yield return new WaitForSeconds(4f);
        communicator.GetComponent<communicator>().trigger = true;
        player.GetComponent<Animator>().SetTrigger("standup");
        battalemanager.Instance.cameramanager.GetComponent<CameraManager>().LookPlayer();
    }

    public void PlayerAfterImage()
    {
        player.GetComponent<afterimagetest>().StartGenerate();
    }

    public void PlayerAfterImageEnd()
    {
        player.GetComponent<afterimagetest>().EndGenerate();
    }

    public void SmokeChance()
    {

        //if (UnityEngine.Random.Range(0, 75) == 0)
        //{
        //    animator.SetTrigger("cigarette");
        //}
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

    public void MoveLittlebackTo()
    {
        //BoxTrue();
        if (direction == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(30f * Vector2.right, ForceMode2D.Impulse);
        }
        else if (direction == -1)
        {
            GetComponent<Rigidbody2D>().AddForce(30f * Vector2.left, ForceMode2D.Impulse);
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
        GetComponent<boss_hpbar>().Attack();
    }

    public void AttackAndWithNextAttackPhase2()
    {
        GetComponent<boss_hpbar>().Attack();
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

    public void Phase2()
    {
        GetComponent<boss_hpbar>().PhaseUp();
        GetComponent<Animator>().SetTrigger("attack5");
    }

    IEnumerator TestAttackReady()
    {
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("ready");
    }
}
