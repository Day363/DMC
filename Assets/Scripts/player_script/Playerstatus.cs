using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstatus : MonoBehaviour
{
    public int maxhp;
    public int currenthp;

    public void Start()
    {
        currenthp = maxhp;
    }
}
