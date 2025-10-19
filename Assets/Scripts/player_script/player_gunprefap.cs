using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_gunprefap : MonoBehaviour
{
    public Weapon weapon;
    public GameObject attackcore;
    public GameObject player;

    public GameObject effect;
    public GameObject effectpos;

    public GameObject bullet;
    public GameObject bulletpos;

    public float damagenmum;
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

    public void DamageDecision()
    {
        damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenmum);
        transform.GetChild(0).GetComponent<playerattackdamage>().damagenum = damagenmum;
    }

    public void Shoot_notbullet()
    {
        if (canshoot)
        {
            Magazine magazine = attackcore.GetComponent<attackcore>().weaponsmagazine.Find(x => x.Weapon == weapon);
            magazine.IfShoot();
        }
    }

    public void Shoot()
    {
        damage = Mathf.RoundToInt(player.GetComponent<playerstatus>().attackpower * damagenmum);

        if (canshoot)
        {
            if (attackcore.GetComponent<attackcore>().weaponsmagazine.Find(x => x.Weapon == weapon).Remainmagazine > 0)
            {
                GameObject currentbullet = Instantiate(bullet, bulletpos.transform.position, transform.rotation);
                playerbullet currentbulletplayerbullet = currentbullet.GetComponent<playerbullet>();
                currentbulletplayerbullet.damage = damage;
                currentbulletplayerbullet.attackcore = attackcore;
            }

            Magazine magazine = attackcore.GetComponent<attackcore>().weaponsmagazine.Find(x => x.Weapon == weapon);
            magazine.IfShoot();
        }
        
    }
}
