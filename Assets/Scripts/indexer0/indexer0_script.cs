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
    boss_hpbar hpBar;

    string[] weaponNames =
    {
        "카타나",
        "대검",
        "창",
        "샷건",
        "라이플"
    };

    public void Start()
    {
        animator = GetComponent<Animator>();

        boss_hpbar.OnCycleEnd += RainShoot;
        WeaponEffect();
        hpBar = GetComponent<boss_hpbar>();
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[15], 1);
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[16], 1);
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[17], 1);
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[18], 1);
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[19], 1);
    }

    public void SoundPlay(string soundname)
    {
        battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay(soundname);
    }

    public void WeaponEffect()
    {

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

        int count = 0;
        foreach (string weapon in weaponNames)
        {
            var stack = hpBar.activeStacks.Find(s => s.stackData.effectName == weapon);
            if (stack != null)
            {
                count += stack.currentStack;
            }
        }
        if (count <= 4)
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
            }

            if (playerpos.position.x > transform.position.x)
            {
                direction = 1;
            }
        }

        if (!whileattack)
        {
            if (direction < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (direction > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
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

        

        if (GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "카타나") != null)
        {
            animator.SetBool("katana", true);
        }

        if (GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "라이플") != null)
        {
            animator.SetBool("rifle", true);
        }

        if (GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "대검") != null)
        {
            animator.SetBool("bigsword", true);
        }

        if (GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "창") != null)
        {
            animator.SetBool("spear", true);
        }

        if (GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "샷건") != null)
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
        int count = 0;
        foreach (string weapon in weaponNames)
        {
            var stack = hpBar.activeStacks.Find(s => s.stackData.effectName == weapon);
            if (stack != null)
            {
                count += stack.currentStack;
            }
        }
        if (count > 4)
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
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[15], 1);
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[16], 1);
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[17], 1);
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[18], 1);
        GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[19], 1);
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
        GetComponent<boss_hpbar>().RemoveStack(GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "카타나").stackData, 1);
        GetComponent<Animator>().SetBool("katana", false);
    }

    public void RifleAttackStart()
    {
        whileattack = true;
    }

    public void RifleAttackEnd()
    {
        whileattack = false;
        GetComponent<boss_hpbar>().RemoveStack(GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "라이플").stackData, 1);
        GetComponent<Animator>().SetBool("rifle", false);
    }

    public void BigswordAttackStart()
    {
        whileattack = true;
    }

    public void BigswordAttackEnd()
    {
        whileattack = false;
        GetComponent<boss_hpbar>().RemoveStack(GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "대검").stackData, 1);
        GetComponent<Animator>().SetBool("bigsword", false);
    }

    public void SpearAttackStart()
    {
        whileattack = true;
    }

    public void SpearAttackEnd()
    {
        whileattack = false;
        GetComponent<boss_hpbar>().RemoveStack(GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "창").stackData, 1);
        GetComponent<Animator>().SetBool("spear", false);
    }

    public void ShootgunAttackStart()
    {
        whileattack = true;
    }

    public void ShootgunAttackEnd()
    {
        whileattack = false;
        GetComponent<boss_hpbar>().RemoveStack(GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "샷건").stackData, 1);
        GetComponent<Animator>().SetBool("shootgun", false);
    }

    public void RiflebigswordAttackStart()
    {
        whileattack = true;
    }

    public void RiflebigswordAttackEnd()
    {
        whileattack = false;
        GetComponent<boss_hpbar>().RemoveStack(GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "라이플").stackData, 1);
        GetComponent<boss_hpbar>().RemoveStack(GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "대검").stackData, 1);
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
        GetComponent<boss_hpbar>().RemoveStack(GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "창").stackData, 1);
        GetComponent<boss_hpbar>().RemoveStack(GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "샷건").stackData, 1);
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
        currentspear.GetComponent<enemyattack>().enemy = gameObject;
        currentspear.GetComponent<enemyattack>().player = battalemanager.Instance.player;
    }

    public void KatanaShoot()
    {
        Vector3 dir = playerfloorpos - katanapos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        currentkatana = Instantiate(katana, katanapos, Quaternion.AngleAxis(angle + 180, Vector3.back));
        currentkatana.GetComponent<enemyattack>().enemy = gameObject;
        currentkatana.GetComponent<enemyattack>().player = battalemanager.Instance.player;
    }
}
