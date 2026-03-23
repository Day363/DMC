using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;

public class playerskillmove : MonoBehaviour
{
    public static event Action Whenattackend;

    public GameObject attackcore;
    public string chat;

    public GameObject cameramanager;
    public GameObject Gamemanager;

    public GameObject enemyposition;
    public GameObject cameraposition;

    public bool fixenemy;
    public bool cameraset;

    public float camerasize;
    public float damageint;
    public float distance;
    public float time;

    public List<GameObject> effects;

    public void Start()
    {
        cameramanager = battalemanager.Instance.cameramanager;
    }

    public void FixedUpdate()
    {
        if (fixenemy)
        {
            if (battalemanager.Instance.currentenemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                battalemanager.Instance.currentenemy.transform.position = enemyposition.transform.position;
                battalemanager.Instance.currentenemy.transform.rotation = enemyposition.transform.rotation;
            }
            else
            {
                battalemanager.Instance.currentenemy.transform.parent.position = enemyposition.transform.position;
                battalemanager.Instance.currentenemy.transform.parent.rotation = enemyposition.transform.rotation;
            }
        }

    }

    public void LateUpdate()
    {

        if (cameraset)
        {
            cameramanager.GetComponent<CameraManager>().maincam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = camerasize;
        }
    }

    public void FixedDamage_(float damint)
    {
        float damage = GetComponent<playerstatus>().attackpower * damint;
        battalemanager.Instance.currentenemy.GetComponent<boss_hpbar>().Damage((int)damage);
    }

    public void SlashDamage_(float damint)
    {
        float damage = GetComponent<playerstatus>().attackpower * damint;
        battalemanager.Instance.currentenemy.GetComponent<boss_hpbar>().SlashDamage((int)damage);
    }

    public void PenetrateDamage_(float damint)
    {
        float damage = GetComponent<playerstatus>().attackpower * damint;
        battalemanager.Instance.currentenemy.GetComponent<boss_hpbar>().PenetrateDamage((int)damage);
    }

    public void BlowDamage_(float damint)
    {
        float damage = GetComponent<playerstatus>().attackpower * damint;
        battalemanager.Instance.currentenemy.GetComponent<boss_hpbar>().BlowDamage((int)damage);
    }

    public void Chat()
    {
        GetComponent<playerstatus>().StartTyping(chat);
    }

    public void CamaraSet()
    {
        cameramanager.GetComponent<CameraManager>().LookSkillposition(cameraposition);
        cameraset = true;
    }

    public void CameraReturn()
    {
        cameramanager.GetComponent<CameraManager>().LookPlayer();
        cameraset = false;
    }

    public void CameraReturnTime(float time)
    {
        StartCoroutine(CameraReturnTime_co(time)); 
    }

    IEnumerator CameraReturnTime_co(float time)
    {
        yield return new WaitForSeconds(time);
        cameramanager.GetComponent<CameraManager>().LookPlayer();
        cameraset = false;
    }

    public void CameraShortZoomin()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(()=> camerasize, x => camerasize = x, 5.5f, 0.2f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraLongZoomin()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 5.5f, 0.8f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraShortZoomin2()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 3f, 0.1f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraLongZoomin2()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 3f, 0.8f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraShortZoomout()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 10f, 0.2f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraLongZoomout()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 10f, 0.8f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraShortZoomout2()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 5.8f, 0.2f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraLongZoomout2()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 5.8f, 0.8f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraZoomOut10_2()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 10f, 2f).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraZoomInFree(float time)
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 4f, time).SetUpdate(UpdateType.Late).SetId("CameraZoom");

    }
    public void CameraZoomOutFree(float time)
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 10f, time).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CamVib1()
    {
        cameramanager.GetComponent<CameraManager>().CamVibration1();
    }

    public void CantMove()
    {
        GetComponent<PlayerMove>().canmove = false;
        attackcore.GetComponent<attackcore>().canattack = false;
    }

    public void CanMove()
    {
        Whenattackend?.Invoke();

        GetComponent<PlayerMove>().canmove = true;
        attackcore.GetComponent<attackcore>().AmalgamedAnimation();
        attackcore.GetComponent<attackcore>().canattack = true;

    }

    public void FixSight()
    {
        if (transform.position.x < battalemanager.Instance.currentenemy.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<PlayerMove>().dir = 1;
        }
        if (transform.position.x > battalemanager.Instance.currentenemy.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            GetComponent<PlayerMove>().dir = -1;
        }
    }

    public void FixEnemySpecificPoint()
    {
        fixenemy = true;
    }

    public void UnfixEnemySpecificPoint()
    {
        fixenemy = false;
    }

    public void EnemyAddForce(float force)
    {
        if (battalemanager.Instance.currentenemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            if (GetComponent<PlayerMove>().dir == 1)
            {
                rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
            }
            if (GetComponent<PlayerMove>().dir == -1)
            {
                rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
            }
        }
        else if (battalemanager.Instance.currentenemy.transform.parent.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2))
        {
            if (GetComponent<PlayerMove>().dir == 1)
            {
                rb2.AddForce(Vector2.right * force, ForceMode2D.Impulse);
            }
            if (GetComponent<PlayerMove>().dir == -1)
            {
                rb2.AddForce(Vector2.left * force, ForceMode2D.Impulse);
            }
        }
    }

    public void MovetoBackOfEnemy()
    {
        if (GetComponent<PlayerMove>().dir == 1)
        {
            Vector3 tomove = new Vector3(battalemanager.Instance.currentenemy.transform.position.x + distance, transform.position.y, 0);
            transform.DOMove(tomove, time);
        }
        if (GetComponent<PlayerMove>().dir == -1)
        {
            Vector3 tomove = new Vector3(battalemanager.Instance.currentenemy.transform.position.x - distance, transform.position.y, 0);
            transform.DOMove(tomove, time);
        }
    }

    public void MovetoBackOfEnemyEaseoutCubic()
    {
        if (GetComponent<PlayerMove>().dir == 1)
        {
            Vector3 tomove = new Vector3(battalemanager.Instance.currentenemy.transform.position.x + distance, transform.position.y, 0);
            transform.DOMove(tomove, time).SetEase(Ease.OutCubic);
        }
        if (GetComponent<PlayerMove>().dir == -1)
        {
            Vector3 tomove = new Vector3(battalemanager.Instance.currentenemy.transform.position.x - distance, transform.position.y, 0);
            transform.DOMove(tomove, time).SetEase(Ease.OutCubic);
        }
    }

    public void EndORStartAttack()
    {
        if (attackcore.GetComponent<attackcore>().standbyskills.Count < 1)
        {
            attackcore.GetComponent<attackcore>().EndStandbySkill();
        }
        if (attackcore.GetComponent<attackcore>().standbyskills.Count >= 1)
        {
            attackcore.GetComponent<attackcore>().UseStandbySkill();
        }
        
    }
}
