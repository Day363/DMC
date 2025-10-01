using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class normal_enemy_hp : MonoBehaviour
{
    public GameObject currenthitobjcet;

    public GameObject gammanager;
    public GameObject cammanager;
    public GameObject attackcore;
    public GameObject damagepos;
    public GameObject damagetext;

    public float slashtolerance;
    public float penetratetolerance;
    public float blowtolerance;

    public float maxhealth;
    public float currenthealth;

    public bool canhit = true;

    private void Start()
    {
        currenthealth = maxhealth;
    }

    public void Redemisson()
    {
        DOTween.Kill("enemyflash");
        GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", 0.5f);
        DOTween.To(() => GetComponent<SpriteRenderer>().material.GetFloat("_flashamount"), value => GetComponent<SpriteRenderer>().material.SetFloat("_flashamount", value), 0f, 0.35f).SetEase(Ease.OutQuart).SetUpdate(true).SetId("enemyflash");
    }

    public void Damage(int damage)
    {
        if (canhit)
        {
            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;
            currenthealth -= damage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }

    }

    public void SlashDamage(int damage)
    {
        if (canhit)
        {
            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;
            currenthealth -= damage * slashtolerance;
            GameObject damt = Instantiate(damagetext);
            damagetext damtdamagetext = damt.GetComponent<damagetext>();
            if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
            {
                damtdamagetext.wherexpos = 1;
            }
            else
            {
                damtdamagetext.wherexpos = -1;
            }
            damtdamagetext.slash = true;
            damt.transform.position = damagepos.transform.position;
            damtdamagetext.damage = damage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }

    }

    public void PenetrateDamage(int damage)
    {
        if (canhit)
        {
            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;
            currenthealth -= damage * penetratetolerance;
            GameObject damt = Instantiate(damagetext);
            damagetext damtdamagetext = damt.GetComponent<damagetext>();
            if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
            {
                damtdamagetext.wherexpos = 1;
            }
            else
            {
                damtdamagetext.wherexpos = -1;
            }
            damtdamagetext.penetarte = true;
            damt.transform.position = damagepos.transform.position;
            damtdamagetext.damage = damage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }

    }

    public void BlowDamage(int damage)
    {
        if (canhit)
        {
            Redemisson();

            cammanager.GetComponent<CameraManager>().CamVibration0_5();
            attackcore.GetComponent<attackcore>().BossDamaged();

            if (maxhealth == 0 || currenthealth <= 0)
                return;
            currenthealth -= damage * blowtolerance;
            GameObject damt = Instantiate(damagetext);
            damagetext damtdamagetext = damt.GetComponent<damagetext>();
            if (gammanager.GetComponent<battalemanager>().player.transform.position.x - gameObject.transform.position.x > 0)
            {
                damtdamagetext.wherexpos = 1;
            }
            else
            {
                damtdamagetext.wherexpos = -1;
            }
            damtdamagetext.blow = true;
            damt.transform.position = damagepos.transform.position;
            damtdamagetext.damage = damage;
            if (currenthealth <= 0)
            {
                Dead();
            }
        }

    }

    public void Dead()
    {
        GetComponent<Animator>().SetTrigger("dying");
    }
}
