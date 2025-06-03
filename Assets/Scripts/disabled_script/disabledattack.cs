using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledattack : MonoBehaviour
{
    public GameObject player;
    public GameObject disabled;
    public int damage;
    public bool lightattack = false;
    public bool heavyattack = false;
    public float parryback = 35;
    public float selfback;
    public float playerback;
    public float ratiosum;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerattack")
        {
            gameObject.SetActive(false);
            disabled.GetComponent<boss_hpbar>().BalanceDamage(collision.gameObject.GetComponent<playerattackdamage>().damage);

            ratiosum = damage + collision.gameObject.GetComponent<playerattackdamage>().damage;
            selfback = (collision.gameObject.GetComponent<playerattackdamage>().damage / ratiosum) * parryback;
            playerback = (damage / ratiosum) * parryback;

            if (player.transform.position.x > disabled.transform.position.x)
            {
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * playerback, ForceMode2D.Impulse);
                disabled.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -selfback, ForceMode2D.Impulse);
            }
            else
            {
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -playerback, ForceMode2D.Impulse);
                disabled.GetComponent<Rigidbody2D>().AddForce(Vector2.right * selfback, ForceMode2D.Impulse);
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            if (heavyattack)
            {
                heavyattack = false;
                player.GetComponent<playerhit>().StrongHit(damage, transform);

            }

            if (lightattack)
            {
                lightattack = false;
                player.GetComponent<playerhit>().Hit(damage);
            }
        }
    }

}
