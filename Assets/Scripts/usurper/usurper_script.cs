using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usurper_script : MonoBehaviour
{
    public GameObject player;
    public GameObject hitbox;
    public GameObject trail;
    public GameObject baldoeffectpos;
    public GameObject Baldoslash;
    public GameObject Baldoslash2;
    public bool whileattack = false;
    public bool walk = false;
    public int direction;
    public int attackrushpower;
    public int attackrange;
    public int baldorange;
    public int baldobackrange;
    public int walkspeed;
    public Vector2 baldopos;

    public void Update()
    {

        Transform playerpos = player.GetComponent<Transform>();
        Transform disabledpos = GetComponent<Transform>();

        if (!whileattack)
        {
            if (playerpos.position.x < disabledpos.position.x)
            {
                direction = -1;
                hitbox.GetComponent<usurper_hitbox>().direction = -1;
            }

            if (playerpos.position.x > disabledpos.position.x)
            {
                direction = 1;
                hitbox.GetComponent<usurper_hitbox>().direction = 1;
            }
        }

        if (whileattack)
        {
            if (walk)
            {

            }
        }



        if (!whileattack)
        {
            if (direction < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (direction > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (Vector2.Distance(player.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position) > attackrange)
        {
            gameObject.GetComponent<Animator>().SetBool("walk", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("walk", false);
        }

        if (Vector2.Distance(player.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position) > baldorange)
        {
            gameObject.GetComponent<Animator>().SetBool("baldo", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("baldo", false);
        }
    }

    public void startRush()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * attackrushpower, ForceMode2D.Impulse);
        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * attackrushpower, ForceMode2D.Impulse);
        }
    }

    public void WhileAttack()
    {
        whileattack = true;
    }

    public void EndAttack()
    {
        whileattack = false;
    }

    public void StartTrail()
    {
        trail.SetActive(true);
    }

    public void StartBaldo()
    {
        if (direction == -1)
        {
            baldopos = new Vector2(player.transform.position.x - baldobackrange, gameObject.transform.position.y);
        }

        if (direction == 1)
        {
            baldopos = new Vector2(player.transform.position.x + baldobackrange, gameObject.transform.position.y);
        }

        gameObject.transform.position = baldopos;

    }

    public void FailBaldo()
    {
        if (direction == -1)
        {
            Instantiate(Baldoslash, baldoeffectpos.transform);
        }
        else
        {
            Instantiate(Baldoslash2, baldoeffectpos.transform);
        }
        
    }

    public void BaldoDetected()
    {
        gameObject.GetComponent<Animator>().SetBool("baldoinrange", true);
        player.GetComponent<PlayerMove>().canmove = false;
        player.GetComponent<Animator>().SetBool("stiffness", true);
    }

    public void Baldoend()
    {
        trail.SetActive(false);
        gameObject.GetComponent<Animator>().SetBool("baldoinrange", false);
    }

    public void Walk()
    {
        walk = true;
    }

    public void Stop()
    {
        walk = false;
    }
}
