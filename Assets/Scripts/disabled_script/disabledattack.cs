using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledattack : MonoBehaviour
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject player;
    public GameObject self;
    public int damage;
    public bool lightattack = false;
    public bool heavyattack = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!heavyattack && !lightattack)
        {
            if(collision.gameObject.tag == "playerattack")
            {
                float angle1 = Random.Range(0, 361);
                float angle2 = Random.Range(0, 361);
                Vector3 collisionsite = new Vector3(collision.transform.position.x, collision.transform.position.y, 0);
                Debug.Log("Ãæµ¹");
                self.SetActive(false);
                Instantiate(effect1, collisionsite, Quaternion.Euler(0, 0, angle1));
                Instantiate(effect2, collisionsite, Quaternion.Euler(0, 0, angle2));

            }
        }

        if (heavyattack)
        {
            heavyattack = false;
            player.GetComponent<playerhit>().StrongHit(damage, collision.transform);
            
        }

        if (lightattack)
        {
            lightattack = false;
            player.GetComponent<playerhit>().Hit(damage);
        }
    }
}
