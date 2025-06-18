using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_gunprefap : MonoBehaviour
{
    public GameObject effect;
    public GameObject effectpos;

    public GameObject bullet;
    public GameObject bulletpos;

    public int damage;

    public void Effect()
    {
        Instantiate(effect, effectpos.transform.position, transform.rotation);
    }

    public void Shoot()
    {
        GameObject currentbullet = Instantiate(bullet, bulletpos.transform.position, transform.rotation);
        currentbullet.GetComponent<playerbullet>().damage = damage;
    }
}
