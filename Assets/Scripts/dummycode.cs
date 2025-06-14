using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummycode : MonoBehaviour
{
    public GameObject gamemanager;

    private void Start()
    {
        gamemanager.GetComponent<battalemanager>().currentenemy = gameObject;
    }
}
