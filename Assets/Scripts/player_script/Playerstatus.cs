using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerstatus : MonoBehaviour
{
    [SerializeField]
    public Slider balancebar;
    public TMP_Text hptext;
    public Slider balancebarint;

    public float side;
    public float height;

    public float maxbalance;
    public float currentbalance;
    public float speed;
    public int attackpower;

    private void Start()
    {
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
            // ±ÕÇüºØ±«
        }
    }

}
