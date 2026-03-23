using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class playerhit : MonoBehaviour
{
    public static event Action OnHitCalled;

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

    public bool canhit = true;

    public Coroutine currenthitstop;

    public void Hit(int damage)
    {
        playerstatus playerstatus_com = GetComponent<playerstatus>();
        if (canhit)
        {
            OnHitCalled?.Invoke();

            int culdam = 0;
            if (defense)
            {
                culdam = Mathf.Max(1, damage - (int)(playerstatus_com.attackpower * defenseCoef));
            }
            if (counter)
            {
                culdam = damage;
                StartCoroutine(CounterHitpass());
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
            else
            {
                culdam = damage;
            }
            GetComponent<playerstatus>().BalanceDamage(culdam);
            Hitcamera();
        }
    }

    public void SlashHit(int damage)
    {
        playerstatus playerstatus_com = GetComponent<playerstatus>();
        if (canhit)
        {
            OnHitCalled?.Invoke();

            int culdam = 0;
            if (defense)
            {
                culdam = Mathf.Max(1, damage - (int)(playerstatus_com.attackpower * defenseCoef));
            }
            if (counter)
            {
                culdam = damage;
                StartCoroutine(CounterHitpass());
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
            else
            {
                culdam = damage;
            }
            GetComponent<playerstatus>().SlashDamage(culdam);
            Hitcamera();
        }
    }

    public void PenetrateHit(int damage)
    {
        playerstatus playerstatus_com = GetComponent<playerstatus>();
        if (canhit)
        {
            OnHitCalled?.Invoke();

            int culdam = 0;
            if (defense)
            {
                culdam = Mathf.Max(1, damage - (int)(playerstatus_com.attackpower * defenseCoef));
            }
            if (counter)
            {
                culdam = damage;
                StartCoroutine(CounterHitpass());
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
            else
            {
                culdam = damage;
            }
            GetComponent<playerstatus>().PenetrateDamage(culdam);
            Hitcamera();
        }
    }

    public void BlowHit(int damage)
    {
        playerstatus playerstatus_com = GetComponent<playerstatus>();
        if (canhit)
        {
            OnHitCalled?.Invoke();

            int culdam = 0;
            if (defense)
            {
                culdam = Mathf.Max(1, damage - (int)(playerstatus_com.attackpower * defenseCoef));
            }
            if (counter)
            {
                culdam = damage;
                StartCoroutine(CounterHitpass());
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
            else
            {
                culdam = damage;
            }
            GetComponent<playerstatus>().BlowDamage(culdam);
            Hitcamera();
        }
    }

    IEnumerator CounterHitpass()
    {
        canhit = false;
        yield return new WaitForSeconds(0.5f);
        canhit = true;
    }

    public void StrongHit(int damage, Transform attacktransform)
    {
        if (canhit)
        {
            OnHitCalled?.Invoke();

            currenthitstop = StartCoroutine(HitStop());

            Hitcamera();
            GetComponent<Animator>().SetBool("knockback", true);
            GetComponent<PlayerMove>().canmove = false;
            StartCoroutine(knockbackcool());
            GetComponent<playerstatus>().BalanceDamage(damage);
            int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 1 : -1;
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
        
    }

    public void SlashStrongHit(int damage, Transform attacktransform)
    {
        if (canhit)
        {
            OnHitCalled?.Invoke();

            currenthitstop = StartCoroutine(HitStop());

            Hitcamera();
            GetComponent<Animator>().SetBool("knockback", true);
            GetComponent<PlayerMove>().canmove = false;
            StartCoroutine(knockbackcool());
            GetComponent<playerstatus>().SlashDamage(damage);
            int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 1 : -1;
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

    }

    public void PenetrateStrongHit(int damage, Transform attacktransform)
    {
        if (canhit)
        {
            OnHitCalled?.Invoke();

            currenthitstop = StartCoroutine(HitStop());

            Hitcamera();
            GetComponent<Animator>().SetBool("knockback", true);
            GetComponent<PlayerMove>().canmove = false;
            StartCoroutine(knockbackcool());
            GetComponent<playerstatus>().PenetrateDamage(damage);
            int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 1 : -1;
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

    }

    public void BlowStrongHit(int damage, Transform attacktransform)
    {
        if (canhit)
        {
            OnHitCalled?.Invoke();

            currenthitstop = StartCoroutine(HitStop());

            Hitcamera();
            GetComponent<Animator>().SetBool("knockback", true);
            GetComponent<PlayerMove>().canmove = false;
            StartCoroutine(knockbackcool());
            GetComponent<playerstatus>().BlowDamage(damage);
            int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 1 : -1;
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

    }

    public void FlyAway(float power, float up)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(power, up), ForceMode2D.Impulse);
    }

    public void Update()
    {
        
    }

    IEnumerator HitStop()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.15f);
        Time.timeScale = 1f;
    }

    IEnumerator knockbackcool()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerMove>().canmove = true;
        GetComponent<Animator>().SetBool("knockback", false);
    }

    void Hitcamera()
    {
        battalemanager.Instance.cameramanager.GetComponent<CameraManager>().CamVibration0_5();
    }

}
