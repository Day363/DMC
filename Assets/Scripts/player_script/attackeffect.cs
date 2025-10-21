using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackeffect : MonoBehaviour
{
    public GameObject[] attackeffectall;

    public GameObject[] attackeffect_random;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemyattack")
        {
            foreach (GameObject effect in attackeffect_random)
            {
                float angle = Random.Range(0, 361);
                Vector2 midpoint = (transform.position + collision.gameObject.transform.position) / 2f;
                Instantiate(effect, midpoint, Quaternion.Euler(0, 0, angle));
            }
        }

        if (collision.gameObject.tag == "client")
        {
            int attackeffectnum = Random.Range(0, attackeffect_random.Length);
            Vector3 eposition = collision.transform.position;
            eposition.z = 0;

            Vector3 direction = eposition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Instantiate(attackeffect_random[attackeffectnum], collision.gameObject.transform.position, Quaternion.Euler(0, 0, angle));
            foreach (GameObject effect in attackeffectall)
            {
                Instantiate(effect, collision.gameObject.transform.position, Quaternion.Euler(0, 0, angle));
            }
        }
    }
}
