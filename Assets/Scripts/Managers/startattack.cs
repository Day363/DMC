using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startattack : MonoBehaviour
{
    public GameObject attackcore;
    public GameObject selectUi;

    public void StartFight()
    {
        Time.timeScale = 1f;
        selectUi.SetActive(false);
        attackcore.GetComponent<attackcore>().ArreyComplete();
        attackcore.GetComponent<attackcore>().StartCircum();
    }
}
