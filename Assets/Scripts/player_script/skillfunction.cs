using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class skillfunction : MonoBehaviour
{
    public GameObject gamemanger;
    public playerstatus playerStackHandler;
    public GameObject cammanager;

    public Dictionary<string, Action> commandMap;
    public Stack Inference;
    public GameObject lazer2;

    void Start()
    {
        // 명령어와 함수 매핑
        commandMap = new Dictionary<string, Action>
        {
            { "trapal_shoot", Trapal_shoot },
            { "Trapal_Add_Inference", Trapal_Add_Inference },
            { "Trapal_shoot", Trapal_shoot }
        };
    }

    public void Trapal_shoot()
    {
        StartCoroutine(Trapal_shoot_co());
    }

    IEnumerator Trapal_shoot_co()
    {
        List<GameObject> lazer2s = new List<GameObject> { };
        GameObject curstartlazer2;
        yield return new WaitForSeconds(0.7f);
        if (GetComponent<PlayerMove>().dir == 1)
        {
            curstartlazer2 = Instantiate(lazer2, new Vector3(transform.position.x + 1.73f, transform.position.y + 0.36f, -6.5f), Quaternion.Euler(0, 70, 0));
        }
        else
        {
            curstartlazer2 = Instantiate(lazer2, new Vector3(transform.position.x - 1.73f, transform.position.y + 0.36f, -6.5f), Quaternion.Euler(0, 70, 0));
            curstartlazer2.transform.localScale = new Vector3(0.5f, 0.5f, -0.5f);
        }
        curstartlazer2.GetComponent<player_trapal_lazer2>().target = gamemanger.GetComponent<battalemanager>().currentenemy;
        curstartlazer2.GetComponent<player_trapal_lazer2>().look = false;
        curstartlazer2.GetComponent<player_trapal_lazer2>().cammanager = cammanager;
        yield return new WaitForSeconds(0.5f);
        curstartlazer2.GetComponent<player_trapal_lazer2>().ShootNotDes();
        playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
        if (instance != null)
        {
            if (instance.currentStack >= 3)
            {
                int runs = instance.currentStack / 3;
                for (int i = 0; i < runs; i++)
                {
                    if (GetComponent<PlayerMove>().dir == 1)
                    {
                        yield return new WaitForSeconds(0.25f);
                        GameObject curlazer2 = Instantiate(lazer2, new Vector3(transform.position.x - UnityEngine.Random.Range(3f, 7f), transform.position.y + UnityEngine.Random.Range(-6f, 6f), -6.5f), Quaternion.identity);
                        lazer2s.Add(curlazer2);
                        curlazer2.GetComponent<player_trapal_lazer2>().cammanager = cammanager;
                        curlazer2.GetComponent<player_trapal_lazer2>().target = gamemanger.GetComponent<battalemanager>().currentenemy;
                        curlazer2.GetComponent<player_trapal_lazer2>().look = true;

                    }
                    else
                    {
                        yield return new WaitForSeconds(0.25f);
                        GameObject curlazer2 = Instantiate(lazer2, new Vector3(transform.position.x + UnityEngine.Random.Range(3f, 7f), transform.position.y + UnityEngine.Random.Range(-6f, 6f), -6.5f), Quaternion.identity);
                        lazer2s.Add(curlazer2);
                        curlazer2.GetComponent<player_trapal_lazer2>().cammanager = cammanager;
                        curlazer2.GetComponent<player_trapal_lazer2>().target = gamemanger.GetComponent<battalemanager>().currentenemy;
                        curlazer2.GetComponent<player_trapal_lazer2>().look = true;
                    }

                }
                yield return new WaitForSeconds(1.3f);
                curstartlazer2.GetComponent<player_trapal_lazer2>().Shoot();
            }
            else if (instance.currentStack <= 2)
            {
                yield return new WaitForSeconds(0.8f);
                curstartlazer2.GetComponent<player_trapal_lazer2>().Shoot();
            }


        }
        else
        {
            yield return new WaitForSeconds(1f);
            curstartlazer2.GetComponent<player_trapal_lazer2>().Shoot();
        }

    }

    public void Trapal_Add_Inference()
    {
        GetComponent<playerstatus>().ApplyStack(Inference, 1);
        GetComponent<playerstatus>().PrintStacks();
    }

    public void ExecuteCommand(List<string> commands)
    {
        foreach (string command in commands)
        {
            if (commandMap.TryGetValue(command, out Action action))
            {
                action.Invoke();
            }
            else
            {
                Debug.LogWarning($"Unknown command: {command}");
            }
        }
        
    }
}
