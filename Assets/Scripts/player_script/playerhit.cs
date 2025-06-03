using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class playerhit : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    public void Hit(int damage)
    {
        GetComponent<playerstatus>().Damage(damage);
        StartCoroutine(Hitcamera());
    }

    public void StrongHit(int damage, Transform attacktransform)
    {
        StartCoroutine(Hitcamera());
        GetComponent<Animator>().SetBool("knockback", true);
        GetComponent<PlayerMove>().canmove = false;
        StartCoroutine(knockbackcool());
        GetComponent<playerstatus>().Damage(damage);
        int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 3 : -3;
        if (dir < 0)
        {
            GetComponent<PlayerMove>().dir = 1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<PlayerMove>().dir = -1;
            GetComponent<SpriteRenderer>().flipX = true;
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
        virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 7;
        virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
        yield return new WaitForSeconds(0.1f);
        virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

}
