using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static DefenseSkill;

public class playerhit : MonoBehaviour
{
    public static event Action<GameObject, int> OnPlayerHitCalled;
    public static event Action<GameObject> OnPlayerEvasionCalled;

    public GameObject cammanager;
    public GameObject gamemanager;

    public GameObject evasiontext;

    public GameObject defenseskillui;

    public bool defense;
    public bool evasion;
    public bool counter;
   
    public float defenseCoef;
    public float counterCoef;
    public float evasionCoef;
    public float offsetCoef;
    public string counteranimationtrigger;

    public bool canhit = true;

    public Coroutine currenthitstop;
    public Coroutine currentEmisson;


    public void DefenseUiPosition(GameObject enemy)
    {
        if (transform.position.x < enemy.transform.position.x)
        {
            RectTransform defenseskilluiRectTransform = defenseskillui.GetComponent<RectTransform>();
            defenseskilluiRectTransform.localPosition = new Vector3(-45, 67.8f, 0);
        }
        else if (transform.position.x > enemy.transform.position.x)
        {
            RectTransform defenseskilluiRectTransform = defenseskillui.GetComponent<RectTransform>();
            defenseskilluiRectTransform.localPosition = new Vector3(45, 67.8f, 0);
        }
        
    }

    public void Hit(int damage, GameObject enemy)
    {
        DefenseUiPosition(enemy);

        defense = false;
        evasion = false;
        counter = false;

        playerstatus playerstatus_com = GetComponent<playerstatus>();

        attackcore attackCore = battalemanager.Instance.attackcore.GetComponent<attackcore>();

        DefenseSkill currentdefense = null;

        if (attackCore.defenseSkills.Count > 0)
        {
            currentdefense = attackCore.defenseSkills[0];

            if (currentdefense.defenseType == DefenseType.defense)
                defense = true;

            if (currentdefense.defenseType == DefenseType.evasion)
                evasion = true;

            if (currentdefense.defenseType == DefenseType.counter)
                counter = true;
        }

        if (canhit)
        {
            

            int culdam = damage;

            if (currentdefense != null)
            {
                if (defense)
                {
                    int damdecrease = (int)(currentdefense.calculation * playerstatus_com.attackpower);
                    culdam = Mathf.Max(1, damage - damdecrease);
                }
                else if (evasion)
                {
                    int evasioncal = (int)(currentdefense.calculation * playerstatus_com.attackpower);

                    if (damage <= evasioncal)
                    {
                        GameObject currenttext = Instantiate(evasiontext, transform.position, Quaternion.identity);
                        currenttext.transform.localPosition = transform.position;
                        attackCore.DefenseSkillUiDecrease();

                        OnPlayerEvasionCalled?.Invoke(enemy);

                        return;
                    }

                    culdam = (int)(damage * 1.2f);
                }
                else if (counter)
                {
                    if (currentdefense.counterType == CounterType.slash)
                        enemy.GetComponent<boss_hpbar>().SlashDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.penetrate)
                        enemy.GetComponent<boss_hpbar>().PenetrateDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.blow)
                        enemy.GetComponent<boss_hpbar>().BlowDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.fix)
                        enemy.GetComponent<boss_hpbar>().Damage((int)(currentdefense.calculation * playerstatus_com.attackpower));

                    if (currentdefense.skillprefab != null)
                    {
                        GameObject currentprephep = Instantiate(currentdefense.skillprefab, transform);
                        currentprephep.transform.position = Vector2.zero;
                    }
                    
                }

                Debug.Log("alsldfjarrh");
                attackCore.DefenseSkillUiDecrease();
            }

            RedEmisson();
            currenthitstop = StartCoroutine(HitStop());

            GetComponent<playerstatus>().BalanceDamage(enemy, culdam);
            OnPlayerHitCalled?.Invoke(enemy, culdam);
            Hitcamera();
        }
    }

    public void SlashHit(int damage, GameObject enemy)
    {
        DefenseUiPosition(enemy);

        defense = false;
        evasion = false;
        counter = false;

        playerstatus playerstatus_com = GetComponent<playerstatus>();

        attackcore attackCore = battalemanager.Instance.attackcore.GetComponent<attackcore>();

        DefenseSkill currentdefense = null;

        if (attackCore.defenseSkills.Count > 0)
        {
            currentdefense = attackCore.defenseSkills[0];

            if (currentdefense.defenseType == DefenseType.defense)
                defense = true;

            if (currentdefense.defenseType == DefenseType.evasion)
                evasion = true;

            if (currentdefense.defenseType == DefenseType.counter)
                counter = true;
        }

        if (canhit)
        {
            

            int culdam = damage;

            if (currentdefense != null)
            {
                if (defense)
                {
                    int damdecrease = (int)(currentdefense.calculation * playerstatus_com.attackpower);
                    culdam = Mathf.Max(1, damage - damdecrease);
                }
                else if (evasion)
                {
                    int evasioncal = (int)(currentdefense.calculation * playerstatus_com.attackpower);

                    if (damage <= evasioncal)
                    {
                        GameObject currenttext = Instantiate(evasiontext, transform.position, Quaternion.identity);
                        currenttext.transform.localPosition = transform.position;

                        attackCore.DefenseSkillUiDecrease();

                        return;
                    }

                    culdam = (int)(damage * 1.2f);
                }
                else if (counter)
                {
                    if (currentdefense.counterType == CounterType.slash)
                        enemy.GetComponent<boss_hpbar>().SlashDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.penetrate)
                        enemy.GetComponent<boss_hpbar>().PenetrateDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.blow)
                        enemy.GetComponent<boss_hpbar>().BlowDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.fix)
                        enemy.GetComponent<boss_hpbar>().Damage((int)(currentdefense.calculation * playerstatus_com.attackpower));

                    if (currentdefense.skillprefab != null)
                    {
                        GameObject currentprephep = Instantiate(currentdefense.skillprefab, transform);
                        currentprephep.transform.localPosition = Vector2.zero;
                    }
                }

                Debug.Log("alsldfjarrh");
                attackCore.DefenseSkillUiDecrease();
            }

            RedEmisson();
            currenthitstop = StartCoroutine(HitStop());

            GetComponent<playerstatus>().SlashDamage(enemy, culdam);
            OnPlayerHitCalled?.Invoke(enemy, culdam);
            Hitcamera();
        }
    }

    public void PenetrateHit(int damage, GameObject enemy)
    {
        DefenseUiPosition(enemy);

        defense = false;
        evasion = false;
        counter = false;

        playerstatus playerstatus_com = GetComponent<playerstatus>();

        attackcore attackCore = battalemanager.Instance.attackcore.GetComponent<attackcore>();

        DefenseSkill currentdefense = null;

        if (attackCore.defenseSkills.Count > 0)
        {
            currentdefense = attackCore.defenseSkills[0];

            if (currentdefense.defenseType == DefenseType.defense)
                defense = true;

            if (currentdefense.defenseType == DefenseType.evasion)
                evasion = true;

            if (currentdefense.defenseType == DefenseType.counter)
                counter = true;
        }

        if (canhit)
        {
            

            int culdam = damage;

            if (currentdefense != null)
            {
                if (defense)
                {
                    int damdecrease = (int)(currentdefense.calculation * playerstatus_com.attackpower);
                    culdam = Mathf.Max(1, damage - damdecrease);
                }
                else if (evasion)
                {
                    int evasioncal = (int)(currentdefense.calculation * playerstatus_com.attackpower);

                    if (damage <= evasioncal)
                    {
                        GameObject currenttext = Instantiate(evasiontext, transform.position, Quaternion.identity);
                        currenttext.transform.localPosition = transform.position;

                        attackCore.DefenseSkillUiDecrease();

                        return;
                    }

                    culdam = (int)(damage * 1.2f);
                }
                else if (counter)
                {
                    if (currentdefense.counterType == CounterType.slash)
                        enemy.GetComponent<boss_hpbar>().SlashDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.penetrate)
                        enemy.GetComponent<boss_hpbar>().PenetrateDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.blow)
                        enemy.GetComponent<boss_hpbar>().BlowDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.fix)
                        enemy.GetComponent<boss_hpbar>().Damage((int)(currentdefense.calculation * playerstatus_com.attackpower));

                    if (currentdefense.skillprefab != null)
                    {
                        GameObject currentprephep = Instantiate(currentdefense.skillprefab, transform);
                        currentprephep.transform.position = Vector2.zero;
                    }
                }

                Debug.Log("alsldfjarrh");
                attackCore.DefenseSkillUiDecrease();
            }

            RedEmisson();
            currenthitstop = StartCoroutine(HitStop());

            GetComponent<playerstatus>().PenetrateDamage(enemy, culdam);
            OnPlayerHitCalled?.Invoke(enemy, culdam);
            Hitcamera();
        }
    }

    public void BlowHit(int damage, GameObject enemy)
    {
        DefenseUiPosition(enemy);

        defense = false;
        evasion = false;
        counter = false;

        playerstatus playerstatus_com = GetComponent<playerstatus>();

        attackcore attackCore = battalemanager.Instance.attackcore.GetComponent<attackcore>();

        DefenseSkill currentdefense = null;

        if (attackCore.defenseSkills.Count > 0)
        {
            currentdefense = attackCore.defenseSkills[0];

            if (currentdefense.defenseType == DefenseType.defense)
                defense = true;

            if (currentdefense.defenseType == DefenseType.evasion)
                evasion = true;

            if (currentdefense.defenseType == DefenseType.counter)
                counter = true;
        }

        if (canhit)
        {
            

            int culdam = damage;

            if (currentdefense != null)
            {
                if (defense)
                {
                    int damdecrease = (int)(currentdefense.calculation * playerstatus_com.attackpower);
                    culdam = Mathf.Max(1, damage - damdecrease);
                }
                else if (evasion)
                {
                    int evasioncal = (int)(currentdefense.calculation * playerstatus_com.attackpower);

                    if (damage <= evasioncal)
                    {
                        GameObject currenttext = Instantiate(evasiontext, transform.position, Quaternion.identity);
                        currenttext.transform.localPosition = transform.position;

                        attackCore.DefenseSkillUiDecrease();

                        return;
                    }

                    culdam = (int)(damage * 1.2f);
                }
                else if (counter)
                {
                    if (currentdefense.counterType == CounterType.slash)
                        enemy.GetComponent<boss_hpbar>().SlashDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.penetrate)
                        enemy.GetComponent<boss_hpbar>().PenetrateDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.blow)
                        enemy.GetComponent<boss_hpbar>().BlowDamage((int)(currentdefense.calculation * playerstatus_com.attackpower));
                    else if (currentdefense.counterType == CounterType.fix)
                        enemy.GetComponent<boss_hpbar>().Damage((int)(currentdefense.calculation * playerstatus_com.attackpower));

                    if (currentdefense.skillprefab != null)
                    {
                        GameObject currentprephep = Instantiate(currentdefense.skillprefab, transform);
                        currentprephep.transform.position = Vector2.zero;
                    }
                }

                Debug.Log("alsldfjarrh");
                attackCore.DefenseSkillUiDecrease();
            }

            RedEmisson();
            currenthitstop = StartCoroutine(HitStop());

            GetComponent<playerstatus>().BlowDamage(enemy, culdam);
            OnPlayerHitCalled?.Invoke(enemy, culdam);
            Hitcamera();
        }
    }

    IEnumerator CounterHitpass()
    {
        canhit = false;
        yield return new WaitForSeconds(0.5f);
        canhit = true;
    }

    //////public void StrongHit(int damage, Transform attacktransform)
    //////{
    //////    if (canhit)
    //////    {
    //////        OnHitCalled?.Invoke();

    //////        RedEmisson();
    //////        currenthitstop = StartCoroutine(HitStop());

    //////        Hitcamera();
    //////        GetComponent<Animator>().SetBool("knockback", true);
    //////        GetComponent<PlayerMove>().canmove = false;
    //////        StartCoroutine(knockbackcool());
    //////        GetComponent<playerstatus>().BalanceDamage(damage);
    //////        int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 1 : -1;
    //////        if (dir < 0)
    //////        {
    //////            GetComponent<PlayerMove>().LookRight();
    //////        }
    //////        else
    //////        {
    //////            GetComponent<PlayerMove>().LookLeft();
    //////        }
    //////        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 0), ForceMode2D.Impulse);
    //////    }
        
    //////}

    //public void SlashStrongHit(int damage, Transform attacktransform)
    //{
    //    if (canhit)
    //    {
    //        OnHitCalled?.Invoke();

    //        currenthitstop = StartCoroutine(HitStop());

    //        Hitcamera();
    //        GetComponent<Animator>().SetBool("knockback", true);
    //        GetComponent<PlayerMove>().canmove = false;
    //        StartCoroutine(knockbackcool());
    //        GetComponent<playerstatus>().SlashDamage(damage);
    //        int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 1 : -1;
    //        if (dir < 0)
    //        {
    //            GetComponent<PlayerMove>().LookRight();
    //        }
    //        else
    //        {
    //            GetComponent<PlayerMove>().LookLeft();
    //        }
    //        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 0), ForceMode2D.Impulse);
    //    }

    //}

    //public void PenetrateStrongHit(int damage, Transform attacktransform)
    //{
    //    if (canhit)
    //    {
    //        OnHitCalled?.Invoke();

    //        currenthitstop = StartCoroutine(HitStop());

    //        Hitcamera();
    //        GetComponent<Animator>().SetBool("knockback", true);
    //        GetComponent<PlayerMove>().canmove = false;
    //        StartCoroutine(knockbackcool());
    //        GetComponent<playerstatus>().PenetrateDamage(damage);
    //        int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 1 : -1;
    //        if (dir < 0)
    //        {
    //            GetComponent<PlayerMove>().LookRight();
    //        }
    //        else
    //        {
    //            GetComponent<PlayerMove>().LookLeft();
    //        }
    //        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 0), ForceMode2D.Impulse);
    //    }

    //}

    //public void BlowStrongHit(int damage, Transform attacktransform)
    //{
    //    if (canhit)
    //    {
    //        OnHitCalled?.Invoke();

    //        currenthitstop = StartCoroutine(HitStop());

    //        Hitcamera();
    //        GetComponent<Animator>().SetBool("knockback", true);
    //        GetComponent<PlayerMove>().canmove = false;
    //        StartCoroutine(knockbackcool());
    //        GetComponent<playerstatus>().BlowDamage(damage);
    //        int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 1 : -1;
    //        if (dir < 0)
    //        {
    //            GetComponent<PlayerMove>().LookRight();
    //        }
    //        else
    //        {
    //            GetComponent<PlayerMove>().LookLeft();
    //        }
    //        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 0), ForceMode2D.Impulse);
    //    }

    //}

    public void FlyAway(float power, float up)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(power, up), ForceMode2D.Impulse);
    }

    public void Update()
    {
        
    }

    IEnumerator HitStop()
    {
        //Debug.Log("hitstop");
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

    public void RedEmisson()
    {
        if (currentEmisson != null)
        {
            StopCoroutine(currentEmisson);
        }
        currentEmisson = StartCoroutine(RedEmisson_co());
    }

    IEnumerator RedEmisson_co()
    {
        DOTween.Kill("playerEmisson");
        GetComponent<SpriteRenderer>().material.DOFloat(0.5f, "_flashamount", 0.1f).SetId("playerEmisson");
        yield return new WaitForSeconds(0.1f);
        DOTween.Kill("playerEmisson");
        GetComponent<SpriteRenderer>().material.DOFloat(0f, "_flashamount", 0.3f).SetId("playerEmisson");
    }

}
