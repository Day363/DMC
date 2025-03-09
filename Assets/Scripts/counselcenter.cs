using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counselcenter : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    public GameObject self;

    private void Update()
    {

        self.transform.position = new Vector3((player.transform.position.x + target.transform.position.x) / 2, player.transform.position.y, 0);
    }
}
