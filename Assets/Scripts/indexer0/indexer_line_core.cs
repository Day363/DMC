using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer_line_core : MonoBehaviour
{
    public GameObject player;
    public GameObject indexer;
    public GameObject warning;
    public GameObject shoot;
    public float lookspeed;

    public bool look;

    public void FixedUpdate()
    {
        if (look)
        {
            ///transform.LookAt(player.transform);
            Vector3 dir = player.transform.position - transform.position;
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, lookspeed);
        }
        
    }

    public void LookStart()
    {
        look = true;

        foreach (Transform child in transform)
        {
            child.GetComponent<indexer_line>().LookStart();
        }
    }

    public void Shoot()
    {
        StartCoroutine(Shoot_co());

    }

    IEnumerator Shoot_co()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        look = false;

        Instantiate(warning, gameObject.transform.position, Quaternion.Euler(0f, 0f, angle));

        yield return new WaitForSeconds(1.5f);

        GameObject currentshoot = Instantiate(shoot, gameObject.transform.position, Quaternion.Euler(0f, 0f, angle));
        currentshoot.GetComponent<enemyattack>().player = player;
        currentshoot.GetComponent<enemyattack>().enemy = indexer;

        look = true;
    }
}
