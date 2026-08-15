using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draphen_boss : MonoBehaviour
{
    public GameObject player;
    public bool walk;
    public float walkspped;
    public bool firstmet;
    public int direction = -1;

    public Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Animator>().SetTrigger("walk");
        walk = true;
    }

    public void Update()
    {
        if (!firstmet && Vector3.Distance(transform.position, player.transform.position) < 10)
        {
            firstmet = true;
            walk = false;
            battalemanager.Instance.Battlestart();
            GetComponent<boss_hpbar>().Attack();
            
        }

        if (walk)
        {
            rb.velocity = new Vector3(walkspped * -transform.localScale.x, rb.velocity.y);
        }
    }

    

    public void LookPlayer()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            direction = 1;
        }
        else if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            direction = -1;
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
        
        GetComponent<Rigidbody2D>().AddForce(direction * 50f * Vector2.left, ForceMode2D.Impulse);
        
    }

    public void MoveForwardTo()
    {
        GetComponent<Rigidbody2D>().AddForce(direction * 80f * Vector2.left, ForceMode2D.Impulse);
    }

    public void MoveOverTo()
    {
        GetComponent<Rigidbody2D>().AddForce(direction * 150f * Vector2.left, ForceMode2D.Impulse);
    }

    public void MovebackTo()
    {
        GetComponent<Rigidbody2D>().AddForce(direction * 40f * Vector2.right, ForceMode2D.Impulse);
    }

    public void Sheathing()
    {
        GetComponent<Animator>().SetBool("sheathed", true);
    }

    public void Baldo()
    {
        GetComponent<Animator>().SetBool("sheathed", false);
    }

    public void NextAttack()
    {
        GetComponent<boss_hpbar>().Attack();
    }
}
