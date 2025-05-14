using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_arms : MonoBehaviour
{
    public int randomspeed;
    public float randomsecond;
    public float randomstopsecond;
    public int direction;
    public HingeJoint2D hingeJointe;
    public JointMotor2D motor;

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

        StartCoroutine(TurnStart());
    }
}
