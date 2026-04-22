using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_arms : MonoBehaviour
{
    public int randomspeed;
    public float randomsecond;
    public float randomstopsecond;
    public int direction;
    public HingeJoint2D hingeJointe;
    public JointMotor2D motor;


    public AudioSource audio;
    public float maxpitch;
    public float lowpitch;
    public float maxvolume;

    private void Start()
    {
        hingeJointe = GetComponent<HingeJoint2D>();
        motor = hingeJointe.motor;
        hingeJointe.motor = motor;

        StartCoroutine(Turn());
    }

    IEnumerator TurnStart()
    {
        motor.motorSpeed = 0;
        hingeJointe.motor = motor;
        randomstopsecond = Random.Range(0.5f, 10.5f);
        yield return new WaitForSeconds(randomstopsecond);
        StartCoroutine(Turn());
    }

    IEnumerator Turn()
    {
        DOTween.To(() => audio.volume, x => audio.volume = x, maxvolume, 0.5f).SetEase(Ease.OutQuad);
        audio.DOPitch(maxpitch, 1.5f);

        direction = Random.Range(1, 3);
        if (direction == 1)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        randomspeed = Random.Range(10, 41) * direction;
        randomsecond = Random.Range(1.5f, 5);
        motor.motorSpeed = randomspeed;
        hingeJointe.motor = motor;

        yield return new WaitForSeconds(randomsecond);

        audio.DOPitch(lowpitch, 1.5f);
        DOTween.To(() => audio.volume, x => audio.volume = x, 0, 0.5f).SetEase(Ease.OutQuad);
        
        StartCoroutine(TurnStart());
    }
}
