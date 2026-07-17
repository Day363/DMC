using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
    public GameObject counselcam;
    public GameObject smokeeffect;
    public GameObject eyeeffect;

    public GameObject communicator2;
    public GameObject slashcore2;

    public float moveSpeed;
    public float stopDistance;

    public bool walk;
    public bool firstmet;
    public bool ready;
    public bool attackready;
    public bool trigger;

    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.speed = 0.75f;
        //StartWalk();
    }

    public void FixedUpdate()
    {
        if (trigger && firstmet && !attackready && ready && transform.position.x < player.transform.position.x + 24)
        {
            attackready = true;
            battalemanager.Instance.Battlestart();
            AttackAndWithNextAttack();
            communicator2.GetComponent<communicator2>().AttackAndWithNextAttack();
            communicator2.GetComponent<communicator2>().battlestart = true;
            communicator2.GetComponent<communicator2>().xstop = (int)(communicator2.transform.position.x + 75);
            communicator2.GetComponent<communicator2>().minusxstop = (int)(communicator2.transform.position.x - 75);
            communicator2.GetComponent<communicator2>().SpawnWall();
        }

        if (firstmet && !ready && transform.position.x < player.transform.position.x + 4)
        {
            ready = true;
            counselcam.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            battalemanager.Instance.cameramanager.GetComponent<CameraManager>().LookCounsel(counselcam);
            uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<PlayerMove>().Stop();
            battalemanager.Instance.gameObject.GetComponent<chatmanager>().CallDialogue(18);
        }

        if (!firstmet && Vector2.Distance(gameObject.transform.position, player.transform.position) < 13)
        {
            battalemanager.Instance.currentenemys.Add(gameObject);
            battalemanager.Instance.currentenemys.Add(communicator2);


            firstmet = true;
            counselcam.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            battalemanager.Instance.cameramanager.GetComponent<CameraManager>().LookCounsel(counselcam);
            uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<PlayerMove>().Stop();
            battalemanager.Instance.gameObject.GetComponent<chatmanager>().CallDialogue(17);

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

    public void SmokeChance()
    {
        
        //if (Random.Range(0, 75) == 0)
        //{
        //    animator.SetTrigger("smoke");
        //}
    }

    IEnumerator TestAttackReady()
    {
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("ready");
    }


    public void AttackAndWithNextAttack()
    {
        GetComponent<boss_hpbar>().Attack();
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

    public void Focus1()
    {
        GameObject currenteffect = Instantiate(eyeeffect, effectpos.transform);
        currenteffect.transform.localPosition = Vector3.zero;
    }

    public void Focus1_2()
    {
        boss_hpbar bh = GetComponent<boss_hpbar>();
        bh.barrierAdd((int)((bh.maxhealth - bh.currenthealth) * 0.35f));
        GetComponent<communicator_passive>().focus1trigger = true;
    }

    public void CamVib()
    {
        battalemanager.Instance.cameramanager.GetComponent<CameraManager>().CamVibration0_5();
    }

    public void Helpattack()
    {
        animator.speed = 1;
        animator.SetTrigger("helpattack");
        
    }

    public void Communicator1_slashCoreTrigger()
    {
        boss_hpbar.StackInstance playerStackInstance1 = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "구심회력");
        if (playerStackInstance1 != null)
        {
            StartCoroutine(SlashcoreOn());
        }
    }

    IEnumerator SlashcoreOn()
    {
        slashcore2.SetActive(true);
        Debug.Log("ON");
        yield return new WaitForSeconds(0.1f);
        slashcore2.SetActive(false);
        Debug.Log("OFF");
    }

}
