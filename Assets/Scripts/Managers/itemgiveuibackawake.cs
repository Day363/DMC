using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemgiveuibackawake : MonoBehaviour
{
    public void Awake()
    {
        battalemanager.Instance.itemgiveui = gameObject;
    }
}
