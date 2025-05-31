using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boss_hpbar : MonoBehaviour
{
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

    public void BalanceDamage(int balance)
    {
        currentbalance += balance;
        BalanceCheck();
        if (currentbalance >= maxbalance)
        {
            currentbalance = 0;
            StartCoroutine(Collapsetime());
            // ±ÕÇüºØ±«
        }
    }

    IEnumerator Collapsetime()
    {
        GetComponent<Animator>().SetBool("collapse", true);
        yield return new WaitForSeconds(collapsefloat);
        GetComponent<Animator>().SetBool("collapse", false);
    }

    public void Damage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage;
        currentbalance += damage * 0.1f;
        BalanceCheck();
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
        currentbalance += damage * 0.1f;
        BalanceCheck();
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
        currentbalance += damage * 0.1f;
        BalanceCheck();
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
        currentbalance += damage * 0.1f;
        BalanceCheck();
        GameObject damt = Instantiate(damagetext);
        damt.transform.position = damagepos.transform.position;
        damt.GetComponent<damagetext>().damage = damage;
        if (currenthealth <= 0)
        {
            //* Ã¼·ÂÀÌ 0 ÀÌÇÏ¶ó Á×À½
        }
    }
}
