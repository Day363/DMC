using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerlightscript : MonoBehaviour
{
    public void Awake()
    {
        battalemanager.Instance.player_light = gameObject;
    }
}
