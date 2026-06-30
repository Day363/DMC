using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;
using static UnityEditor.PlayerSettings;

public class trapal_script : MonoBehaviour
{
    public Animator animator;

    public boss_hpbar BossstackHander;
    public GameObject bomb_bullet;
    public GameObject trapal_weapon1;
    public GameObject trapal_point;
    public Vector3 lazer1point;
    public GameObject cammanager;
    public GameObject gamemanager;
    public GameObject player;
    public GameObject lazer2;
    public GameObject lazer;
    public GameObject pointattack;
    public GameObject barrier;
    public GameObject gun;
    public GameObject eye;
    public GameObject eyeflash;
    public GameObject eyeslash;
    public GameObject lazer1;
    public GameObject sword;
    public GameObject denycore;
    public GameObject warning;
    public GameObject bombshoot;
    public bool lazer2time;
    public bool swordtime;
    public float weapon1cool = 0;
    public float weapon1cooltime;
    public float lazer1cool = 0;
    public float lazer1cooltime;
    public float lazercool = 0;
    public float lazercooltime;
    public float pointattackcool = 0;
    public float pointattackcooltime;
    public float barriercool = 0;
    public float barriercooltime;
    public float guncool = 0;
    public float guncooltime;
    public float bombcool = 0;
    public float bombcooltime;
    public float gunrandomx1;
    public float gunrandomx2;
    public int gunmaxint;
    public int gunint;
    public float eyecool = 0;
    public float eyecooltime;
    public int randomeyeint;
    public float eyerandomx;
    public float eyerandomy;
    public bool canattack = true;
    public float spawnTime;        
    public float startInterval;     
    public float endInterval;
    private float currentInterval;
    

    public Vector3 targetPosition = new Vector3(0f, 3.46f, 0f);  
    public float duration = 2f;
    public bool backtrigger = false;

    public GameObject deny;
    public int dir = 1;
    public bool phase2;
    public GameObject lazerpos;
    public GameObject phase2lazer;
    public GameObject phase2currentlazer;
    public bool whileattack;
    //public float phase2lazercooltime;
    //public float phase2lazercool;
    //public float phase2attack1cooltime;
    //public float phase2attack1cool;
    //public float phase2attack2cooltime;
    //public float phase2attack2cool;
    public float phase2attack1cooltime;
    public float phase2attack1cool = 0;
    public List<GameObject> lazer2s = new List<GameObject> { };
    public int attack1count;
    public GameObject trapal_slash;
    public GameObject trapal_slash_warning;
    public GameObject trapal_attack2_slash;
    public List<int> attack2angle = new List<int> { };
    public List<Vector3> attack2pos = new List<Vector3> { };
    public GameObject currenatcore;
    public GameObject spaceslash;
    public GameObject phase2currentlazer1;
    public GameObject shaft;
    public GameObject currentshaft;
    public bool fix = true;
    public GameObject dashslasheffect;
    public GameObject flash;
    public GameObject particle1;
    public Material normalmat;
    public Material glitchmat;
    public GameObject concern;
    public GameObject text1;
    public GameObject curtext1;
    public GameObject lazer2small;
    public GameObject curlazer2small;
    public GameObject effectpos;
    public GameObject effectpos2;
    public GameObject pierece;
    public GameObject box;
    public GameObject wind;
    public GameObject slashcore;
         
    public void Start()
    {
        animator = GetComponent<Animator>();

        gamemanager = battalemanager.Instance.gameObject;
        gamemanager.GetComponent<battalemanager>().currentenemys.Add(gameObject);
    }

    private void FixedUpdate()
    {
        if (fix)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        

        if (!phase2)
        {
            if (lazer2time)
            {
                lazer2time = false;
                Lazer2spwan();
            }

            if (swordtime)
            {
                swordtime = false;
                SwordSpawn();
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("collapse"))
            {
                if (fix)
                {
                    GetComponent<Rigidbody2D>().gravityScale = 1;
                    backtrigger = true;
                }
                
            }
            else
            {
                if (fix)
                {
                    GetComponent<Rigidbody2D>().gravityScale = 0;
                    if (backtrigger)
                    {
                        Returntopos();
                    }
                }
                
            }

            weapon1cool++;
            lazer1cool++;
            lazercool++;
            barriercool++;
            bombcool++;

            if (canattack)
            {
                if (weapon1cool >= weapon1cooltime)
                {
                    weapon1cool = 0;
                    if (trapal_point.transform.childCount < trapal_point.GetComponent<trapal_weapon_point>().count)
                    {
                        GameObject curweapon1 = Instantiate(trapal_weapon1, trapal_point.transform);
                        curweapon1.GetComponent<enemyattack>().player = player;
                        curweapon1.GetComponent<enemyattack>().enemy = gameObject;
                        curweapon1.GetComponent<enemyattack>().canattack = false;
                    }

                }

                if (lazer1cool >= lazer1cooltime)
                {
                    lazer1cool = 0;
                    GameObject curlazer1 = Instantiate(lazer1, lazer1point, Quaternion.identity);
                    curlazer1.GetComponent<enemyattack>().player = player;
                    curlazer1.GetComponent<enemyattack>().enemy = gameObject;
                    curlazer1.GetComponent<trapal_lazer1>().player = player.transform;
                    Vector2 direction = player.transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    curlazer1.transform.rotation = Quaternion.Euler(0, 0, angle);
                }


                if (lazercool >= lazercooltime)
                {
                    lazercool = 0;
                    lazer.GetComponent<trapal_lazer>().LazerStart();

                }

                if (pointattackcool >= pointattackcooltime)
                {
                    pointattackcool = 0;
                    Instantiate(pointattack, new Vector3(Random.Range(-15, 16), 0, 0), Quaternion.identity);
                }

                if (barriercool >= barriercooltime)
                {
                    barriercool = 0;
                    barrier.GetComponent<trapal_barreirtext>().StartBarrier();
                }

                if (bombcool >= bombcooltime)
                {
                    bombcool = 0;
                    StartCoroutine(Bomb());
                }

            }
        }

        //else if (phase2)
        //{
        //    phase2lazercool++;
        //    phase2attack1cool++;
        //    phase2attack2cool++;

        //    if (canattack && !whileattack && phase2lazercool >= phase2lazercooltime)
        //    {
        //        phase2lazercool = 0;

        //        if (Mathf.Abs(player.transform.position.x - transform.position.x) >= 7f)
        //        {
        //            canattack = false;
        //            GetComponent<Animator>().SetTrigger("lazer");
        //        }
        //    }

        //    if (canattack && !whileattack && phase2attack1cool >= phase2attack1cooltime)
        //    {
        //        phase2attack1cool = 0;
        //        whileattack = true;

        //        GetComponent<Animator>().SetTrigger("attack1_ready");
        //    }

        //    if (canattack && !whileattack && phase2attack2cool >= phase2attack2cooltime)
        //    {
        //        phase2attack2cool = 0;
        //        whileattack = true;

        //        GetComponent<Animator>().SetTrigger("attack2");
        //    }
        //}
    }

    IEnumerator Bomb()
    {
        int count = Random.Range(3, 6);
        int i = 0;
        while(i < count)
        {
            i++;
            yield return new WaitForSeconds(1f);
            StartCoroutine(Bombreal());

        }
    }

    IEnumerator Bombreal()
    {
        Vector3 wheretopos = new Vector3(Random.Range(-17f, 17f), Random.Range(1f, 7f), 0);
        GameObject currentcore = Instantiate(denycore, wheretopos, Quaternion.identity);
        currentcore.transform.localScale = new Vector3(0, 0, 1);
        currentcore.transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.4f).SetEase(Ease.OutQuart);
        float angledefault = Random.Range(0f, 91f);
        Quaternion angle1 = Quaternion.Euler(0, 0, angledefault);
        Quaternion angle2 = Quaternion.Euler(0, 0, angledefault + 90);
        GameObject currentwarning1 = Instantiate(warning, wheretopos, angle1);
        GameObject currentwarning2 = Instantiate(warning, wheretopos, angle2);
        float addangle = Random.Range(300f, 390f);
        currentwarning1.transform.DOLocalRotate(new Vector3(0, 0, angledefault + addangle), 2f, RotateMode.FastBeyond360).SetEase(Ease.OutExpo);
        currentwarning2.transform.DOLocalRotate(new Vector3(0, 0, angledefault + addangle + 90), 2f, RotateMode.FastBeyond360).SetEase(Ease.OutExpo);
        currentwarning1.GetComponent<lazer2_warning>().time = 2.5f;
        currentwarning2.GetComponent<lazer2_warning>().time = 2.5f;
        currentwarning1.GetComponent<lazer2_warning>().scaley = 3.3f;
        currentwarning2.GetComponent<lazer2_warning>().scaley = 3.3f;
        yield return new WaitForSeconds(0.4f);
        currentcore.transform.DOScale(new Vector3(0, 0, 1), 1.6f).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(1.5f);
        GameObject bombshoot1 = Instantiate(bombshoot, wheretopos, Quaternion.Euler(0, 0, angledefault + addangle));
        GameObject bombshoot2 = Instantiate(bombshoot, wheretopos, Quaternion.Euler(0, 0, angledefault + addangle + 90));
        cammanager.GetComponent<CameraManager>().CamVibration1();
        bombshoot1.GetComponent<enemyattack>().heavyattack = true;
        bombshoot1.GetComponent<enemyattack>().lightattack = false;
        bombshoot1.GetComponent<enemyattack>().player = player;
        bombshoot2.GetComponent<enemyattack>().heavyattack = true;
        bombshoot2.GetComponent<enemyattack>().lightattack = false;
        bombshoot2.GetComponent<enemyattack>().player = player;
        boss_hpbar.StackInstance instance = BossstackHander.activeStacks.Find(s => s.stackData.effectName == "부정");
        if (instance != null)
        {
            if (instance.currentStack >= 12)
            {
                GameObject currentbullet = Instantiate(bomb_bullet, currentcore.transform.position, Quaternion.identity);
                currentbullet.GetComponent<trapal_bomb_bullet>().cameramanager = cammanager;
            }
        }
        
    }

    public void Returntopos()
    {
        backtrigger = false;
        transform.DOMove(targetPosition, duration).SetEase(Ease.OutCubic);
        
    }

    
    public void SwordSpawn()
    {
        GameObject cursword = Instantiate(sword, new Vector3(transform.position.x, transform.position.y + 30, 0), Quaternion.Euler(90, 0, 0));
        cursword.GetComponent<trapal_sword>().player = player;
        cursword.GetComponent<trapal_sword>().cammanager = cammanager;
        cursword.transform.DOLocalMoveY(8f, 3f).SetEase(Ease.Linear);
    }

    public void Lazer2spwan()
    {
        currentInterval = startInterval;

        DOTween.To(() => currentInterval, x => currentInterval = x, endInterval, spawnTime)
               .SetEase(Ease.Linear);

        StartCoroutine(SpawnLoop());

        
    }

    IEnumerator SpawnLoop()
    {
        float elapsed = 0f;

        while (elapsed < spawnTime)
        {
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            Vector2 spawnDirection = -directionToPlayer;
            Vector2 spawnPosition = (Vector2)transform.position + spawnDirection * 15;
            GameObject curlazer2 = Instantiate(lazer2, spawnPosition, Quaternion.identity);
            curlazer2.transform.position = new Vector3(curlazer2.transform.position.x, curlazer2.transform.position.y, -6.5f);
            curlazer2.GetComponent<lazer2lookat>().player = player;
            curlazer2.GetComponent<lazer2lookat>().look = true;
            curlazer2.GetComponent<lazer2lookat>().cammanager = cammanager;

            yield return new WaitForSeconds(currentInterval);
            elapsed += currentInterval;
        }
    }

    public void Phase2Lazer()
    {
        if (transform.position.x - player.transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        GameObject currentlazer = Instantiate(phase2lazer, lazerpos.transform.position, Quaternion.identity);
        currentlazer.transform.rotation = Quaternion.Euler(0, -60, 0);
        currentlazer.GetComponent<lazer2lookat>().cammanager = cammanager;
        currentlazer.GetComponent<lazer2lookat>().player = player;
        phase2currentlazer = currentlazer;

        StartCoroutine(Phase2LazerDump());
    }

    IEnumerator Phase2LazerDump()
    {
        boss_hpbar.StackInstance DenyInstance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "부정");
        if (DenyInstance != null)
        {
            int runs = DenyInstance.currentStack / 3;
            if (DenyInstance.currentStack > 3)
            {
                GetComponent<boss_hpbar>().RemoveStack(DenyInstance.stackData, 3);
            }
            for (int i = 0; i < runs; i++)
            {
                if (transform.localScale.x == -1)
                {
                    yield return new WaitForSeconds(0.25f);
                    GameObject curlazer2 = Instantiate(phase2lazer, new Vector3(transform.position.x - UnityEngine.Random.Range(3f, 7f), transform.position.y + UnityEngine.Random.Range(-6f, 6f), -6.5f), Quaternion.identity);
                    lazer2s.Add(curlazer2);
                    lazer2lookat curlazer2_Trapal_Lazer2 = curlazer2.GetComponent<lazer2lookat>();
                    curlazer2_Trapal_Lazer2.player = player;
                    curlazer2_Trapal_Lazer2.cammanager = cammanager;
                    curlazer2_Trapal_Lazer2.look = true;
                    curlazer2_Trapal_Lazer2.canwarning = true;
                    StartCoroutine(Shoot_co(curlazer2));
                }
                else
                {
                    yield return new WaitForSeconds(0.25f);
                    GameObject curlazer2 = Instantiate(phase2lazer, new Vector3(transform.position.x + UnityEngine.Random.Range(3f, 7f), transform.position.y + UnityEngine.Random.Range(-6f, 6f), -6.5f), Quaternion.identity);
                    lazer2s.Add(curlazer2);
                    lazer2lookat curlazer2_Trapal_Lazer2 = curlazer2.GetComponent<lazer2lookat>();
                    curlazer2_Trapal_Lazer2.player = player;
                    curlazer2_Trapal_Lazer2.cammanager = cammanager;
                    curlazer2_Trapal_Lazer2.look = true;
                    curlazer2_Trapal_Lazer2.canwarning = true;
                    StartCoroutine(Shoot_co(curlazer2));
                }

            }
            yield return new WaitForSeconds(1.5f);
            phase2currentlazer.GetComponent<lazer2lookat>().Shoot2();
        }
    }

    IEnumerator Shoot_co(GameObject lazer)
    {
        yield return new WaitForSeconds(1f);
        lazer.GetComponent<lazer2lookat>().Shoot();
    }

    public void Canattack()
    {
        canattack = true;
    }

    public void Attack1_ready()
    {
        StartCoroutine(Attack1_ready_co());
    }

    IEnumerator Attack1_ready_co()
    {
        yield return new WaitForSeconds(1f);
        DOTween.Kill("dash");
        if (attack1count >= 5)
        {
            animator.ResetTrigger("attack1_1");
            animator.ResetTrigger("attack1_2");
            attack1count = 0;
            transform.position = new Vector3(transform.position.x, player.transform.position.y + 4f, 0);
            animator.SetTrigger("idle2");
            whileattack = false;
            yield break;
        }
        int i = Random.Range(1, 3);
        if (i == 1)
        {
            animator.ResetTrigger("attack1_1");
            animator.ResetTrigger("attack1_2");
            animator.SetTrigger("attack1_1");
            int x = Random.Range(1, 3);
            if (x == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                dir = -1;
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                dir = 1;
            }
        }
        else if (i == 2)
        {
            animator.ResetTrigger("attack1_1");
            animator.ResetTrigger("attack1_2");
            animator.SetTrigger("attack1_2");
            int x = Random.Range(1, 3);
            if (x == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                dir = -1;
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                dir = 1;
            }
        }
        attack1count++;
    }

    public void Attack1_1()
    {
        transform.position = new Vector3(player.transform.position.x + (-5f * dir), player.transform.position.y + 1.5f, 0);
    }

    public void Attack1_1_dash()
    {
        transform.DOMoveX(transform.position.x + (7.5f * dir), 0.5f).SetEase(Ease.OutQuart).SetId("dash");
    }

    public void Attack1_2()
    {
        transform.position = new Vector3(player.transform.position.x + (-7.5f * dir), player.transform.position.y + 1.5f, 0);
    }

    public void Attack1_2_dash()
    {
        transform.DOMoveX(transform.position.x + (12 * dir), 0.5f).SetEase(Ease.OutQuart).SetId("dash");
    }

    public void Slash()
    {
        StartCoroutine(Slash_co());
    }

    IEnumerator Slash_co()
    {
        float angle = Random.Range(150, 200);
        Vector3 playerpos = player.transform.position;
        Instantiate(trapal_slash_warning, player.transform.position, Quaternion.Euler(0, 0, angle));
        yield return new WaitForSeconds(1f);
        GameObject currentslash = Instantiate(trapal_slash, playerpos, Quaternion.Euler(0, 0, angle));
        cammanager.GetComponent<CameraManager>().CamVibration0_5();
        yield return new WaitForSeconds(1.5f);
        Destroy(currentslash);
    }

    public void SpawnAttack2()
    {
        currenatcore = Instantiate(denycore, lazerpos.transform.position, Quaternion.identity);
        currenatcore.transform.localScale = new Vector3(0, 0, 1);
        currenatcore.transform.DOScale(new Vector3(0.38f, 0.38f, 1), 1f).SetEase(Ease.OutQuart);
    }

    public void Attack2_warning()
    {
        StartCoroutine(Attack2_warning_co());
    }

    IEnumerator Attack2_warning_co()
    {
        attack2angle.Clear();
        attack2pos.Clear();

        for (int i = 0; i < 20; i++)
        {
            int angle = Random.Range(0, 360);
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-50f, 50f), transform.position.y + Random.Range(-15f, 15f), 0);
            Instantiate(trapal_slash_warning, pos, Quaternion.Euler(0, 0, angle));
            attack2angle.Add(angle);
            attack2pos.Add(pos);
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void Attack2()
    {
        Destroy(currenatcore);
        int i = 0;
        foreach (Vector3 pos in attack2pos)
        {
            Instantiate(trapal_attack2_slash, pos, Quaternion.Euler(0, 0, attack2angle[i]));
            i++;
        }
    }

    public void EndAttack()
    {
        whileattack = false;
    }

    public void SpaceSlash()
    {
        StartCoroutine(SpaceSlash_co());
    }

    IEnumerator SpaceSlash_co()
    {
        Vector2 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Instantiate(trapal_slash_warning, transform.position, Quaternion.Euler(0, 0, angle));
        yield return new WaitForSeconds(0.5f);
        GameObject currentslash1 = Instantiate(spaceslash, transform.position, Quaternion.Euler(0, 0, angle));
        yield return new WaitForSeconds(0.7f);
        Vector3 playerpos = player.transform.position;
        Vector3 pos = new Vector3(Random.Range(-30f, 30f), Random.Range(-25f, 25f), 0);
        dir = player.transform.position - pos;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Instantiate(trapal_slash_warning, playerpos, Quaternion.Euler(0, 0, angle));
        yield return new WaitForSeconds(0.5f);
        GameObject currentslash2 = Instantiate(spaceslash, playerpos, Quaternion.Euler(0, 0, angle));
        currentslash2.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
        yield return new WaitForSeconds(0.7f);
        playerpos = player.transform.position;
        pos = new Vector3(Random.Range(-30f, 30f), Random.Range(-25f, 25f), 0);
        dir = player.transform.position - pos;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Instantiate(trapal_slash_warning, playerpos, Quaternion.Euler(0, 0, angle));
        yield return new WaitForSeconds(0.5f);
        GameObject currentslash3 = Instantiate(spaceslash, playerpos, Quaternion.Euler(0, 0, angle));
        currentslash3.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
        yield return new WaitForSeconds(3f);
        Destroy(currentslash1);
        Destroy(currentslash2);
        Destroy(currentslash3);

    }

    public void Gravity()
    {
        fix = false;
        GetComponent<Rigidbody2D>().gravityScale = 5f;
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

    public void Phase2AttackEnd()
    {
        int i = Random.Range(0, 2);
        if (i == 1)
        {
            animator.SetTrigger("phase2_attack1");
        }
        else if (i == 2)
        {
            animator.SetTrigger("phase2_attack2");
        }
    }

    public void Phase2attack1_Dash()
    {
        float power = 50 + Vector3.Distance(transform.position, player.transform.position) * 5;

        GetComponent<Rigidbody2D>().AddForce(Vector2.left * transform.localScale.x * power, ForceMode2D.Impulse);

        Vector3 effectpos = new Vector3(transform.position.x + (-transform.localScale.x * Vector3.Distance(transform.position, player.transform.position) / 2), transform.position.y - 1, 0);

        StartCoroutine(Dashslasheffect());
    }

    IEnumerator Dashslasheffect()
    {
        particle1.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        
        particle1.SetActive(false);
    }

    public void Phase2attack1_lazer0()
    {
        phase2currentlazer1 = Instantiate(lazer2, lazerpos.transform.position, Quaternion.Euler(0, -68, 0));
        phase2currentlazer1.GetComponent<lazer2lookat>().cammanager = battalemanager.Instance.cameramanager;
        phase2currentlazer1.GetComponent<lazer2lookat>().look = false;
    }

    public void Phase2attack1_lazer0_shoot()
    {
        phase2currentlazer1.GetComponent<lazer2lookat>().anglea = 0;
        phase2currentlazer1.GetComponent<lazer2lookat>().Shoot();
        if (transform.localScale.x == 1)
        {
            Vector3 pos = new Vector3(lazerpos.transform.position.x + 10, lazerpos.transform.position.y, 0);
            currentshaft = Instantiate(shaft, pos, Quaternion.identity);
            currentshaft.transform.DOMoveX(lazerpos.transform.position.x, 1.5f).SetEase(Ease.OutQuart).SetId("shaftmove");
        }
        else if (transform.localScale.x == -1)
        {
            Vector3 pos = new Vector3(lazerpos.transform.position.x - 10, lazerpos.transform.position.y, 0);
            currentshaft = Instantiate(shaft, pos, Quaternion.identity);
            currentshaft.transform.localScale = new Vector3(-1, 1, 1);
            currentshaft.transform.DOMoveX(lazerpos.transform.position.x, 1.5f).SetEase(Ease.OutQuart).SetId("shaftmove");
        }

        StartCoroutine(Positioning());
    }

    public void Phase2attack1_lazer0_2()
    {
        phase2currentlazer1 = Instantiate(lazer2, lazerpos.transform.position, Quaternion.Euler(0, -68, 0));
        phase2currentlazer1.GetComponent<lazer2lookat>().cammanager = battalemanager.Instance.cameramanager;
        phase2currentlazer1.GetComponent<lazer2lookat>().look = false;
    }

    public void Phase2attack1_lazer0_shoot2()
    {
        phase2currentlazer1.GetComponent<lazer2lookat>().anglea = 0;
        phase2currentlazer1.GetComponent<lazer2lookat>().Shoot();
        if (transform.localScale.x == 1)
        {
            Vector3 pos = new Vector3(lazerpos.transform.position.x + 10, lazerpos.transform.position.y, 0);
            currentshaft = Instantiate(shaft, pos, Quaternion.identity);
            currentshaft.transform.DOMoveX(lazerpos.transform.position.x, 1.5f).SetEase(Ease.OutQuart).SetId("shaftmove");
        }
        else if (transform.localScale.x == -1)
        {
            Vector3 pos = new Vector3(lazerpos.transform.position.x - 10, lazerpos.transform.position.y, 0);
            currentshaft = Instantiate(shaft, pos, Quaternion.identity);
            currentshaft.transform.localScale = new Vector3(-1, 1, 1);
            currentshaft.transform.DOMoveX(lazerpos.transform.position.x, 1.5f).SetEase(Ease.OutQuart).SetId("shaftmove");
        }

        StartCoroutine(Transforming());
    }

    IEnumerator Positioning()
    {
        yield return new WaitForSeconds(1.6f);
        currentshaft.GetComponent<trapal_shaft>().RamdomPosition();
    }

    IEnumerator Transforming()
    {
        yield return new WaitForSeconds(1f);
        currentshaft.GetComponent<trapal_shaft>().Transforming();
    }

    public void ShaftShoot()
    {
        currentshaft.GetComponent<trapal_shaft>().Shoot();
    }

    public void ShaftShootGo()
    {
        currentshaft.GetComponent<trapal_shaft>().ShootGO();
    }

    public void Phase2attack1_lazer1()
    {
        phase2currentlazer1 = Instantiate(lazer2, lazerpos.transform.position, lazer2.transform.rotation);
        phase2currentlazer1.GetComponent<lazer2lookat>().cammanager = battalemanager.Instance.cameramanager;
        phase2currentlazer1.GetComponent<lazer2lookat>().look = false;
    }

    public void Phase2attack1_lazer1_shoot()
    {
        phase2currentlazer1.GetComponent<lazer2lookat>().anglea = 90;
        phase2currentlazer1.GetComponent<lazer2lookat>().Shoot();
    }

    public void Flash()
    {
        Instantiate(flash, lazerpos.transform.position, Quaternion.identity);
    }

    public void AfterImage()
    {
        GetComponent<afterimagetest>().StartGenerate();
        GetComponent<SpriteRenderer>().material = glitchmat;
    }

    public void EndAfterImage()
    {
        GetComponent<afterimagetest>().EndGenerate();
        GetComponent<SpriteRenderer>().material = normalmat;

    }

    public void Consern1()
    {
        var ps = concern.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = 5f;
    }

    public void Consern2()
    {
        var ps = concern.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = 10f;
    }

    public void Consern3()
    {
        var ps = concern.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = 25f;
    }

    public void Consern4()
    {
        var ps = concern.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = 50f;
    }

    public void Consern0()
    {
        var ps = concern.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = 0f;
    }

    public void SpawnEtching()
    {
        GameObject curlazer = Instantiate(lazer2small, lazerpos.transform.position, Quaternion.Euler(0, -70, 0));
        curlazer.GetComponent<lazer2lookat>().look = false;
        curlazer.GetComponent<lazer2lookat>().isshoot = false;
        curlazer.GetComponent<lazer2lookat>().canwarning = false;
        curlazer.GetComponent<lazer2lookat>().canshoot = false;
        curlazer.GetComponent<lazer2lookat>().player = player;
        curlazer.GetComponent<lazer2lookat>().cammanager = cammanager;
        curlazer2small = curlazer;

        Vector3 newpos = lazerpos.transform.position;

        curtext1 = Instantiate(text1, lazerpos.transform.position, Quaternion.identity);
        curtext1.transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }

    public void Range()
    {
        curlazer2small.GetComponent<lazer2lookat>().Charge();
    }

    public void Shoot()
    {
        curlazer2small.GetComponent<lazer2lookat>().Shoot2();
        Destroy(curtext1);
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
            GetComponent<Rigidbody2D>().AddForce(120f * Vector2.left, ForceMode2D.Impulse);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(120f * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void MovebackTo()
    {
        BoxTrue();
        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(30f * Vector2.right, ForceMode2D.Impulse);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(30f * Vector2.left, ForceMode2D.Impulse);
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

    public void ActiveSlashcore()
    {
        slashcore.SetActive(true);
    }

    public void FalseSlashcore()
    {
        slashcore.SetActive(false);
    }
}
