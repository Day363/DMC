using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_lazer2_turn : MonoBehaviour
{
    public bool charge;

    public GameObject player;

    public Vector2 speedrange;
    public Vector2 zrange;

    public float turnspeed;
    public float currentangle;

    public float targetz;
    public float curz;
    public float shoottime;

    public void Start()
    {
        turnspeed = Random.Range(speedrange.x, speedrange.y);
        targetz = Random.Range(zrange.x, zrange.y);

        DOTween.To(() => curz, x => curz = x, targetz, 3f).SetEase(Ease.OutQuart);
    }

    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(0, 0, curz);
        currentangle = currentangle + turnspeed;
        transform.localEulerAngles = new Vector3(0, 0, currentangle);
        if (charge)
        {
            turnspeed = turnspeed + 0.2f;
        }
        
    }

    public void Shoot()
    {
        targetz = Random.Range(-10f, -30f);
        DOTween.To(() => curz, x => curz = x, targetz, 1f).SetEase(Ease.OutQuart);
    }
}
