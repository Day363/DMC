using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battalemanager : MonoBehaviour
{
    public static battalemanager Instance { get; private set; }

    public GameObject player;
    public GameObject attackcore;
    public GameObject currentenemy;
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

    public int number;
    public Sprite numberimage;

    IEnumerator Uistart()
    {
        yield return SceneManager.LoadSceneAsync("uiscene", LoadSceneMode.Additive);
    }

    private void Awake()
    {
        Debug.Log("Aa");
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
        yield return new WaitForSeconds(0.3f);
        currentattack.SetActive(true);
    }
}
