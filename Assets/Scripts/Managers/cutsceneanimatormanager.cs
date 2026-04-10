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
    public GameObject spaceslasheffect;
    public GameObject deathray;
    public GameObject deathray2;
    public GameObject deathray3;
    public PlayableDirector director;
    public GameObject cam;
    public GameObject campos;
    public GameObject cammanager;
    public GameObject skillcam;

    void Start()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1.25f);
        
    }

    public void LateUpdate()
    {
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

    public void Effect3()
    {
        Instantiate(spaceslasheffect, effectpos.transform.position, Quaternion.identity);
    }

    public void Effect4()
    {
        GameObject deathrayobject = Instantiate(deathray, new Vector3(effectpos.transform.position.x, effectpos.transform.position.y, -2.5f), Quaternion.identity);
        Instantiate(deathray2, effectpos.transform.position, Quaternion.identity);
        //Instantiate(deathray3, effectpos.transform.position, Quaternion.identity);
        deathrayobject.GetComponent<deathray>().target = enemy;
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
