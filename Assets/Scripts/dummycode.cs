using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummycode : MonoBehaviour
{
    public GameObject gamemanager;

    private void Start()
    {
        battalemanager.Instance.currentenemys.Add(gameObject);
        InvokeRepeating("Cycle", 0, 3);
 
    }

    
}
