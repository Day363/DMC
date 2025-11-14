using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_script : MonoBehaviour
{
    public GameObject katana;
    public GameObject spear;
    public GameObject bulletpos;
    public GameObject bullet;
    public GameObject cam;
    public GameObject rainmanager;
    public GameObject[] rainshader;
    public GameObject player;
    public GameObject hitbox;
    public GameObject raindex;
    public List<string> weapons = new List<string>();
    public int rainmincooltime;
    public int raincool;
    public bool rain = false;
    public bool walk = true;
    public int range;
    public bool whileattack = false;
    public int direction;
    public Vector2 playerposition;
    public float movespeed;
    public int weakrushpower;
    public int rush1power;
    public int rush2power;
    public int backrushpower;
    public int backweakrushpower;
    public bool run = false;
    public float runspeed;
    public int teleportcooltime;
    public int teleportcool = 0;
    public int teleportrange;
    public int teleportposition;
    public int rangeteleportposition;
    public Vector2 teleportpos;
    public bool canmove = true;
    public Vector3 playerfloorpos;
    public Vector3 spearpos;
    public Vector3 spearteleportpos;
    public GameObject currentspear;
    public Vector3 katanapos;
    public Vector3 katanateleportpos;
    public GameObject currentkatana;
    public int attackint = 0;
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();

        boss_hpbar.OnCycleEnd += RainShoot;
    }

    public void FixedUpdate()
    {
        //if (animator.GetBool("canreplace"))
        //{
        //    if (weapons.Contains("rifle") && !weapons.Contains("bigsword"))
        //    {
        //        animator.SetBool("canreplace", false);
        //    }
        //    else if (!weapons.Contains("rifle") && weapons.Contains("bigsword"))
        //    {
        //        animator.SetBool("canreplace", false);
        //    }
        //    else if (weapons.Contains("spear") && !weapons.Contains("shootgun"))
        //    {
        //        animator.SetBool("canreplace", false);
        //    }
        //    else if(!weapons.Contains("spear") && weapons.Contains("shootgun"))
        //    {
        //        animator.SetBool("canreplace", false);
        //    }
        //}

        playerfloorpos = new Vector3(player.transform.position.x, 2, 0);
        spearpos = new Vector3(transform.position.x, transform.position.y + 1, 0);
        if (direction == -1)
        {
            katanapos = new Vector3(transform.position.x - 3, transform.position.y + 0.7f, 0);
        }
        else
        {
            katanapos = new Vector3(transform.position.x + 3, transform.position.y + 0.7f, 0);
        }

        if (weapons.Count <= 4)
        {
            raincool++;
        }

        if (teleportcool > teleportcooltime)
        {
            if (Vector2.Distance(player.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position) > teleportrange)
            {
                animator.SetBool("teleport", true);
                teleportcool = 0;
            }
        }
   

        if (raincool >= rainmincooltime)
        {
            animator.SetTrigger("rain");
        }

        Transform playerpos = player.transform;

        if (!whileattack)
        {
            if (playerpos.position.x < transform.position.x)
            {
                direction = -1;
                hitbox.GetComponent<indexer0_hitbox_script>().direction = -1;
            }

            if (playerpos.position.x > transform.position.x)
            {
                direction = 1;
                hitbox.GetComponent<indexer0_hitbox_script>().direction = 1;
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

        animator.SetFloat("walkrange", Mathf.Abs(transform.position.x - player.transform.position.x));

        if (Vector2.Distance(player.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position) > range)
        {

            animator.SetBool("range", true);
            animator.SetBool("melee", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("range", false);
            GetComponent<Animator>().SetBool("melee", true);
        }

        
        if (!whileattack && canmove)
        {
            if (walk)
            {
                teleportcool++;
                playerposition = new Vector2(player.transform.position.x, gameObject.transform.position.y);
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerposition, movespeed);
            }

            if (!walk && run)
            {
                playerposition = new Vector2(player.transform.position.x, gameObject.transform.position.y);
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerposition, runspeed);
            }
        }

        if (weapons.Contains("katana"))
        {
            animator.SetBool("katana", true);
        }

        if (weapons.Contains("rifle"))
        {
            animator.SetBool("rifle", true);
        }

        if (weapons.Contains("bigsword"))
        {
            animator.SetBool("bigsword", true);
        }

        if (weapons.Contains("spear"))
        {
            animator.SetBool("spear", true);
        }

        if (weapons.Contains("shootgun"))
        {
            animator.SetBool("shootgun", true);
        }
    }

    public void FuckAnimationEvent()
    {
        GetComponent<indexer0_counsel>().AttackEnd();
    }

    public void RainShoot()
    {
        raindex.GetComponent<indexer_line_core>().Shoot();
    }

    public void AddStack()
    {
        attackint++;
        if (weapons.Count > 4)
        {
            GetComponent<Animator>().SetBool("canreplace", true);
            attackint = 0;
        }
    }

    public void LookClinet()
    {
        cam.GetComponent<CameraManager>().LookEnemy();
    }

    public void LookPlayer()
    {
        cam.GetComponent<CameraManager>().LookPlayer();
    }

    public void Startrain()
    {
        rainmanager.GetComponent<indexer0_rainmanager>().DoRain();
        whileattack = true;
    }

    public void Landstart()
    {
        weapons.Add("katana");
        weapons.Add("rifle");
        weapons.Add("bigsword");
        weapons.Add("spear");
        weapons.Add("shootgun");
        raincool = 0;
        GetComponent<Animator>().ResetTrigger("rain");
        StartCoroutine(LandTime());
    }

    IEnumerator LandTime()
    {
        yield return new WaitForSeconds(0.15f);
        rainmanager.GetComponent<indexer0_rainmanager>().Land();
        rainmanager.GetComponent<indexer0_rainmanager>().rain = false;
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().SetTrigger("landend");
        yield return new WaitForSeconds(1f);
        whileattack = false;
    }

    public void StartRun()
    {
        walk = false;
        gameObject.GetComponent<Animator>().SetBool("walk", false);
        run = true;
        gameObject.GetComponent<Animator>().SetBool("run", true);
    }

    public void WeakRush()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * weakrushpower, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * weakrushpower, ForceMode2D.Impulse);

        }
    }

    public void Rush1()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * rush1power, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * rush1power, ForceMode2D.Impulse);

        }
    }

    public void Rush2()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * rush2power, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * rush2power, ForceMode2D.Impulse);

        }
    }

    public void WeakBackRush()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * backweakrushpower, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * backweakrushpower, ForceMode2D.Impulse);

        }
    }

    public void BackRush()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * backrushpower, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * backrushpower, ForceMode2D.Impulse);

        }
    }

    public void KatanaAttackStart()
    {
        whileattack = true;
    }

    public void KatanaAttackEnd()
    {
        whileattack = false;
        weapons.Remove("katana");
        GetComponent<Animator>().SetBool("katana", false);
    }

    public void RifleAttackStart()
    {
        whileattack = true;
    }

    public void RifleAttackEnd()
    {
        whileattack = false;
        weapons.Remove("rifle");
        GetComponent<Animator>().SetBool("rifle", false);
    }

    public void BigswordAttackStart()
    {
        whileattack = true;
    }

    public void BigswordAttackEnd()
    {
        whileattack = false;
        weapons.Remove("bigsword");
        GetComponent<Animator>().SetBool("bigsword", false);
    }

    public void SpearAttackStart()
    {
        whileattack = true;
    }

    public void SpearAttackEnd()
    {
        whileattack = false;
        weapons.Remove("spear");
        GetComponent<Animator>().SetBool("spear", false);
    }

    public void ShootgunAttackStart()
    {
        whileattack = true;
    }

    public void ShootgunAttackEnd()
    {
        whileattack = false;
        weapons.Remove("shootgun");
        GetComponent<Animator>().SetBool("shootgun", false);
    }

    public void RiflebigswordAttackStart()
    {
        whileattack = true;
    }

    public void RiflebigswordAttackEnd()
    {
        whileattack = false;
        weapons.Remove("rifle");
        weapons.Remove("bigsword");
        GetComponent<Animator>().SetBool("rifle", false);
        GetComponent<Animator>().SetBool("bigsword", false);
        GetComponent<Animator>().SetBool("canreplace", false);
    }

    public void SpearShootgunAttackStart()
    {
        whileattack = true;
    }

    public void SpearShootgunAttackEnd()
    {
        whileattack = false;
        weapons.Remove("spear");
        weapons.Remove("shootgun");
        GetComponent<Animator>().SetBool("spear", false);
        GetComponent<Animator>().SetBool("shootgun", false);
        GetComponent<Animator>().SetBool("canreplace", false);
    }

    public void PosTeleport()
    {
        if (direction == -1)
        {
            teleportpos = new Vector2(player.transform.position.x, gameObject.transform.position.y);
        }
        if (direction == 1)
        {
            teleportpos = new Vector2(player.transform.position.x, gameObject.transform.position.y);
        }

        gameObject.transform.position = teleportpos;
    }

    public void Teleport()
    {
        if (direction == -1)
        {
            teleportpos = new Vector2(player.transform.position.x + teleportposition, gameObject.transform.position.y);
        }
        if (direction == 1)
        {
            teleportpos = new Vector2(player.transform.position.x - teleportposition, gameObject.transform.position.y);
        }

        gameObject.transform.position = teleportpos;
    }

    public void RangeTeleport()
    {
        if (direction == -1)
        {
            teleportpos = new Vector2(player.transform.position.x + rangeteleportposition, gameObject.transform.position.y);
        }
        if (direction == 1)
        {
            teleportpos = new Vector2(player.transform.position.x - rangeteleportposition, gameObject.transform.position.y);
        }

        gameObject.transform.position = teleportpos;
    }

    public void TeleportEnd()
    {
        teleportcool = 0;
        GetComponent<Animator>().SetBool("teleport", false);
    }

    public void spearteleport()
    {
        spearteleportpos = new Vector3(currentspear.transform.position.x, transform.position.y, 0);
        gameObject.transform.position = spearteleportpos;
    }

    public void Speardisapear()
    {
        Destroy(currentspear);
    }

    public void Katanateleport()
    {
        katanateleportpos = new Vector3(currentkatana.transform.position.x, transform.position.y, 0);
        gameObject.transform.position = katanateleportpos;
    }

    public void Katanadisapear()
    {
        Destroy(currentkatana);
    }

    public void AttackEnd()
    {
        whileattack = false;
        canmove = false;
    }

    public void AttackStart()
    {
        whileattack = true;
        canmove = true;
    }

    public void RainmanagerCount()
    {
        rainmanager.GetComponent<indexer0_rainmanager>().passiverainint++;
    }

    public void Shoot()
    {
        if (direction == 1)
        {
            Instantiate(bullet, bulletpos.transform.position, Quaternion.identity);
        }

        if (direction == -1)
        {
            Instantiate(bullet, bulletpos.transform.position, Quaternion.Euler(0, 0, 180));
        }
    }

    public void Spearshoot()
    {
        Vector3 dir = playerfloorpos - spearpos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        currentspear = Instantiate(spear, spearpos, Quaternion.AngleAxis(angle + 180, Vector3.back));
        
    }

    public void KatanaShoot()
    {
        Vector3 dir = playerfloorpos - katanapos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        currentkatana = Instantiate(katana, katanapos, Quaternion.AngleAxis(angle + 180, Vector3.back));
    }
}
