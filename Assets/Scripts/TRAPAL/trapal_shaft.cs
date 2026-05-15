using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.Unicode;
using static UnityEditor.Experimental.GraphView.GraphView;

public class trapal_shaft : MonoBehaviour
{
    public GameObject player;
    public GameObject lazer2;
    public GameObject lazer2_small;
    public GameObject curlazer2;
    public List<GameObject> lazer2list = new List<GameObject>();
    public List<GameObject> lazer2smalllist = new List<GameObject>();
    public GameObject warning;
    public GameObject dummy;
    public GameObject currentdummy;
    public bool sight;

    public void Start()
    {
        player = battalemanager.Instance.player;
    }

    public void RamdomPosition()
    {
        DOTween.Kill("shaftmove");

        float posx = Random.Range(player.transform.position.x - 10f, player.transform.position.x + 10f);
        float posy = Random.Range(player.transform.position.y + 5f, player.transform.position.y + 8f);
        Vector3 pos = new Vector3(posx, posy, 0);

        transform.DOMove(pos, 3.5f).SetEase(Ease.OutQuad);

        Vector2 dir = player.transform.position - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Vector3 angle_ = new Vector3(0, 0, angle);

        transform.DORotate(angle_, 3.5f, RotateMode.Fast).SetEase(Ease.OutQuad);
    }

    public void FixedUpdate()
    {
        if (sight)
        {
            Vector2 dir = player.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion targetRot = Quaternion.Euler(0, 0, angle + 180f);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                180f * Time.deltaTime // 회전 속도
            );
        }
    }

    public void Transforming()
    {
        GetComponent<Animator>().SetTrigger("transform");
        StartCoroutine(PiercerShoot());
    }

    IEnumerator PiercerShoot()
    {
        yield return new WaitForSeconds(2.5f);
        sight = true;
        yield return new WaitForSeconds(1f);
        LittleShoot();
        yield return new WaitForSeconds(1.5f);
        LittleShoot();
        yield return new WaitForSeconds(1.5f);
        LittleShoot();
    }

    public void LittleShoot()
    {
        sight = false;

        StartCoroutine(LittleShoot_co());
        currentdummy = Instantiate(dummy, player.transform.position, Quaternion.identity);
    }

    IEnumerator LittleShoot_co()
    {
        Vector2 backPos = (Vector2)transform.position - (Vector2)transform.right * -5;

        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.05f);

            Vector3 pos = new Vector3(backPos.x + Random.Range(-2.5f, 2.5f), backPos.y + Random.Range(-2.5f, 2.5f), -6.5f);

            GameObject curlazer2 = Instantiate(lazer2_small, pos, Quaternion.identity);
            lazer2smalllist.Add(curlazer2);
            lazer2lookat curlazer2_Trapal_Lazer2 = curlazer2.GetComponent<lazer2lookat>();
            curlazer2_Trapal_Lazer2.player = currentdummy;
            curlazer2_Trapal_Lazer2.cammanager = battalemanager.Instance.cameramanager;
            curlazer2_Trapal_Lazer2.look = true;
            curlazer2_Trapal_Lazer2.canwarning = true;
        }

        foreach (GameObject smalllazer in lazer2smalllist)
        {
            yield return new WaitForSeconds(0.05f);
            smalllazer.GetComponent<lazer2lookat>().Shoot();    
        }
        lazer2smalllist.Clear();

        sight = true;
    }

    public void Shoot()
    {
        StartCoroutine(Shoot_co());
    }

    IEnumerator Shoot_co()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector3 targetPosition = player.transform.position + (Vector3)(direction * 20);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float opposangle = angle;
        float rad = opposangle * Mathf.Deg2Rad;
        Vector2 direction1 = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Vector2 spawnPosition = (Vector2)transform.position + direction1 * 10;

        GameObject curwarning = Instantiate(warning, transform.position, Quaternion.identity);
        curwarning.transform.rotation = Quaternion.Euler(0, 0, angle);

        curlazer2 = Instantiate(lazer2, spawnPosition, Quaternion.identity);
        lazer2list.Add(curlazer2);
        curlazer2.GetComponent<lazer2lookat>().isshoot = false;
        curlazer2.transform.position = new Vector3(curlazer2.transform.position.x, curlazer2.transform.position.y, -6.5f);
        curlazer2.GetComponent<lazer2lookat>().player = gameObject;
        curlazer2.GetComponent<lazer2lookat>().look = true;
        curlazer2.GetComponent<lazer2lookat>().cammanager = battalemanager.Instance.cameramanager;

        DOTween.Kill(transform);

        transform.DORotate(new Vector3(0, 0, angle), 0.5f).SetEase(Ease.OutQuart);

        yield return new WaitForSeconds(0.6f);

        curlazer2.GetComponent<lazer2lookat>().ShootNotDes();
        transform.DOMove(targetPosition, 0.35f).SetEase(Ease.OutQuad);
    }

    public void ShootGO()
    {
        StartCoroutine(ShootGO_co());
    }

    IEnumerator ShootGO_co()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector3 targetPosition = player.transform.position + (Vector3)(direction * 50);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float opposangle = angle;
        float rad = opposangle * Mathf.Deg2Rad;
        Vector2 direction1 = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Vector2 spawnPosition = (Vector2)transform.position + direction1 * 10;
        curlazer2 = Instantiate(lazer2, spawnPosition, Quaternion.identity);
        lazer2list.Add(curlazer2);
        curlazer2.GetComponent<lazer2lookat>().isshoot = false;
        curlazer2.transform.position = new Vector3(curlazer2.transform.position.x, curlazer2.transform.position.y, -6.5f);
        curlazer2.GetComponent<lazer2lookat>().player = gameObject;
        curlazer2.GetComponent<lazer2lookat>().look = true;
        curlazer2.GetComponent<lazer2lookat>().cammanager = battalemanager.Instance.cameramanager;


        float travelTime = Vector3.Distance(transform.position, targetPosition) / 20;

        DOTween.Kill(transform);

        transform.DORotate(new Vector3(0, 0, angle), 1f).SetEase(Ease.OutQuart);

        yield return new WaitForSeconds(1.1f);

        curlazer2.GetComponent<lazer2lookat>().ShootNotDes();
        transform.DOMove(targetPosition, 0.7f).SetEase(Ease.OutQuart);
    }
}
