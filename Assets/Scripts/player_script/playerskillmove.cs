using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class playerskillmove : MonoBehaviour
{
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

    public void FixedUpdate()
    {
        if (fixenemy)
        {
            Gamemanager.GetComponent<battalemanager>().currentenemy.transform.position = enemyposition.transform.position;
            Gamemanager.GetComponent<battalemanager>().currentenemy.transform.rotation = enemyposition.transform.rotation;
        }

    }

    public void LateUpdate()
    {

        if (cameraset)
        {
            cameramanager.GetComponent<CameraManager>().maincam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = camerasize;
        }
    }

    public void FixedDamage()
    {
        float damage = GetComponent<playerstatus>().attackpower * damageint;
        Gamemanager.GetComponent<battalemanager>().currentenemy.GetComponent<boss_hpbar>().Damage((int)damage);
    }

    public void SlashDamage()
    {
        float damage = GetComponent<playerstatus>().attackpower * damageint;
        Gamemanager.GetComponent<battalemanager>().currentenemy.GetComponent<boss_hpbar>().SlashDamage((int)damage);
    }

    public void PenetrateDamage()
    {
        float damage = GetComponent<playerstatus>().attackpower * damageint;
        Gamemanager.GetComponent<battalemanager>().currentenemy.GetComponent<boss_hpbar>().PenetrateDamage((int)damage);
    }

    public void BlowDamage()
    {
        float damage = GetComponent<playerstatus>().attackpower * damageint;
        Gamemanager.GetComponent<battalemanager>().currentenemy.GetComponent<boss_hpbar>().BlowDamage((int)damage);
    }

    public void CamaraSet()
    {
        cameramanager.GetComponent<CameraManager>().LookSkillposition(cameraposition);
        cameraset = true;
    }

    public void CameraReturn()
    {
        cameraset = false;
        cameramanager.GetComponent<CameraManager>().LookPlayer();
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

    public void CantMove()
    {
        GetComponent<PlayerMove>().canmove = false;
    }

    public void CanMove()
    {
        GetComponent<PlayerMove>().canmove = true;
    }

    public void FixSight()
    {
        if (transform.position.x < Gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x)
        {
            GetComponent<PlayerMove>().dir = 1;
        }
        if (transform.position.x > Gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x)
        {
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

    public void MovetoBackOfEnemy()
    {
        if (GetComponent<PlayerMove>().dir == 1)
        {
            Vector3 tomove = new Vector3(Gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x + distance, transform.position.y, 0);
            transform.DOMove(tomove, time);
        }
        if (GetComponent<PlayerMove>().dir == -1)
        {
            Vector3 tomove = new Vector3(Gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x - distance, transform.position.y, 0);
            transform.DOMove(tomove, time);
        }
    }

    public void MovetoBackOfEnemyEaseoutCubic()
    {
        if (GetComponent<PlayerMove>().dir == 1)
        {
            Vector3 tomove = new Vector3(Gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x + distance, transform.position.y, 0);
            transform.DOMove(tomove, time).SetEase(Ease.OutCubic);
        }
        if (GetComponent<PlayerMove>().dir == -1)
        {
            Vector3 tomove = new Vector3(Gamemanager.GetComponent<battalemanager>().currentenemy.transform.position.x - distance, transform.position.y, 0);
            transform.DOMove(tomove, time).SetEase(Ease.OutCubic);
        }
    }
}
