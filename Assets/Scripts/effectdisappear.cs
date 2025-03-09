using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectdisappear : MonoBehaviour
{
    public GameObject self;

    public void EffectDisappear()
    {
        self.SetActive(false);
    }
}
