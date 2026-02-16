using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test1 : MonoBehaviour
{
    public GameObject player;
    public GameObject eye;
    public GameObject eyeposition;
    public GameObject slasheffect;

    public void LookPlayer()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Move()
    {
        DOTween.Kill("move");
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            float x = player.transform.position.x - Random.Range(5, 10);
            transform.DOMoveX(x, 0.3f).SetEase(Ease.OutExpo).SetId("move");
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            float x = player.transform.position.x + Random.Range(5, 10);
            transform.DOMoveX(x, 0.3f).SetEase(Ease.OutExpo).SetId("move");
        }

    }

    public void Move2()
    {
        DOTween.Kill("move");
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            float x = player.transform.position.x + 3f;
            transform.DOMoveX(x, 0.3f).SetEase(Ease.OutExpo).SetId("move");
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            float x = player.transform.position.x - 3f;
            transform.DOMoveX(x, 0.3f).SetEase(Ease.OutExpo).SetId("move");
        }
    }

    public void Move3()
    {
        DOTween.Kill("move");
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            float x = player.transform.position.x - 10f;
            transform.DOMoveX(x, 0.15f).SetEase(Ease.OutExpo).SetId("move");
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            float x = player.transform.position.x + 10f;
            transform.DOMoveX(x, 0.15f).SetEase(Ease.OutExpo).SetId("move");
        }
    }

    public void EyeSpawn()
    {
        GameObject cureye = Instantiate(eye, eyeposition.transform);
        cureye.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void Slasheffect2()
    {
        GameObject cureffect = Instantiate(slasheffect, player.transform.position, Quaternion.Euler(0, 0, Random.Range(135f, 45f)));
        cureffect.transform.position = player.transform.position;
        cureffect = Instantiate(slasheffect, player.transform.position, Quaternion.Euler(0, 0, Random.Range(135f, 45f)));
        cureffect.transform.position = player.transform.position;
    }
}
