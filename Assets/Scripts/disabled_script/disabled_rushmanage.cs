using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabled_rushmanage : MonoBehaviour
{
    public float rushpower;
    public float attackrushpower;
    public float jumppower;
    public int direction;
    public float rushrange;
    public bool tomove = false;
    public bool attack4move = false;
    public GameObject player;
    public GameObject hitbox;
    public GameObject self;
    public GameObject floor;
    public bool whileattack = false;
    public int rushselec = 1;
    public int skillselec = 1;
    public int attack3cool = 0;
    public int attack3cooldown;
    public int attack4cool = 0;
    public int attack4cooldown;
    public int attack5cool = 0;
    public int attack5cooldown;
    public Transform bulletspawnpoint;
    public GameObject bullet;
    public GameObject bomb;
    public bool bombtime = false;
    public int bombgo = 0;
    public float attack4speedlow;
    public float attack4distance;
    public float attack4speed;
    public bool nowflying = false;


    public void Update()
    {
        
        Transform playerpos = player.GetComponent<Transform>();
        Transform disabledpos = GetComponent<Transform>();

        if (!whileattack)
        {
            if (playerpos.position.x < disabledpos.position.x)
            {
                direction = -1;
                hitbox.GetComponent<disabledhitboxscript>().direction = -1;
            }

            if (playerpos.position.x > disabledpos.position.x)
            {
                direction = 1;
                hitbox.GetComponent<disabledhitboxscript>().direction = 1;
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

        if (Vector2.Distance(player.GetComponent<Transform>().position, self.GetComponent<Transform>().position) > rushrange)
        {
            attack4cool = attack4cool + 1;
            attack5cool = attack5cool + 1;
            tomove = true;
            if (attack5cool > attack5cooldown)
            {
                GetComponent<Animator>().SetBool("attack5", true);
            }

            if (attack4cool > attack4cooldown)
            {
                GetComponent<Animator>().SetBool("attack4", true);
            }
        }

        if (Vector2.Distance(player.GetComponent<Transform>().position, self.GetComponent<Transform>().position) < rushrange)
        {
            tomove = false;
            
            if (skillselec == 1)
            {
                GetComponent<Animator>().SetBool("attack1", true);
            }

            if (skillselec == 2)
            {
                GetComponent<Animator>().SetBool("attack2", true);
            }

            if (skillselec == 3)
            {
                GetComponent<Animator>().SetBool("attack6", true);
            }

            if (skillselec == 4)
            {
                GetComponent<Animator>().SetBool("attack7", true);
            }

            if (skillselec == 5)
            {
                GetComponent<Animator>().SetBool("attack8", true);
            }
        }


        if (attack4move)
        {
            attack4speed = Vector2.Distance(player.GetComponent<Transform>().position, self.GetComponent<Transform>().position) / attack4speedlow;
            
            if (direction == -1)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * attack4speed, ForceMode2D.Impulse);
            }
            
            if (direction == 1)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * attack4speed, ForceMode2D.Impulse);
            }
            

            if (nowflying)
            {
                if (Vector2.Distance(new Vector2(0, self.GetComponent<Transform>().position.y), new Vector2(0, floor.GetComponent<Transform>().position.y)) < 10.6)
                {
                    GetComponent<Animator>().SetBool("attack4end", true);
                }
            }
            
        }

        if (tomove)
        {
            if (rushselec < 2)
            {
                GetComponent<Animator>().SetBool("rush1", true);
            }

            if (rushselec > 1)
            {
                GetComponent<Animator>().SetBool("rush2", true);
            }
        }

        if (attack3cool > attack3cooldown)
        {
            GetComponent<Animator>().SetBool("attack3", true);
        }

        

        if (bombtime)
        {
            bombgo = bombgo + 1;

            if (bombgo == 100)
            {
                Instantiate(bomb, new Vector3(player.transform.position.x, -1, 0), Quaternion.identity);
            }

            if (bombgo == 130)
            {
                Instantiate(bomb, new Vector3(player.transform.position.x, -1, 0), Quaternion.identity);
            }

            if (bombgo == 190)
            {
                Instantiate(bomb, new Vector3(player.transform.position.x, -1, 0), Quaternion.identity);
            }

            if (bombgo == 230)
            {
                Instantiate(bomb, new Vector3(player.transform.position.x, -1, 0), Quaternion.identity);
            }

            if (bombgo == 240)
            {
                Instantiate(bomb, new Vector3(player.transform.position.x, -1, 0), Quaternion.identity);
            }

            if (bombgo == 250)
            {
                Instantiate(bomb, new Vector3(player.transform.position.x, -1, 0), Quaternion.identity);
            }

            if (bombgo == 300)
            {
                Instantiate(bomb, new Vector3(player.transform.position.x, -1, 0), Quaternion.identity);
                bombtime = false;
                bombgo = 0;
            }
        }

        attack3cool = attack3cool + 1;
    }

    public void Rush1()
    {

        rushselec = Random.Range(1, 3);

        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * rushpower, ForceMode2D.Impulse);
            GetComponent<Animator>().SetBool("rush1", false);
            GetComponent<Animator>().SetBool("rush2", false);
            
        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * rushpower, ForceMode2D.Impulse);
            GetComponent<Animator>().SetBool("rush1", false);
            GetComponent<Animator>().SetBool("rush2", false);
            
        }
    }

    public void Attackrush()
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

    public void Whileattack()
    {
        whileattack = true;
    }

    public void Rushend()
    {
        whileattack = false;
    }

    public void Attack1end()
    {
        whileattack = false;
        GetComponent<Animator>().SetBool("attack1", false);
        skillselec = Random.Range(1, 6);
    }

    public void Attack2end()
    {
        whileattack = false;
        GetComponent<Animator>().SetBool("attack2", false);
        skillselec = Random.Range(1, 6);
    }

    public void Attack6end()
    {
        whileattack = false;
        GetComponent<Animator>().SetBool("attack6", false);
        skillselec = Random.Range(1, 6);
    }

    public void Attack7end()
    {
        whileattack = false;
        GetComponent<Animator>().SetBool("attack7", false);
        skillselec = Random.Range(1, 6);
    }

    public void Attack8end()
    {
        whileattack = false;
        GetComponent<Animator>().SetBool("attack8", false);
        skillselec = Random.Range(1, 6);
    }

    public void Attack3end()
    {
        GetComponent<Animator>().SetBool("attack3", false);
        whileattack = false;
        attack3cool = 0;
    }
    public void Attack5end()
    {
        GetComponent<Animator>().SetBool("attack5", false);
        whileattack = false;
        attack5cool = 0;
    }

    public void shot()
    {
        Instantiate(bullet, bulletspawnpoint);
    }

    public void Bomb()
    {
        bombtime = true;
    }

    public void Startrun()
    {
        attack4move = true;
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
    }

    public void Nowflying()
    {
        nowflying = true;
    }

    public void Attack4end()
    {
        attack4move = false;
        nowflying = false;
        whileattack =  false;
        attack4cool = 0;
        GetComponent<Animator>().SetBool("attack4end", false);
        GetComponent<Animator>().SetBool("attack4", false);
    }
}
