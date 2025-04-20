using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerstatus : MonoBehaviour
{
    [SerializeField]
    public Slider hpslider;
    public TMP_Text hptext;

    public float max_health;
    public float health;
    public float speed;
    public int attackpower;

    public void CheckHp()
    {
        if (hpslider != null)
        {
            hpslider.value = health / max_health;
            hptext.text = health.ToString();
        }
    }

    public void Damage(int damage) 
    {
        if (max_health == 0 || health <= 0) 
            return;
        health -= damage;
        CheckHp();
        if (health <= 0)
        {
            //* 체력이 0 이하라 죽음
        }
    }

    public void Awake()
    {
        health = max_health;

    }

}
