using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_script : MonoBehaviour
{
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
    public bool lazer2time;
    public bool swordtime;
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

        lazer1cool++;
        lazercool++;
        barriercool++;

        if (canattack)
        {
            if (lazer1cool >= lazer1cooltime)
            {
                lazer1cool = 0;
                GameObject curlazer1 = Instantiate(lazer1, lazer1point, Quaternion.identity);
                curlazer1.transform.GetChild(0).GetComponent<enemydattack>().player = player;
                curlazer1.transform.GetChild(0).GetComponent<enemydattack>().enemy = gameObject;
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
