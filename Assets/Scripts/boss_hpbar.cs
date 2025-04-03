using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss_hpbar : MonoBehaviour
{
    public GameObject hpbar;
    public GameObject canvas;
    public Slider hpbarhp;

    RectTransform hpbarrect;

    public float maxhealth;
    public float currenthealth;

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

    public void CheckHp()
    {
        if (hpbar != null)
            hpbarhp.value = currenthealth / maxhealth;
    }

    public void Damage(int damage)
    {
        if (maxhealth == 0 || currenthealth <= 0)
            return;
        currenthealth -= damage;
        CheckHp();
        if (currenthealth <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }
}
