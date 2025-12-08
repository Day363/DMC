using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeoutawake : MonoBehaviour
{
    public void Awake()
    {
        battalemanager.Instance.fadeout = gameObject;
    }
}
