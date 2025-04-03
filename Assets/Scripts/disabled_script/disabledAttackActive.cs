using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledAttackActive : MonoBehaviour
{
    public GameObject[] rush1s;
    public GameObject[] rush2s;
    public GameObject[] attack1s;
    public GameObject[] attack2s;
    public GameObject[] attack3s;
    public GameObject[] attack4s;
    public GameObject[] attack5s;
    public GameObject[] attack6s;
    public GameObject[] attack7s;
    public GameObject[] attack8s;

    public void ActiveRush1()
    {
        for (int i = 0; i < rush1s.Length; i++)
        {
            rush1s[i].SetActive(true);
        }
    }

    public void ActiveRush2()
    {
        for (int i = 0; i < rush2s.Length; i++)
        {
            rush2s[i].SetActive(true);
        }
    }
    public void ActiveAttack1()
    {
        for (int i = 0; i < attack1s.Length; i++)
        {
            attack1s[i].SetActive(true);
        }
    }

    public void ActiveAttack2()
    {
        for (int i = 0; i < attack2s.Length; i++)
        {
            attack2s[i].SetActive(true);
        }
    }

    public void ActiveAttack3()
    {
        for (int i = 0; i < attack3s.Length; i++)
        {
            attack3s[i].SetActive(true);
        }
    }

    public void ActiveAttack4()
    {
        for (int i = 0; i < attack4s.Length; i++)
        {
            attack4s[i].SetActive(true);
        }
    }

    public void ActiveAttack5()
    {
        for (int i = 0; i < attack5s.Length; i++)
        {
            attack5s[i].SetActive(true);
        }
    }

    public void ActiveAttack6()
    {
        for (int i = 0; i < attack6s.Length; i++)
        {
            attack6s[i].SetActive(true);
        }
    }

    public void ActiveAttack7()
    {
        for (int i = 0; i < attack7s.Length; i++)
        {
            attack7s[i].SetActive(true);
        }
    }

    public void ActiveAttack8()
    {
        for (int i = 0; i < attack8s.Length; i++)
        {
            attack8s[i].SetActive(true);
        }
    }
}
