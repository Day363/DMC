using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boss_hpbar : MonoBehaviour
{
    public GameObject attackcore;

    public GameObject damagepos;

    public GameObject balancebar;
    public GameObject canvas;
    public Slider balancebarint;

    RectTransform hpbarrect;

    public GameObject damagetext;

    public float collapsefloat;

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
        balancebarint.maxValue = maxbalance;
        currentbalance = 0;
    }

    private void Update()
    {
        Vector3 balancebarpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + side, transform.position.y + height, 0));
        balancebar.transform.position = balancebarpos;
    }

    public void BalanceCheck()
    {
        balancebarint.value = currentbalance;
    }

    public void BalanceDamage(float balance)
    {
        currentbalance += balance;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            if (attackcore.GetComponent<attackcore>().standbyskills.Count > 0)
            {
                attackcore.GetComponent<attackcore>().UseStandbySkill();
            }

            GetComponent<Animator>().SetTrigger("collapse");
            GetComponent<Animator>().SetBool("idle", false);
            // ±ÕÇüºØ±«
        }
    }

    IEnumerator Collapsetimeout()
    {
        yield return new WaitForSeconds(collapsefloat);
        GetComponent<Animator>().SetBool("collapse", false);
    }

    public void Damage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage;
        BalanceDamage(damage * 0.1f);
        if (currenthealth <= 0)
        {
            //* Ã¼·ÂÀÌ 0 ÀÌÇÏ¶ó Á×À½
        }
    }

    public void SlashDamage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage * slashtolerance;
        BalanceDamage(damage * 0.1f);
        GameObject damt = Instantiate(damagetext);
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        if (currenthealth <= 0)
        {
            //* Ã¼·ÂÀÌ 0 ÀÌÇÏ¶ó Á×À½
        }
    }

    public void PenetrateDamage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage * penetratetolerance;
        BalanceDamage(damage * 0.1f);
        GameObject damt = Instantiate(damagetext);
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        if (currenthealth <= 0)
        {
            //* Ã¼·ÂÀÌ 0 ÀÌÇÏ¶ó Á×À½
        }
    }

    public void BlowDamage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage * blowtolerance;
        BalanceDamage(damage * 0.1f);
        GameObject damt = Instantiate(damagetext);
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        if (currenthealth <= 0)
        {
            //* Ã¼·ÂÀÌ 0 ÀÌÇÏ¶ó Á×À½
        }
    }
}
