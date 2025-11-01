using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class disabled_rushmanage : MonoBehaviour
{
    public GameObject battalemanager;
    public GameObject cammanager;
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
    public bool canrotate;

    public bool phase2 = false;
    public float phase2rushpower;
    public float phase2rushdistance;
    public int phase2skillselec;
    public float phase2attackrush;
    public float phase2attacbigkrush;
    public int deathattackcooltime;
    public int deathattckcool;

    public disabled_counsel disc;
    public Animator animator;
    public Rigidbody2D rb;

    public void Start()
    {
        cammanager.GetComponent<CameraManager>().enemy = gameObject;
        battalemanager.GetComponent<battalemanager>().currentenemy = gameObject;

    }

    public void FixedUpdate()
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

        if (!whileattack && canrotate)
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

        if (!phase2)
        {
            if (GetComponent<boss_hpbar>().maxhealth / 2 >= GetComponent<boss_hpbar>().currenthealth)
            {
                Start2Phase();
            }

            if (Vector2.Distance(player.GetComponent<Transform>().position, self.GetComponent<Transform>().position) > rushrange)
            {
                attack4cool = attack4cool + 1;
                attack5cool = attack5cool + 1;
                tomove = true;
                if (attack5cool > attack5cooldown)
                {
                    animator.SetBool("attack5", true);
                }

                if (attack4cool > attack4cooldown)
                {
                    animator.SetBool("attack4", true);
                }
            }

            if (Vector2.Distance(player.GetComponent<Transform>().position, self.GetComponent<Transform>().position) < rushrange)
            {
                tomove = false;

                if (skillselec == 1)
                {
                    animator.SetBool("attack1", true);
                }

                if (skillselec == 2)
                {
                    animator.SetBool("attack2", true);
                }

                if (skillselec == 3)
                {
                    animator.SetBool("attack6", true);
                }

                if (skillselec == 4)
                {
                    animator.SetBool("attack7", true);
                }

                if (skillselec == 5)
                {
                    animator.SetBool("attack8", true);
                }
            }


            if (attack4move)
            {
                attack4speed = Vector2.Distance(player.GetComponent<Transform>().position, self.GetComponent<Transform>().position) / attack4speedlow;

                if (direction == -1)
                {
                    rb.AddForce(Vector2.left * attack4speed, ForceMode2D.Impulse);
                }

                if (direction == 1)
                {
                    rb.AddForce(Vector2.right * attack4speed, ForceMode2D.Impulse);
                }


                if (nowflying)
                {
                    if (Vector2.Distance(new Vector2(0, self.GetComponent<Transform>().position.y), new Vector2(0, floor.GetComponent<Transform>().position.y)) < 10.6)
                    {
                        animator.SetBool("attack4end", true);
                    }
                }

            }

            if (tomove)
            {
                if (rushselec < 2)
                {
                    animator.SetBool("rush1", true);
                }

                if (rushselec > 1)
                {
                    animator.SetBool("rush2", true);
                }
            }

            if (attack3cool > attack3cooldown)
            {
                animator.SetBool("attack3", true);
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

        if (phase2)
        {
            deathattckcool = deathattckcool + 1;

            if(Vector2.Distance(player.transform.position, gameObject.transform.position) > phase2rushdistance)
            {
                tomove = true;
            }

            if (tomove)
            {
                if (rushselec < 2)
                {
                    animator.SetBool("2phase_rush1", true);
                }

                if (rushselec > 1)
                {
                    animator.SetBool("2phase_rush2", true);
                }
            }

            if (Vector2.Distance(player.transform.position, gameObject.transform.position) < phase2rushdistance)
            {
                tomove = false;
                if(deathattckcool >= deathattackcooltime)
                {
                    animator.SetBool("2phase_deathattack", true);
                }

                if (phase2skillselec == 1)
                {
                    animator.SetBool("2phase_attack1", true);
                }

                if (phase2skillselec == 2)
                {
                    animator.SetBool("2phase_attack2", true);
                }

                if (phase2skillselec == 3)
                {
                    animator.SetBool("2phase_attack3", true);
                }

                if (phase2skillselec == 4)
                {
                    animator.SetBool("2phase_attack4", true);
                }

            }
        }
        
    }

    public void Rush1()
    {

        rushselec = Random.Range(1, 3);

        if (direction < 0)
        {
            rb.AddForce(Vector2.left * rushpower, ForceMode2D.Impulse);
            animator.SetBool("rush1", false);
            animator.SetBool("rush2", false);
            
        }

        if (direction > 0)
        {
            rb.AddForce(Vector2.right * rushpower, ForceMode2D.Impulse);
            animator.SetBool("rush1", false);
            animator.SetBool("rush2", false);
            
        }

        
    }

    public void Attackrush()
    {
        if (direction < 0)
        {
            rb.AddForce(Vector2.left * attackrushpower, ForceMode2D.Impulse);
        }

        if (direction > 0)
        {
            rb.AddForce(Vector2.right * attackrushpower, ForceMode2D.Impulse);
        }
    }

    public void Whileattack()
    {
        whileattack = true;
    }

    public void Endattack()
    {
        whileattack = false;
    }

    public void Rushend()
    {
        whileattack = false;
        disc.AttackEnd();
    }

    public void Attack1end()
    {
        whileattack = false;
        animator.SetBool("attack1", false);
        skillselec = Random.Range(1, 6);
        disc.AttackEnd();
    }

    public void Attack2end()
    {
        whileattack = false;
        animator.SetBool("attack2", false);
        skillselec = Random.Range(1, 6);
        disc.AttackEnd();
    }

    public void Attack6end()
    {
        whileattack = false;
        animator.SetBool("attack6", false);
        skillselec = Random.Range(1, 6);
        disc.AttackEnd();
    }

    public void Attack7end()
    {
        whileattack = false;
        animator.SetBool("attack7", false);
        skillselec = Random.Range(1, 6);
        disc.AttackEnd();
    }

    public void Attack8end()
    {
        whileattack = false;
        animator.SetBool("attack8", false);
        skillselec = Random.Range(1, 6);
        disc.AttackEnd();
    }

    public void Attack3end()
    {
        animator.SetBool("attack3", false);
        whileattack = false;
        attack3cool = 0;
        disc.AttackEnd();
    }
    public void Attack5end()
    {
        animator.SetBool("attack5", false);
        whileattack = false;
        attack5cool = 0;
        disc.AttackEnd();
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
        rb.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
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
        animator.SetBool("attack4end", false);
        animator.SetBool("attack4", false);
        disc.AttackEnd();
    }

    public void Start2Phase()
    {
        animator.SetBool("2phase", true);
        phase2 = true;
    }

    public void Phase2Rush()
    {
        rushselec = Random.Range(1, 3);

        if (direction < 0)
        {
            rb.AddForce(Vector2.left * phase2rushpower, ForceMode2D.Impulse);
            animator.SetBool("2phase_rush1", false);
            animator.SetBool("2phase_rush2", false);

        }

        if (direction > 0)
        {
            rb.AddForce(Vector2.right * phase2rushpower, ForceMode2D.Impulse);
            animator.SetBool("2phase_rush1", false);
            animator.SetBool("2phase_rush2", false);

        }
    }

    public void Phase2Attack1end()
    {
        whileattack = false;
        animator.SetBool("2phase_attack1", false);
        phase2skillselec = Random.Range(1, 5);
        disc.AttackEnd();
    }

    public void Phase2Attack2end()
    {
        whileattack = false;
        animator.SetBool("2phase_attack2", false);
        phase2skillselec = Random.Range(1, 5);
        disc.AttackEnd();
    }

    public void Phase2Attack3end()
    {
        whileattack = false;
        animator.SetBool("2phase_attack3", false);
        phase2skillselec = Random.Range(1, 5);
        disc.AttackEnd();
    }

    public void Phase2Attack4end()
    {
        whileattack = false;
        animator.SetBool("2phase_attack4", false);
        phase2skillselec = Random.Range(1, 5);
        disc.AttackEnd();
    }

    public void DeathAttackend()
    {
        whileattack = false;
        animator.SetBool("2phase_deathattack", false);
        animator.SetBool("2phase_deathattack_success", false);
        deathattckcool = 0;
    }

    public void Phase2Attackrush()
    {
        if (direction < 0)
        {
            rb.AddForce(Vector2.left * phase2attackrush, ForceMode2D.Impulse);
        }

        if (direction > 0)
        {
            rb.AddForce(Vector2.right * phase2attackrush, ForceMode2D.Impulse);
        }
    }

    public void Phase2AttackBigrush()
    {
        if (direction < 0)
        {
            rb.AddForce(Vector2.left * phase2attacbigkrush, ForceMode2D.Impulse);
        }

        if (direction > 0)
        {
            rb.AddForce(Vector2.right * phase2attacbigkrush, ForceMode2D.Impulse);
        }
    }

    public void CamreaShackmethod()
    {
        cammanager.GetComponent<CameraManager>().LimitlessShake(13, 1, 0.1f);
    }

}
