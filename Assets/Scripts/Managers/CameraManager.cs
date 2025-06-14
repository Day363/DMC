using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public GameObject maincam;
    public GameObject enemy;

    public GameObject playercam;
    public GameObject enemycam;
    public GameObject smallpointcam;
    public GameObject skillcam;

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

    public void LookSkillposition(GameObject target)
    {
        maincam = skillcam;
        skillcam.GetComponent<CinemachineVirtualCamera>().Follow = target.transform;
        GetComponent<Animator>().SetTrigger("skillcam");
    }
}
