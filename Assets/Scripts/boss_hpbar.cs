using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boss_hpbar : MonoBehaviour
{
    public GameObject hpbar;
    public TMP_Text hptext;
    public GameObject canvas;
    public GameObject damagepos;
    public Slider hpbarhp;

    RectTransform hpbarrect;

    public GameObject damagetext;

    public float maxhealth;
    public float currenthealth;
    public float maxbalance;
    public float currentbalance;

    public float slashtolerance;
    public float penetratetolerance;
    public float blowtolerance;

    public float height;
    public float side;

    private void Start()
    {
        currenthealth = maxhealth;
    }

    private void Update()
    {
        Vector3 hpbarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + side, transform.position.y + height, 0));
        hpbar.transform.position = hpbarpos;
    }

    public void CheckHp(int damage)
    {
        if (hpbar != null)
            hpbarhp.value = currenthealth / maxhealth;
            hptext.text = currenthealth.ToString();
    }

    public void Damage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage;
        CheckHp(damage);
        if (currenthealth <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }

    public void SlashDamage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage * slashtolerance;
        GameObject damt = Instantiate(damagetext);
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        CheckHp(damage);
        if (currenthealth <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }

    public void PenetrateDamage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage * penetratetolerance;
        GameObject damt = Instantiate(damagetext);
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        CheckHp(damage);
        if (currenthealth <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }

    public void BlowDamage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage * blowtolerance;
        GameObject damt = Instantiate(damagetext);
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        CheckHp(damage);
        if (currenthealth <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }
}
