using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectdisappear : MonoBehaviour
{
    public GameObject self;

    public void Awake()
    {
        self = gameObject;
    }

    public void EffectDisappear()
    {
        Destroy(self);
    }
}
