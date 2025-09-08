using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battalemanager : MonoBehaviour
{
    public GameObject player;
    public GameObject attackcore;
    public GameObject currentenemy;

    public void Battlestart()
    {
        attackcore.GetComponent<attackcore>().BattleStart();
    }
}
