using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealth : MonoBehaviour
{
    public int max_health;
    public int health;
    public float speed;

    public void Awake()
    {
        health = max_health;

    }

}
