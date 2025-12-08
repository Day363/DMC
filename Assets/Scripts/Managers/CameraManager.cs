using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public bool fuckcinemachine;
    public bool resetRotation;
    public bool killcam;

    public GameObject braincam;

    public GameObject Gamemanager;

    public float killcamsize;

    public GameObject priorcamera;

    public GameObject maincam;
    public GameObject enemy;

    public GameObject playercam;
    public GameObject bigcam;
    public GameObject enemycam;
    public GameObject smallpointcam;
    public GameObject skillcam;
    public GameObject counselcam;

    public static CameraManager Instance;

    public void Awake()
    {
        battalemanager.Instance.cameramanager = gameObject;
        Instance = this;
        maincam = playercam;
    }

    public void Update()
    {
        if (fuckcinemachine)
        {
            GetComponent<CinemachineConfiner2D>().InvalidateCache();
        }
        if (killcam)
        {
            maincam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = killcamsize;
        }
    }

    public void CinemachineInvalidateCache()
    {
        StartCoroutine(Fuckcinemachinetwice());
    }

    IEnumerator Fuckcinemachinetwice()
    {
        fuckcinemachine = true;
        yield return new WaitForSeconds(3f);
        fuckcinemachine = false;
    }

    public void LookPlayer()
    {
        maincam = playercam;
        GetComponent<Animator>().SetTrigger("playercam");
    }

    public void LookCounsel(GameObject target)
    {
        maincam = counselcam;
        counselcam.GetComponent<CinemachineVirtualCamera>().Follow = target.transform;
        GetComponent<Animator>().SetTrigger("counselcam");
    }

    public void LookBigCam()
    {
        maincam = bigcam;
        GetComponent<Animator>().SetTrigger("bigcam");
    }

    public void LookEnemy()
    {
        maincam = enemycam;
        enemycam.GetComponent<CinemachineVirtualCamera>().Follow = battalemanager.Instance.currentenemy.transform;
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

    public void ShakeCamera(float strength, float duration)
    {
        StartCoroutine(ShakeCameraC(strength, duration));
    }

    IEnumerator ShakeCameraC(float strength, float duration)
    {
        CinemachineVirtualCamera maincamCinemachineVirtualCamera = maincam.GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin maincamCinemachineBasicMultiChannelPerlin = maincamCinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = strength;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = 1;
        yield return new WaitForSeconds(duration);
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
        CamStable();
    }

    public void CamVibTimeIgnore()
    {
        braincam.transform.DOShakePosition(0.12f, strength: 0.3f).SetUpdate(true);
    }

    public void CamVibration0_5()
    {
        StartCoroutine(CamVib0_5());
    }

    public void CamVibration1()
    {
        StartCoroutine(CamVib1());
    }

    public void CamVibration20()
    {
        StartCoroutine(CamVib20());
    }

    public void KIllcam()
    {
        StartCoroutine(Killcam_co());
    }

    IEnumerator Killcam_co()
    {
        float backpriorty = maincam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
        maincam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5f;
        killcamsize = 5f;
        killcam = true;
        DOTween.To(() => killcamsize, x => killcamsize = x, 10f, 3f).SetEase(Ease.OutQuart);
        yield return new WaitForSecondsRealtime(3f);
        killcam = false;
    }

    public void CamStable()
    {
        StartCoroutine(FuckCinemachine());
    }
    
    public void LimitlessShake(float AmplitudeGain, float FrequencyGain, float time)
    {
        StartCoroutine(CamVibE(AmplitudeGain, FrequencyGain, time));
    }

    IEnumerator FuckCinemachine()
    {
        CinemachineBrain priorcameraCinemachineBrain = priorcamera.GetComponent<CinemachineBrain>();
        priorcameraCinemachineBrain.enabled = false;
        yield return new WaitForEndOfFrame();
        priorcamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        priorcameraCinemachineBrain.enabled = true;
    }

    IEnumerator CamVibE(float AmplitudeGain, float FrequencyGain, float time)
    {
        CinemachineVirtualCamera maincamCinemachineVirtualCamera = maincam.GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin maincamCinemachineBasicMultiChannelPerlin = maincamCinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = AmplitudeGain;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = FrequencyGain;
        yield return new WaitForSeconds(time);
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
        CamStable();
    }

    IEnumerator CamVib1()
    {
        CinemachineVirtualCamera maincamCinemachineVirtualCamera = maincam.GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin maincamCinemachineBasicMultiChannelPerlin = maincamCinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 12;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = 1;
        yield return new WaitForSeconds(0.2f);
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
        CamStable();
    }

    IEnumerator CamVib0_5()
    {
        CinemachineVirtualCamera maincamCinemachineVirtualCamera = maincam.GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin maincamCinemachineBasicMultiChannelPerlin = maincamCinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 6;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = 1;
        yield return new WaitForSeconds(0.12f);
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
        CamStable();
    }

    IEnumerator CamVib20()
    {
        CinemachineVirtualCamera maincamCinemachineVirtualCamera = maincam.GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin maincamCinemachineBasicMultiChannelPerlin = maincamCinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 1;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0.5f;
        yield return new WaitForSeconds(20f);
        maincamCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        maincamCinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
        CamStable();
    }
}
