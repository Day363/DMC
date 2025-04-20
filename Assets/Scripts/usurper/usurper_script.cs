using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usurper_script : MonoBehaviour
{
    public GameObject player;
    public GameObject hitbox;
    public GameObject trail;
    public GameObject baldoeffectpos;
    public GameObject baldowaringpos;
    public GameObject Baldoslash;
    public GameObject Baldoslash2;
    public GameObject baldowaring1;
    public GameObject baldowaring2;
    public GameObject baldoeffect;
    public GameObject usurpercam;
    public GameObject star;
    public GameObject range;
    public int awaypower;
    public int uppower;
    public int rangecooltime;
    public int rangecool = 0;
    public bool whileattack = false;
    public bool walk = false;
    public int direction;
    public float movespeed;
    public int attackrushpower;
    public int attackrange;
    public int rushattackrange;
    public int rushcooltime;
    public int rushcool = 0;
    public int rushpower;
    public int rushbackrange;
    public int rushnum = 0;
    public int baldorange;
    public int baldobackrange;
    public int walkspeed;
    public Vector2 baldopos;
    public Vector2 rushpos;
    public Vector2 playerposition;

    public void FixedUpdate()
    {

        Transform playerpos = player.GetComponent<Transform>();
        Transform disabledpos = GetComponent<Transform>();

        rangecool = rangecool + 1;
        if (rangecool >= rangecooltime)
        {
            GetComponent<Animator>().SetBool("rangeready", true);
        }

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

        if (!whileattack)
        {
            if (walk)
            {
                playerposition = new Vector2(player.transform.position.x, gameObject.transform.position.y);
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerposition, movespeed);
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

        if (Vector2.Distance(player.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position) < rushattackrange)
        {
            rushcool = rushcool + 1;
        }

        if (rushcool > rushcooltime)
        {
            GetComponent<Animator>().SetBool("rushtrigger", true);
            rushcool = 0;
        }

        if (rushnum == 3)
        {
            GetComponent<Animator>().SetBool("rushend", true);
        }

        if (Vector2.Distance(player.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position) > attackrange)
        {
            gameObject.GetComponent<Animator>().SetBool("walk", true);
            walk = true;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("walk", false);
            walk = false;
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

    public void Starappear()
    {
        star.SetActive(true);
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

    public void BaldoWaring()
    {
        if (direction == -1)
        {
            Instantiate(baldowaring1, baldowaringpos.transform);
        }
        else
        {
            Instantiate(baldowaring2, baldowaringpos.transform);
        }
    }

    public void BaldoDetected()
    {
        gameObject.GetComponent<Animator>().SetBool("baldoinrange", true);
        player.GetComponent<PlayerMove>().Stiff(4);
    }

    public void Counselcam()
    {
        usurpercam.GetComponent<Animator>().SetBool("counsel", true);
    }

    public void Counselcamend()
    {
        usurpercam.GetComponent<Animator>().SetBool("counsel", false);
    }

    public void Baldoeffect()
    {
        Vector3 effectpos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        Instantiate(baldoeffect, effectpos, Quaternion.identity);
    }

    public void Baldoend()
    {
        trail.SetActive(false);
        gameObject.GetComponent<Animator>().SetBool("baldoinrange", false);
    }

    public void Rush()
    {
        if (direction == -1)
        {
            rushpos = new Vector2(player.transform.position.x - rushbackrange, gameObject.transform.position.y);
        }

        if (direction == 1)
        {
            rushpos = new Vector2(player.transform.position.x + rushbackrange, gameObject.transform.position.y);
        }

        gameObject.transform.position = rushpos;
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * rushpower, ForceMode2D.Impulse);
        rushnum = rushnum + 1;
    }

    public void Rushend()
    {
        GetComponent<Animator>().SetBool("rushend", false);
        GetComponent<Animator>().SetBool("rushtrigger", false);
        rushcool = 0;
        rushnum = 0;
    }

    public void Rushendrush()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * -1 * rushpower, ForceMode2D.Impulse);
    }

    public void Misile()
    {
        star.GetComponent<usurper_star_script>().Attack4();
    }

    public void GoAway()
    {
        if (direction == -1)
        {
            player.GetComponent<playerhit>().FlyAway(-awaypower, uppower);
        }
        else
        {
            player.GetComponent<playerhit>().FlyAway(awaypower, uppower);
        }
    }

    public void Rangeready()
    {
        range.SetActive(true);
    }

    public void Range()
    {
        range.GetComponent<usurper_range>().StartShoot();
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
