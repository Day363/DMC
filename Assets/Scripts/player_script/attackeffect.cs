using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackeffect : MonoBehaviour
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject[] attackeffect_;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemyattack")
        {
            float angle1 = Random.Range(0, 361);
            float angle2 = Random.Range(0, 361);

            Vector2 midpoint = (transform.position + collision.gameObject.transform.position) / 2f;

            Instantiate(effect1, midpoint, Quaternion.Euler(0, 0, angle1));
            Instantiate(effect2, midpoint, Quaternion.Euler(0, 0, angle2));
        }

        if (collision.gameObject.tag == "client")
        {
            int attackeffectnum = Random.Range(0, attackeffect_.Length);
            Vector3 eposition = collision.transform.position;
            eposition.z = 0;

            Vector3 direction = eposition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Instantiate(attackeffect_[attackeffectnum], collision.gameObject.transform.position, Quaternion.Euler(0, 0, angle));
        }
    }
}
