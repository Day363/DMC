using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_skillposition_rotate : MonoBehaviour
{
    public GameObject player;
    public int direction = 0;

    private void Update()
    {
        direction = player.GetComponent<PlayerMove>().dir;

        
    }
}
