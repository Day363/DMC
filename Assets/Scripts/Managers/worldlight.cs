using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldlight : MonoBehaviour
{
    public void Awake()
    {
        battalemanager.Instance.world_globallight = gameObject;
    }
}
