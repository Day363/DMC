using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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
    public GameObject playerchatbox;
    public GameObject enemychatbox;
    public GameObject glitch;
    public GameObject fadeout;

    public GameObject curray;

    void Start()
    {
        Time.timeScale = 1f;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1.25f);
        battalemanager.Instance.gameObject.GetComponent<chatmanager>().playerchatbox = playerchatbox;
        battalemanager.Instance.gameObject.GetComponent<chatmanager>().enemychatbox = enemychatbox;
        counselfunctionmanager.OnCutSceneEnd += End;
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
        curray = deathrayobject;
        Instantiate(deathray2, effectpos.transform.position, Quaternion.identity);
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

    public void Chat()
    {
        battalemanager.Instance.gameObject.GetComponent<chatmanager>().CallDialogue(14);
    }

    public void End()
    {
        StartCoroutine(CutScene2End_co());
    }

    IEnumerator CutScene2End_co()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay("cutscene1_swoosh");
        

        List<GameObject> gliths = new List<GameObject> { };

        uimanager.Instance.CloseFightUi();

        yield return new WaitForSecondsRealtime(3.3f);
        Time.timeScale = 0.1f;
        cammanager.GetComponent<CameraManager>().SkilCamvib30();

        soundmanager.instance.SoundPlay("indexer0_weaponland1");
        soundmanager.instance.BGMStop();

        battalemanager.Instance.player_light.GetComponent<Light2D>().color = Color.black;
        battalemanager.Instance.world_globallight.GetComponent<Light2D>().color = Color.red;
        battalemanager.Instance.world_globallight.GetComponent<Light2D>().intensity = 1.5f;
        
        for (int i = 0; i < 30; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-15f, 15f), transform.position.y + Random.Range(-9f, 9f), 0);
            Vector3 scale = new Vector3(1000, Random.Range(-1f, 1f), 0);
            GameObject currentglitch;
            currentglitch = Instantiate(glitch, pos, Quaternion.identity);
            currentglitch.transform.localScale = scale;
            currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(Random.Range(-0.07f, 0.07f), 0));
            gliths.Add(currentglitch);
        }
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.3f);
        fadeout.SetActive(true);
        yield return new WaitForSeconds(5f);
        battalemanager.Instance.gameObject.GetComponent<chatmanager>().chating = false;
        string currentSceneName = SceneManager.GetActiveScene().name;
        battalemanager.Instance.gameObject.GetComponent<chatmanager>().StopChatting();

        battalemanager.Instance.DataSaveTo();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("mirrorselect", LoadSceneMode.Additive);
        yield return loadOperation;

        Scene newScene = SceneManager.GetSceneByName("mirrorselect");
        SceneManager.SetActiveScene(newScene);

        uimanager.Instance.ResetUi();

        yield return SceneManager.UnloadSceneAsync(currentSceneName);
    }
}
