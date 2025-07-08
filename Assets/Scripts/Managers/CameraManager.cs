using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public GameObject priorcamera;

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

    public void CamVibration1()
    {
        StartCoroutine(CamVib1());
    }

    public void CamStable()
    {
        StartCoroutine(FuckCinemachine());
    }
    
    IEnumerator FuckCinemachine()
    {
        priorcamera.GetComponent<CinemachineBrain>().enabled = false;
        yield return new WaitForEndOfFrame();
        priorcamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        priorcamera.GetComponent<CinemachineBrain>().enabled = true;
    }

    IEnumerator CamVib1()
    {
        maincam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 12;
        maincam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
        yield return new WaitForSeconds(0.2f);
        maincam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        maincam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        CamStable();
    }

    
}
