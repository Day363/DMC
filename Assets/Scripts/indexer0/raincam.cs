using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class raincam : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin perlin;
    public float currentfloat = 0;
    public float softfloat = 2f;
    public float mediumfloat = 4.5f;
    public float hardfloat = 7f;
    public float deathfloat = 20f;
    public int divideint;
    public bool softrain = false;
    public bool mediumrain = false;
    public bool hardrain = false;
    public bool deathrain = false;
    public bool softrainend = false;
    public bool mediumrainend = false;
    public bool hardrainend = false;
    public bool deathrainend = false;

    void Start()
    {
        perlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void FixedUpdate()
    {
        if (softrain)
        {
            currentfloat = currentfloat + softfloat / divideint;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentfloat;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            if (currentfloat >= softfloat)
            {
                softrain = false;
                StartCoroutine(Rain1endcoruetine());
            }
        }

        if (softrainend)
        {
            currentfloat = currentfloat - softfloat / divideint;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentfloat;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            if (currentfloat <= 0)
            {
                softrainend = false;
                currentfloat = 0;
                virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
            }
        }

        if (mediumrain)
        {
            currentfloat = currentfloat + mediumfloat / divideint;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentfloat;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            if (currentfloat >= mediumfloat)
            {
                mediumrain = false;
                StartCoroutine(Rain2endcoruetine());
            }
        }

        if (mediumrainend)
        {
            currentfloat = currentfloat - mediumfloat / divideint;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentfloat;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            if (currentfloat <= 0)
            {
                mediumrainend = false;
                currentfloat = 0;
                virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
            }
        }

        if (hardrain)
        {
            currentfloat = currentfloat + hardfloat / divideint;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentfloat;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            if (currentfloat >= hardfloat)
            {
                hardrain = false;
                StartCoroutine(Rain3endcoruetine());
            }
        }

        if (hardrainend)
        {
            currentfloat = currentfloat - hardfloat / divideint;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentfloat;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            if (currentfloat <= 0)
            {
                hardrainend = false;
                currentfloat = 0;
                virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
            }
        }

        if (deathrain)
        {
            currentfloat = currentfloat + deathfloat / divideint;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentfloat;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            if (currentfloat >= deathfloat)
            {
                deathrain = false;
                StartCoroutine(Rain4endcoruetine());
            }
        }

        if (deathrainend)
        {
            currentfloat = currentfloat - deathfloat / divideint;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentfloat;
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            if (currentfloat <= 0)
            {
                deathrainend = false;
                currentfloat = 0;
                virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
            }
        }
    }

    public void Start1Shake()
    {
        softrain = true;
    }
    public void Start2Shake()
    {
        mediumrain = true;
    }
    public void Start3Shake()
    {
        hardrain = true;
    }
    public void Start4Shake()
    {
        deathrain = true;
    }

    public void StopShake()
    {
        virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
    }

    IEnumerator Rain1endcoruetine()
    {
        yield return new WaitForSeconds(5f);
        Rain1End();
    }

    IEnumerator Rain2endcoruetine()
    {
        yield return new WaitForSeconds(5f);
        Rain2End();
    }

    IEnumerator Rain3endcoruetine()
    {
        yield return new WaitForSeconds(5f);
        Rain3End();
    }

    IEnumerator Rain4endcoruetine()
    {
        yield return new WaitForSeconds(5f);
        Rain4End();
    }

    public void Rain1End()
    {
        softrainend = true;
    }

    public void Rain2End()
    {
        mediumrainend = true;
    }

    public void Rain3End()
    {
        hardrainend = true;
    }

    public void Rain4End()
    {
        deathrainend = true;
    }
}
