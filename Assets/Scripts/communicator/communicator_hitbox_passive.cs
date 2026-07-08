using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class communicator_hitbox_passive : MonoBehaviour
{
    public void Resolve(GameObject player, GameObject enemy)
    {
        player.GetComponent<playerstatus>().RemoveStack(battalemanager.Instance.stackdatas[22], 3);
        enemy.GetComponent<boss_hpbar>().ApplyStack(battalemanager.Instance.stackdatas[24], 1);
    }
}
