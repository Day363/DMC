using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword1attackeffect : MonoBehaviour
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject[] attackeffect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemyattack")
        {
            float angle1 = Random.Range(0, 361);
            float angle2 = Random.Range(0, 361);
            Instantiate(effect1, transform.position, Quaternion.Euler(0, 0, angle1));
            Instantiate(effect2, transform.position, Quaternion.Euler(0, 0, angle2));
        }
        
        if (collision.gameObject.tag == "enemyhitbox")
        {
            int attackeffectnum = Random.Range(0, attackeffect.Length);
            Vector3 eposition = collision.transform.position;
            eposition.z = 0;

            Vector3 direction = eposition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Instantiate(attackeffect[attackeffectnum], collision.transform.position, Quaternion.Euler(0, 0, angle));
        }
    }
}
