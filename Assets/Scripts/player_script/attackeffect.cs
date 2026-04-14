using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackeffect : MonoBehaviour
{
    public GameObject[] attackeffectall;
    public bool anglefix;
    public float fixangle;
    public bool randomangle;

    public GameObject[] attackeffect_random;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemyattack")
        {
            foreach (GameObject effect in attackeffect_random)
            {
                float angle = Random.Range(0, 361);

                if (anglefix)
                {
                    angle = fixangle;
                }

                Vector2 midpoint = (transform.position + collision.gameObject.transform.position) / 2f;
                Instantiate(effect, midpoint, Quaternion.Euler(0, 0, angle));
            }
        }

        if (collision.gameObject.tag == "client")
        {
            int attackeffectnum = Random.Range(0, attackeffectall.Length);
            Vector3 eposition = collision.transform.position;
            eposition.z = 0;

            Vector3 direction = eposition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (anglefix)
            {
                angle = fixangle;
            }

            if (attackeffectall.Length > 0)
            {
                Instantiate(attackeffectall[attackeffectnum], collision.gameObject.transform.position, Quaternion.Euler(0, 0, angle));
            }
            
            foreach (GameObject effect in attackeffectall)
            {
                if (randomangle)
                {
                    angle = Random.Range(0, 361);
                }
                Instantiate(effect, collision.gameObject.transform.position, Quaternion.Euler(0, 0, angle));
            }
        }

        if (collision.gameObject.tag == "enemybullet")
        {
            int attackeffectnum = Random.Range(0, attackeffect_random.Length);
            Vector3 eposition = collision.transform.position;
            eposition.z = 0;

            Vector3 direction = eposition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (anglefix)
            {
                angle = fixangle;
            }

            Instantiate(attackeffect_random[attackeffectnum], collision.gameObject.transform.position, Quaternion.Euler(0, 0, angle));
            foreach (GameObject effect in attackeffectall)
            {
                Instantiate(effect, collision.gameObject.transform.position, Quaternion.Euler(0, 0, angle));
            }
        }
    }
}
