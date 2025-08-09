using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerfollow : MonoBehaviour
{
    public Transform player;
    public float x;
    public float y;
    public float z;

    public void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x + x, player.position.y + y, z);
    }
}
