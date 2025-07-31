using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_gunprefap : MonoBehaviour
{
    public Weapon weapon;
    public GameObject attackcore;

    public GameObject effect;
    public GameObject effectpos;

    public GameObject bullet;
    public GameObject bulletpos;

    public int damage;

    public bool canshoot = true;

    public void Effect()
    {
        if (canshoot)
        {
            if (attackcore.GetComponent<attackcore>().weaponsmagazine.Find(x => x.Weapon == weapon).Remainmagazine > 0)
            {
                Instantiate(effect, effectpos.transform.position, transform.rotation);
            }
                
        }
       
    }

    public void Shoot()
    {
        if (canshoot)
        {
            if (attackcore.GetComponent<attackcore>().weaponsmagazine.Find(x => x.Weapon == weapon).Remainmagazine > 0)
            {
                GameObject currentbullet = Instantiate(bullet, bulletpos.transform.position, transform.rotation);
                currentbullet.GetComponent<playerbullet>().damage = damage;
            }

            Magazine magazine = attackcore.GetComponent<attackcore>().weaponsmagazine.Find(x => x.Weapon == weapon);
            magazine.IfShoot();
        }
        
    }
}
