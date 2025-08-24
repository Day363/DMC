using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class playerhit : MonoBehaviour
{
    public GameObject cammanager;
    public GameObject gamemanager;

    public GameObject evasiontext;

    public bool defense;
    public bool counter;
    public bool evasion;
    public bool immobility;
    public bool offset;

    public float defenseCoef;
    public float counterCoef;
    public float evasionCoef;
    public float offsetCoef;
    public string counteranimationtrigger;

    public void Hit(int damage)
    {
        playerstatus playerstatus_com = GetComponent<playerstatus>();
        int culdam = 0;
        if (defense)
        {
            culdam = Mathf.Max(1, damage - (int)(playerstatus_com.attackpower * defenseCoef));
        }
        if (counter)
        {
            culdam = damage;
            GetComponent<Animator>().SetTrigger(counteranimationtrigger);
        }
        if (evasion)
        {
            if (damage < playerstatus_com.attackpower * evasionCoef)
            {
                Instantiate(evasiontext, transform.position, Quaternion.identity);
                return;
            }
            if (damage > playerstatus_com.attackpower * evasionCoef)
            {
                culdam = (int)(damage * 1.5f);
            }
        }
        if (offset)
        {
            if (damage < playerstatus_com.attackpower * offsetCoef)
            {
                gamemanager.GetComponent<battalemanager>().currentenemy.GetComponent<boss_hpbar>().BalanceDamage((int)(playerstatus_com.attackpower * offsetCoef) - damage);
                return;
            }
            if (damage > playerstatus_com.attackpower * offsetCoef)
            {
                culdam = damage - (int)(damage - (playerstatus_com.attackpower * offsetCoef));
            }
        }
        GetComponent<playerstatus>().BalanceDamage(culdam);
        StartCoroutine(Hitcamera());
    }

    public void StrongHit(int damage, Transform attacktransform)
    {
        StartCoroutine(Hitcamera());
        GetComponent<Animator>().SetBool("knockback", true);
        GetComponent<PlayerMove>().canmove = false;
        StartCoroutine(knockbackcool());
        GetComponent<playerstatus>().BalanceDamage(damage);
        int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 3 : -3;
        if (dir < 0)
        {
            GetComponent<PlayerMove>().LookRight();
        }
        else
        {
            GetComponent<PlayerMove>().LookLeft();
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 0), ForceMode2D.Impulse);
    }

    

    public void FlyAway(float power, float up)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(power, up), ForceMode2D.Impulse);
    }

    public void Update()
    {
        
    }

    IEnumerator knockbackcool()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerMove>().canmove = true;
        GetComponent<Animator>().SetBool("knockback", false);
    }

    IEnumerator Hitcamera()
    {
        cammanager.GetComponent<CameraManager>().maincam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 7;
        cammanager.GetComponent<CameraManager>().maincam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
        yield return new WaitForSeconds(0.1f);
        cammanager.GetComponent<CameraManager>().maincam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cammanager.GetComponent<CameraManager>().maincam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

}
