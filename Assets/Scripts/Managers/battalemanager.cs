using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battalemanager : MonoBehaviour
{
    public static battalemanager Instance;

    public GameObject player;
    public GameObject attackcore;
    public GameObject currentenemy;

    private void Awake()
    {
        
        Instance = this;
       
    }

    public void Battlestart()
    {
        attackcore.GetComponent<attackcore>().SetCronometer();
    }

    public static void EnemyAttackDisabled(GameObject currentattack)
    {
        Instance.StartCoroutine(Instance.EnemyAttackDisabled_co(currentattack));
    }

    IEnumerator EnemyAttackDisabled_co(GameObject currentattack)
    {
        currentattack.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        currentattack.SetActive(true);
    }
}
