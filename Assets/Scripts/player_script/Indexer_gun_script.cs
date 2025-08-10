using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indexer_gun_script : MonoBehaviour
{
    public GameObject self;
    public void Passive2_call()
    {
        GetComponent<player_gunprefap>().player.GetComponent<Passivefunction>().Indexer_gun_call(gameObject);
    }
}
