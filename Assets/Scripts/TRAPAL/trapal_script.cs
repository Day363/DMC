using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_script : MonoBehaviour
{
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

    public void Start()
    {
        gamemanager.GetComponent<battalemanager>().currentenemy = gameObject;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

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

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("collapse"))
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            backtrigger = true;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            if (backtrigger)
            {
                Returntopos();
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
                    curweapon1.GetComponent<enemyattack>().canattack = false;
                }
                
            }

            if (lazer1cool >= lazer1cooltime)
            {
                lazer1cool = 0;
                GameObject curlazer1 = Instantiate(lazer1, lazer1point, Quaternion.identity);
                curlazer1.GetComponent<enemyattack>().player = player;
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
        boss_hpbar.StackInstance instance = BossstackHander.activeStacks.Find(s => s.stackData.effectName == "ºÎÁ¤");
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

}
