using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class cutsceneanimatormanager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject effectpos;
    public GameObject effect;
    public GameObject slasheffect;
    public PlayableDirector director;
    public GameObject cam;
    public GameObject campos;
    public GameObject cammanager;

    void Start()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1.25f);
        cam.GetComponent<CinemachineVirtualCamera>().Follow = campos.transform;
    }

    public void Effect()
    {
        Instantiate(effect, effectpos.transform.position, Quaternion.identity);
    }

    public void Effect2()
    {
        Instantiate(slasheffect, effectpos.transform.position, Quaternion.identity);
    }

    public void CamVib()
    {
        cammanager.GetComponent<CameraManager>().SkilCamvib();
    }

    public void AfterEffectStart()
    {
        player.GetComponent<afterimagetest>().StartGenerate();
        enemy.GetComponent<afterimagetest>().StartGenerate();
    }

    public void AfterEffectStop()
    {
        player.GetComponent<afterimagetest>().EndGenerate();
        enemy.GetComponent<afterimagetest>().EndGenerate();
    }
}
