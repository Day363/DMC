using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class battalemanager : MonoBehaviour
{
    public static battalemanager Instance { get; private set; }
    public static event Action WhenDataSave;


    public GameObject player;
    public GameObject attackcore;
    public GameObject currentenemy;
    //public List<GameObject> currentenemys = new List<GameObject>();
    public List<Weapon> playerweaponinv = new List<Weapon> { };
    public List<Rapport> playerrapportinv = new List<Rapport> { };
    public List<item> playeriteminv = new List<item> { };
    public List<GameObject> available_mirror = new List<GameObject> { };
    public List<GameObject> used_mirror = new List<GameObject> { };
    public GameObject fadeout;
    public GameObject cameramanager;
    public GameObject letterbox;
    public GameObject playerchatbox;
    public GameObject itemgiveui;
    public Stack[] stackdatas;
    public Rapport[] rapportdatas;
    public GameObject cronometer;
    public GameObject world_globallight;
    public GameObject player_light;
    public GameObject dashmanager;

    public int number;
    public Sprite numberimage;

    public void DataSaveTo()
    {
        WhenDataSave?.Invoke();
    }

    IEnumerator Uistart()
    {
        yield return SceneManager.LoadSceneAsync("uiscene", LoadSceneMode.Additive);
    }

    private void Awake()
    {
        StartCoroutine(Uistart());

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        yield return new WaitForSeconds(1f);
        currentattack.SetActive(true);
    }

    
}
