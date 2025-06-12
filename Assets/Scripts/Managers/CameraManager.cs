using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public GameObject maincam;
    public GameObject enemy;

    public GameObject playercam;
    public GameObject enemycam;
    public GameObject smallpointcam;

    public void Awake()
    {
        maincam = playercam;
    }

    public void LookPlayer()
    {
        maincam = playercam;
        GetComponent<Animator>().SetTrigger("playercam");
    }

    public void LookEnemy()
    {
        maincam = enemycam;
        enemycam.GetComponent<CinemachineVirtualCamera>().Follow = enemy.transform;
        GetComponent<Animator>().SetTrigger("playercam");
    }

    public void Looksmallpoint(GameObject target)
    {
        maincam = smallpointcam;
        smallpointcam.GetComponent<CinemachineVirtualCamera>().Follow = target.transform;
        GetComponent<Animator>().SetTrigger("smallpoint");
    }
}
