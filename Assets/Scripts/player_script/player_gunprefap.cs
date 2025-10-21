using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_gunprefap : MonoBehaviour
{
    public bool hitscan;
    public LayerMask client;
    public LayerMask normalenemy;

    public bool slash;
    public bool penetrate;
    public bool blow;

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
            if (!hitscan)
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
            else if (hitscan)
            {
                RaycastHit2D hit = Physics2D.Raycast(bulletpos.transform.position, bulletpos.transform.right, 500f, client);
                Vector3 endPoint;
                if (hit.collider != null)
                {
                    endPoint = hit.point;
                    transform.GetChild(0).GetComponent<playerattackdamage>().OnTriggerEnter2D(hit.collider);
                }
                else
                {
                    endPoint = bulletpos.transform.position + bulletpos.transform.right * 500f;
                }

                // 선 이펙트 표시
                StartCoroutine(ShowLine(bulletpos.transform.position, endPoint));
            }
            
        }
        
    }

    IEnumerator ShowLine(Vector3 start, Vector3 end)
    {
        LineRenderer ir = GetComponent<LineRenderer>();
        ir.SetPosition(0, start);
        ir.SetPosition(1, end);
        ir.enabled = true;

        yield return new WaitForSeconds(0.15f);

        ir.enabled = false;
    }
}
