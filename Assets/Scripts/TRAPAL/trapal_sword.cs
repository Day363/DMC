using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_sword : MonoBehaviour
{
    public GameObject player;
    public GameObject cammanager;
    public GameObject lazer2;
    public GameObject curlazer2;
    public float spawnDistance;
    public float moveSpeed;
    public float distanceBehind;

    public List<GameObject> lazer2list = new List<GameObject> { };

    public float dashpower;
    public int dir;

    public int count = 0;
    public float initialInterval = 5f;    
    public float minInterval = 1.5f;      
    public float acceleration = 0.05f;

    public void Start()
    {
        StartCoroutine(CallFunctionFasterOverTime());
    }

    IEnumerator CallFunctionFasterOverTime()
    {
        yield return new WaitForSeconds(3.5f);

        float interval = initialInterval;

        while (count < 11)
        {
            if (count < 9)
            {
                StartCoroutine(Shoot());

                yield return new WaitForSeconds(interval);


                interval = Mathf.Max(minInterval, interval - acceleration);
            }
            else
            {
                StartCoroutine(Shoot2());

                yield return new WaitForSeconds(interval);


                interval = Mathf.Max(minInterval, interval - acceleration);
            }
            
        }
    }

    IEnumerator Shoot2()
    {
        count++;

        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector3 targetPosition = new Vector3(player.transform.position.x, -0.4f, player.transform.position.z);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float opposangle = angle + 180f;
        float rad = opposangle * Mathf.Deg2Rad;
        Vector2 direction1 = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Vector2 spawnPosition = (Vector2)transform.position + direction1 * spawnDistance;
        curlazer2 = Instantiate(lazer2, spawnPosition, Quaternion.identity);
        lazer2list.Add(curlazer2);
        curlazer2.GetComponent<lazer2lookat>().isshoot = false;
        curlazer2.transform.position = new Vector3(curlazer2.transform.position.x, curlazer2.transform.position.y, -6.5f);
        curlazer2.GetComponent<lazer2lookat>().player = gameObject;
        curlazer2.GetComponent<lazer2lookat>().look = true;
        curlazer2.GetComponent<lazer2lookat>().cammanager = cammanager;


        float travelTime = Vector3.Distance(transform.position, targetPosition) / moveSpeed;

        transform.DORotate(new Vector3(0, 0, angle), 0.3f).SetEase(Ease.OutQuart);

        yield return new WaitForSeconds(0.4f);

        curlazer2.GetComponent<lazer2lookat>().ShootNotDes();
        transform.DOMove(targetPosition, travelTime).SetEase(Ease.OutQuart);

        if (count > 10)
        {
            yield return new WaitForSeconds(1.5f);

            foreach (GameObject lazer2 in lazer2list)
            {
                lazer2.GetComponent<lazer2lookat>().Charge();
            }

            yield return new WaitForSeconds(0.7f);

            GetComponent<Animator>().SetTrigger("big");
            transform.DORotate(new Vector3(0, 0, 90), 1.5f, RotateMode.FastBeyond360).SetEase(Ease.OutQuart).SetId("mnu");

            yield return new WaitForSeconds(1.5f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
            GetComponent<Animator>().SetTrigger("attack");

        }
    }

    IEnumerator Shoot()
    {
        count++;

        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector3 targetPosition = player.transform.position + (Vector3)(direction * distanceBehind);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float opposangle = angle + 180f;
        float rad = opposangle * Mathf.Deg2Rad;
        Vector2 direction1 = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Vector2 spawnPosition = (Vector2)transform.position + direction1 * spawnDistance;
        curlazer2 = Instantiate(lazer2, spawnPosition, Quaternion.identity);
        lazer2list.Add(curlazer2);
        curlazer2.GetComponent<lazer2lookat>().isshoot = false;
        curlazer2.transform.position = new Vector3(curlazer2.transform.position.x, curlazer2.transform.position.y, -6.5f);
        curlazer2.GetComponent<lazer2lookat>().player = gameObject;
        curlazer2.GetComponent<lazer2lookat>().look = true;
        curlazer2.GetComponent<lazer2lookat>().cammanager = cammanager;


        float travelTime = Vector3.Distance(transform.position, targetPosition) / moveSpeed;

        transform.DORotate(new Vector3(0, 0, angle), 0.3f).SetEase(Ease.OutQuart);

        yield return new WaitForSeconds(0.4f);

        curlazer2.GetComponent<lazer2lookat>().ShootNotDes();
        transform.DOMove(targetPosition, travelTime).SetEase(Ease.OutQuart);

        if (count > 10)
        {
            yield return new WaitForSeconds(1.5f);

            foreach (GameObject lazer2 in lazer2list)
            {
                lazer2.GetComponent<lazer2lookat>().Charge();
            }
        }
    }

    public void Dash()
    {
        if (transform.position.x - player.transform.position.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            dir = -1;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            dir = 1;
        }

        GetComponent<Rigidbody2D>().AddForce(Vector2.right * dashpower * dir, ForceMode2D.Impulse);
    }
    public void CamVib()
    {
        cammanager.GetComponent<CameraManager>().CamVibration1();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

}
