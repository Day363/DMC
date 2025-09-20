using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class cutscenemanager : MonoBehaviour
{
    public GameObject player;
    public GameObject cameramanager;
    public GameObject campos;
    public GameObject blackmask;

    public bool cameraset;
    public float camerasize;

    public void LateUpdate()
    {

        if (cameraset)
        {
            cameramanager.GetComponent<CameraManager>().maincam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = camerasize;
        }
    }

    public void CamaraSet()
    {
        cameramanager.GetComponent<CameraManager>().LookCounsel(campos);
        cameraset = true;
    }

    public void CameraReturn()
    {
        cameramanager.GetComponent<CameraManager>().LookPlayer();
        cameraset = false;
    }

    public void Setblackmask()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        blackmask.SetActive(true);
    }

    public void Disblackmask()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        blackmask.SetActive(false);
    }

    public void CameraShortZoomin()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 5.5f, 0.2f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
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

    public void CameraZoomOut6_5()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 6f, 5f).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraZoomOut7_02()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 7f, 0.2f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CameraZoomOut10_02()
    {
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 10f, 0.2f).SetEase(Ease.OutQuart).SetUpdate(UpdateType.Late).SetId("CameraZoom");
    }

    public void CamVib1()
    {
        cameramanager.GetComponent<CameraManager>().CamVibration1();
    }
}
