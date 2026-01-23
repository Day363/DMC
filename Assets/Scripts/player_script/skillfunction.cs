using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class skillfunction : MonoBehaviour
{
    public GameObject globallight;
    public GameObject playerlight;
    public GameObject fadeout;
    public GameObject playerUi;

    public GameObject attackcore;
    public GameObject gamemanger;
    public playerstatus playerStackHandler;
    public GameObject cammanager;
    public GameObject playereffectpos;

    public Dictionary<string, Action> commandMap;
    public Stack Inference;
    public GameObject lazer2;
    public GameObject trapal_weapon1;
    public GameObject trapal_slash1;
    public GameObject trapal_point;
    public GameObject trapal_weapon;
    public GameObject trapal_shockwave;
    public GameObject trapal_lazer1;
    public GameObject trapal_glitch;
    public GameObject trapal_box;
    public GameObject latereffect;
    public Weapon alttriger;
    public GameObject alttrigger_attack2;
    public GameObject cutscene1shoot;
    public GameObject glitch;
    public GameObject cutsceneobject;

    public bool trapal_attack1_recycle;
    public int trapal_attack1_recycle_int;

    void Start()
    {
        gamemanger = battalemanager.Instance.gameObject;

        // 명령어와 함수 매핑
        commandMap = new Dictionary<string, Action>
        {
            { "trapal_slash1", Trapal_Slash1 },
            { "trapal_penetrate", Trapal_Penetrate},
            { "trapal_blow", Trapal_Blow },
            { "trapal_shoot", Trapal_shoot },
            { "Trapal_Add_Inference", Trapal_Add_Inference },
            { "Trapal_shoot", Trapal_shoot },
            { "trapal_defense", Trapal_Defense }
        };
    }

    public void GameOver()
    {
        cammanager.GetComponent<CameraManager>().ShakeCamera(5, 0.02f);
        playerlight.GetComponent<Light2D>().color = Color.black;
        globallight.GetComponent<Light2D>().color = Color.red;
        globallight.GetComponent<Light2D>().intensity = 16.5f;
    }

    public void Walksound()
    {
        if (GetComponent<playerstatus>().groundtype == playerstatus.GroundType.snow)
        {
            int s = UnityEngine.Random.Range(0, 4);
            soundmanager soundmanager = battalemanager.Instance.gameObject.GetComponent<soundmanager>();
            if (s == 0)
            {
                soundmanager.SoundPlay("walk_snow1");
            }
            else if (s == 1)
            {
                soundmanager.SoundPlay("walk_snow2");
            }
            else if (s == 2)
            {
                soundmanager.SoundPlay("walk_snow3");
            }
            else if (s == 3)
            {
                soundmanager.SoundPlay("walk_snow4");
            }
        }
    }

    public void Cutscene1_shoot()
    {
        StartCoroutine(Cutscene1_shoot_co());
        playerUi.SetActive(false);
    }

    IEnumerator Cutscene1_shoot_co()
    {
        battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay("cutscene1_swoosh");
        yield return new WaitForSecondsRealtime(1.5f);

        List<GameObject> gliths = new List<GameObject> { };

        yield return new WaitForSecondsRealtime(1.5f);

        uimanager.Instance.PlayerSceneStatusClose();

        cammanager.GetComponent<CameraManager>().ShakeCamera(30, 0.02f);
        cutsceneobject.GetComponent<Animator>().SetTrigger("cutscene1");

        soundmanager.instance.SoundPlay("cutscene1_error");
        soundmanager.instance.BGMStop();

        Instantiate(cutscene1shoot, playereffectpos.transform.position, Quaternion.Euler(0, 0, -19.38f));
        playerlight.GetComponent<Light2D>().color = Color.black;
        globallight.GetComponent<Light2D>().color = Color.red;
        globallight.GetComponent<Light2D>().intensity = 1.5f;
        Time.timeScale = 0.1f;
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + UnityEngine.Random.Range(-15f, 15f), transform.position.y + UnityEngine.Random.Range(-9f, 9f), 0);
            Vector3 scale = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-15f, 15f), 0);
            Vector3 scale2 = new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-3f, 3f), 0);
            Vector3 scale3 = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f), 0);
            int x = UnityEngine.Random.Range(0, 3);
            GameObject currentglitch;
            if (x == 0)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
            else if (x == 1)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale2;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
            else if (x == 2)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale3;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
        }
        yield return new WaitForSecondsRealtime(1.7f);
        cammanager.GetComponent<CameraManager>().ShakeCamera(50, 0.03f);
        cutsceneobject.GetComponent<Animator>().SetTrigger("cutscene2");
        Instantiate(cutscene1shoot, playereffectpos.transform.position, Quaternion.Euler(0, 0, -19.38f));
        foreach (GameObject glitch in gliths)
        {
            Destroy(glitch);
        }
        for (int i = 0; i < 12; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + UnityEngine.Random.Range(-15f, 15f), transform.position.y + UnityEngine.Random.Range(-9f, 9f), 0);
            Vector3 scale = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-15f, 15f), 0);
            Vector3 scale2 = new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-3f, 3f), 0);
            Vector3 scale3 = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f), 0);
            int x = UnityEngine.Random.Range(0, 3);
            GameObject currentglitch;
            if (x == 0)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
            else if (x == 1)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale2;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
            else if (x == 2)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale3;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
        }
        yield return new WaitForSecondsRealtime(1.7f);
        cammanager.GetComponent<CameraManager>().ShakeCamera(100, 0.05f);
        cutsceneobject.GetComponent<Animator>().SetTrigger("cutscene3");
        Instantiate(cutscene1shoot, playereffectpos.transform.position, Quaternion.Euler(0, 0, -19.38f));
        foreach (GameObject glitch in gliths)
        {
            Destroy(glitch);
        }
        for (int i = 0; i < 15; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + UnityEngine.Random.Range(-15f, 15f), transform.position.y + UnityEngine.Random.Range(-9f, 9f), 0);
            Vector3 scale = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-15f, 15f), 0);
            Vector3 scale2 = new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-3f, 3f), 0);
            Vector3 scale3 = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f), 0);
            int x = UnityEngine.Random.Range(0, 3);
            GameObject currentglitch;
            if (x == 0)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
            else if (x == 1)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale2;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
            else if (x == 2)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale3;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
        }
        yield return new WaitForSecondsRealtime(2.3f);
        soundmanager.instance.SFXStop();
        cammanager.GetComponent<CameraManager>().ShakeCamera(100, 0.06f);
        cutsceneobject.GetComponent<Animator>().SetTrigger("cutscene4");
        Instantiate(cutscene1shoot, playereffectpos.transform.position, Quaternion.Euler(0, 0, -19.38f));
        foreach (GameObject glitch in gliths)
        {
            Destroy(glitch);
        }
        for (int i = 0; i < 25; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + UnityEngine.Random.Range(-15f, 15f), transform.position.y + UnityEngine.Random.Range(-9f, 9f), 0);
            Vector3 scale = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-15f, 15f), 0);
            Vector3 scale2 = new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-3f, 3f), 0);
            Vector3 scale3 = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f), 0);
            int x = UnityEngine.Random.Range(0, 3);
            GameObject currentglitch;
            if (x == 0)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
            else if (x == 1)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale2;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
            else if (x == 2)
            {
                currentglitch = Instantiate(glitch, pos, Quaternion.identity);
                currentglitch.transform.localScale = scale3;
                currentglitch.GetComponent<SpriteRenderer>().material.SetVector("moveto_", new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));
                gliths.Add(currentglitch);
            }
        }
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        fadeout.GetComponent<Image>().DOFade(255, 0).SetUpdate(true);
        yield return new WaitForSecondsRealtime(1f);
        gamemanger.GetComponent<chatmanager>().chating = false;
        string currentSceneName = SceneManager.GetActiveScene().name;

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("mirrorselect", LoadSceneMode.Additive);
        yield return loadOperation;

        Scene newScene = SceneManager.GetSceneByName("mirrorselect");
        SceneManager.SetActiveScene(newScene);

        uimanager.Instance.ResetUi();

        yield return SceneManager.UnloadSceneAsync(currentSceneName);
    }

    public void Alttrigger_attack2()
    {
        playerattackdamage playerplayerattackdamage = alttrigger_attack2.GetComponent<playerattackdamage>();
        List<Magazine> magazines = attackcore.GetComponent<attackcore>().weaponsmagazine;
        Magazine currentmagazine = magazines.Find(m => m.Weapon == alttriger);
        if (currentmagazine.Remaincycle == 0)
        {
            playerplayerattackdamage.damagepercentplus += 1.11f;
        }
    }

    public void Indexer_Call()
    {
        GetComponent<Passivefunction>().Indexer_Call();
    }

    public void Trapal_Slash1()
    {
        StartCoroutine(Trapal_Slash1_co());
    }

    IEnumerator Trapal_Slash1_co()
    {
        GameObject curslash = Instantiate(trapal_slash1, gamemanger.GetComponent<battalemanager>().currentenemy.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f)));
        curslash.GetComponent<playerattackdamage>().player = gameObject;
        curslash.GetComponent<playerattackdamage>().damagenum = 1.1f;
        playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
        if (instance.currentStack >= 4)
        {
            int runs = instance.currentStack / 4;
            for (int i = 0; i < runs; i++)
            {
                yield return new WaitForSeconds(0.06f);
                Vector3 randomposition = new Vector3(gamemanger.GetComponent<battalemanager>().currentenemy.transform.position.x + UnityEngine.Random.Range(-3.5f, 3.5f), gamemanger.GetComponent<battalemanager>().currentenemy.transform.position.y + UnityEngine.Random.Range(-3.5f, 3.5f), 0);
                GameObject curslash1 = Instantiate(trapal_slash1, randomposition, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f)));
                curslash1.GetComponent<playerattackdamage>().player = gameObject;
                curslash1.GetComponent<playerattackdamage>().damagenum = 1.1f;
            }
            
        }
        else
        {
            curslash.transform.localScale = new Vector3(2f, 2f, 1);
        }
    }

    public void Trapal_Penetrate()
    {
        playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
        playerstatus.StackInstance instance2 = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "확신");

        if (instance2 == null)
        {
            if (instance == null)
            {

            }
            else if (instance.currentStack >= 6)
            {
                int runs = instance.currentStack / 6;
                runs = Mathf.Clamp(runs, 0, trapal_point.GetComponent<player_trapal_weapon_arrey>().count);
                for (int i = 0; i < runs; i++)
                {
                    if (trapal_point.transform.childCount < trapal_point.GetComponent<player_trapal_weapon_arrey>().count)
                    {
                        GameObject curpen = Instantiate(trapal_weapon, trapal_point.transform);
                        curpen.GetComponent<playerattackdamage>().canattack = false;
                        curpen.GetComponent<playerattackdamage>().player = gameObject;
                        trapal_point.GetComponent<player_trapal_weapon_arrey>().ArrangeExistingChildren();
                    }

                }
            }
        }
        else if (instance2 != null)
        {
            if (instance == null)
            {

            }
            else if (instance.currentStack >= 3)
            {
                int runs = instance.currentStack / 3;
                runs = Mathf.Clamp(runs, 0, trapal_point.GetComponent<player_trapal_weapon_arrey>().count);
                for (int i = 0; i < runs; i++)
                {
                    if (trapal_point.transform.childCount < trapal_point.GetComponent<player_trapal_weapon_arrey>().count)
                    {
                        GameObject curpen = Instantiate(trapal_weapon, trapal_point.transform);
                        curpen.GetComponent<playerattackdamage>().canattack = false;
                        trapal_point.GetComponent<player_trapal_weapon_arrey>().ArrangeExistingChildren();
                    }

                }
            }
        }
        
        
        if (instance2 != null)
        {
            trapal_point.GetComponent<player_trapal_weapon_arrey>().count = 12;
        }
        else
        {
            trapal_point.GetComponent<player_trapal_weapon_arrey>().count = 3;
        }
        
    }

    public void Trapal_Penetrate_When_attacked()
    {
        if (trapal_point.transform.childCount != 0)
        {
            StartCoroutine(Trapal_Penetrate_When_attacked_co());
        }
        
    }

    IEnumerator Trapal_Penetrate_When_attacked_co()
    {
        
        int index = trapal_point.transform.childCount - 1;
        Transform curpenetrate = trapal_point.transform.GetChild(index);
        curpenetrate.SetParent(null);
        Vector3 direction = (gamemanger.GetComponent<battalemanager>().currentenemy.transform.position - curpenetrate.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        curpenetrate.DORotate(new Vector3(0, 0, targetAngle), 1f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(1.2f);
        float opposangle = curpenetrate.eulerAngles.z + 180f; //일단 각도에 180을 더함
        float rad = opposangle * Mathf.Deg2Rad;//라디안으로 변환
        Vector2 direction1 = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * 4.5f;//거따가 5씩 곱해서 그뭐냐 그 원
        Vector2 spawnPos = (Vector2)curpenetrate.position + direction1; //그걸이제 위치에 더해서 어따소환할지 정함 어잠만
        GameObject curlazer2 = Instantiate(lazer2, spawnPos, Quaternion.identity);
        curlazer2.transform.position = new Vector3(curlazer2.transform.position.x, curlazer2.transform.position.y, -6.5f);
        player_trapal_lazer2 curlazer2_player_Trapal_Lazer2 = curlazer2.GetComponent<player_trapal_lazer2>();
        curlazer2_player_Trapal_Lazer2.target = curpenetrate.gameObject;
        curlazer2_player_Trapal_Lazer2.look = true;
        curlazer2_player_Trapal_Lazer2.startcharge = false;
        curlazer2_player_Trapal_Lazer2.cammanager = cammanager;
        curlazer2_player_Trapal_Lazer2.ShootNotDes();
        playerattackdamage curpenetrate_playerattackdamage = curpenetrate.GetComponent<playerattackdamage>();
        curpenetrate_playerattackdamage.canattack = true;//여기에 버그있음
        curpenetrate_playerattackdamage.player = gameObject;
        curpenetrate_playerattackdamage.damagenum = 3.0f;
        curpenetrate.GetComponent<Rigidbody2D>().AddForce(90f * direction, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        curlazer2.GetComponent<player_trapal_lazer2>().look = false;
        playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "확신");
        if (instance != null)
        {
            yield return new WaitForSeconds(0.4f);
            curlazer2.GetComponent<player_trapal_lazer2>().target = null;
            curlazer2.GetComponent<player_trapal_lazer2>().autoangle = targetAngle;
            curlazer2.GetComponent<player_trapal_lazer2>().player = gameObject;
            curlazer2.GetComponent<player_trapal_lazer2>().damagenum = 2.7f;
            curlazer2.GetComponent<player_trapal_lazer2>().Shoot();
            
        }
        else
        {
            curlazer2.GetComponent<player_trapal_lazer2>().SelfDesM();
        }
    }

    public void Trapal_Blow()
    {
        StartCoroutine(Trapal_Blow_co());
    }

    IEnumerator Trapal_Blow_co()
    {
        GameObject curshock = Instantiate(trapal_shockwave, playereffectpos.transform);
        curshock.transform.localPosition = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.55f);
        transform.position = new Vector3(transform.position.x, -0.35f, transform.position.z);
        GameObject curlazer1 = Instantiate(trapal_lazer1, transform.position, Quaternion.identity);
        curlazer1.GetComponent<playerattackdamage>().player = gameObject;
        curlazer1.GetComponent<playerattackdamage>().damagenum = 2.5f;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);
        Destroy(curshock);
        curlazer1.transform.DOScaleX(0, 1f).SetEase(Ease.OutQuart);
    }

    public void Trapal_shoot()
    {
        StartCoroutine(Trapal_shoot_co());
    }

    IEnumerator Trapal_shoot_co()
    {
        List<GameObject> lazer2s = new List<GameObject> { };
        GameObject curstartlazer2;
        yield return new WaitForSeconds(0.7f);
        if (GetComponent<PlayerMove>().dir == 1)
        {
            curstartlazer2 = Instantiate(lazer2, new Vector3(transform.position.x + 1.73f, transform.position.y + 0.36f, -6.5f), Quaternion.Euler(0, 70, 0));
        }
        else
        {
            curstartlazer2 = Instantiate(lazer2, new Vector3(transform.position.x - 1.73f, transform.position.y + 0.36f, -6.5f), Quaternion.Euler(0, 70, 0));
            curstartlazer2.transform.localScale = new Vector3(0.5f, 0.5f, -0.5f);
            
        }
        player_trapal_lazer2 curstartlazer2_player_Trapal_Lazer2 = curstartlazer2.GetComponent<player_trapal_lazer2>();
        curstartlazer2_player_Trapal_Lazer2.player = gameObject;
        curstartlazer2_player_Trapal_Lazer2.damagenum = 3.7f;
        curstartlazer2_player_Trapal_Lazer2.target = gamemanger.GetComponent<battalemanager>().currentenemy;
        curstartlazer2_player_Trapal_Lazer2.look = false;
        curstartlazer2_player_Trapal_Lazer2.cammanager = cammanager;
        yield return new WaitForSeconds(0.5f);
        curstartlazer2_player_Trapal_Lazer2.ShootNotDes();
        playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
        if (instance != null)
        {
            if (instance.currentStack >= 3)
            {
                int runs = instance.currentStack / 3;
                for (int i = 0; i < runs; i++)
                {
                    if (GetComponent<PlayerMove>().dir == 1)
                    {
                        yield return new WaitForSeconds(0.25f);
                        GameObject curlazer2 = Instantiate(lazer2, new Vector3(transform.position.x - UnityEngine.Random.Range(3f, 7f), transform.position.y + UnityEngine.Random.Range(-6f, 6f), -6.5f), Quaternion.identity);
                        lazer2s.Add(curlazer2);
                        player_trapal_lazer2 curlazer2_player_Trapal_Lazer2 = curlazer2.GetComponent<player_trapal_lazer2>();
                        curlazer2_player_Trapal_Lazer2.player = gameObject;
                        curlazer2_player_Trapal_Lazer2.damagenum = 3.7f;
                        curlazer2_player_Trapal_Lazer2.cammanager = cammanager;
                        curlazer2_player_Trapal_Lazer2.target = gamemanger.GetComponent<battalemanager>().currentenemy;
                        curlazer2_player_Trapal_Lazer2.look = true;

                    }
                    else
                    {
                        yield return new WaitForSeconds(0.25f);
                        GameObject curlazer2 = Instantiate(lazer2, new Vector3(transform.position.x + UnityEngine.Random.Range(3f, 7f), transform.position.y + UnityEngine.Random.Range(-6f, 6f), -6.5f), Quaternion.identity);
                        lazer2s.Add(curlazer2);
                        player_trapal_lazer2 curlazer2_player_Trapal_Lazer2 = curlazer2.GetComponent<player_trapal_lazer2>();
                        curlazer2_player_Trapal_Lazer2.player = gameObject;
                        curlazer2_player_Trapal_Lazer2.damagenum = 3.7f;
                        curlazer2_player_Trapal_Lazer2.cammanager = cammanager;
                        curlazer2_player_Trapal_Lazer2.target = gamemanger.GetComponent<battalemanager>().currentenemy;
                        curlazer2_player_Trapal_Lazer2.look = true;
                    }

                }
                yield return new WaitForSeconds(1.3f);
                curstartlazer2_player_Trapal_Lazer2.Shoot();
            }
            else if (instance.currentStack <= 2)
            {
                yield return new WaitForSeconds(0.8f);
                curstartlazer2_player_Trapal_Lazer2.Shoot();
            }


        }
        else
        {
            yield return new WaitForSeconds(1f);
            curstartlazer2_player_Trapal_Lazer2.Shoot();
        }

    }

    public void Trapal_Weapon1_New_Recycle()
    {
        playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
        if (instance != null)
        {
            if (trapal_attack1_recycle && instance.currentStack >= 3)
            {
                trapal_attack1_recycle_int++;
                if (trapal_attack1_recycle_int < 4)
                {
                    trapal_attack1_recycle = false;
                    GetComponent<Animator>().SetTrigger("trapal_attack1");
                }
                if (trapal_attack1_recycle_int >= 4)
                {
                    trapal_attack1_recycle = false;
                    GetComponent<Animator>().SetTrigger("trapal_attack1_five");
                    trapal_attack1_recycle_int = 0;
                }
            }
            else
            {
                trapal_attack1_recycle_int = 0;
            }
        }
        
    }

    public void Trapal_Weapon1_Lazer2()
    {
        StartCoroutine(Trapal_Weapon1_Lazer2_co());
    }

    IEnumerator Trapal_Weapon1_Lazer2_co()
    {
        if (GetComponent<PlayerMove>().dir == 1)
        {
            GameObject curlazer2 = Instantiate(lazer2, new Vector3(transform.position.x - 9f, transform.position.y, -6.5f), Quaternion.Euler(0, 108, 0));
            player_trapal_lazer2 curlazer2_player_Trapal_Lazer2 = curlazer2.GetComponent<player_trapal_lazer2>();
            curlazer2_player_Trapal_Lazer2.look = false;
            curlazer2_player_Trapal_Lazer2.cammanager = cammanager;

            yield return new WaitForSeconds(0.4f);

            curlazer2_player_Trapal_Lazer2.ShootNotDes();
            curlazer2_player_Trapal_Lazer2.SelfDesM();
        }
        else
        {
            GameObject curlazer2 = Instantiate(lazer2, new Vector3(transform.position.x + 9f, transform.position.y, -6.5f), Quaternion.Euler(0, -108, 0));
            player_trapal_lazer2 curlazer2_player_Trapal_Lazer2 = curlazer2.GetComponent<player_trapal_lazer2>();
            curlazer2_player_Trapal_Lazer2.look = false;
            curlazer2_player_Trapal_Lazer2.cammanager = cammanager;

            yield return new WaitForSeconds(0.4f);

            curlazer2_player_Trapal_Lazer2.ShootNotDes();
            curlazer2_player_Trapal_Lazer2.SelfDesM();
        }
    }

    public void Trapal_Weapon1_Shoot()
    {
        if (GetComponent<PlayerMove>().dir == 1)
        {
            GameObject curweapon = Instantiate(trapal_weapon1, new Vector3(transform.position.x - 4f, transform.position.y - 0.2f, 0), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            GameObject curweapon = Instantiate(trapal_weapon1, new Vector3(transform.position.x + 4f, transform.position.y - 0.2f, 0), Quaternion.Euler(0, 0, 180));
        }
    }

    public void Trapal_Defense()
    {
        playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
        if (instance != null)
        {
            if (instance.currentStack >= 6)
            {
                GetComponent<playerstatus>().RemoveStack(Inference, 2);
            }
            else if (instance.currentStack <= 18)
            {
                GetComponent<playerstatus>().ApplyStack(Inference, 2);
            }
        }
    }

    public void Trapal_Add_Inference()
    {
        GetComponent<playerstatus>().ApplyStack(Inference, 3);
        GetComponent<playerstatus>().PrintStacks();
    }

    public void Trapal_Attack3_Halo()
    {
        StartCoroutine(Trapal_Attack3_Halo_co());
    }

    IEnumerator Trapal_Attack3_Halo_co()
    {
        GameObject curstartlazer2;
        if (GetComponent<PlayerMove>().dir == 1)
        {
            curstartlazer2 = Instantiate(lazer2, new Vector3(transform.position.x + 1.73f, transform.position.y + 0.36f, -6.5f), Quaternion.Euler(0, 70, 0));
        }
        else
        {
            curstartlazer2 = Instantiate(lazer2, new Vector3(transform.position.x - 1.73f, transform.position.y + 0.36f, -6.5f), Quaternion.Euler(0, 70, 0));
            curstartlazer2.transform.localScale = new Vector3(0.5f, 0.5f, -0.5f);
        }
        player_trapal_lazer2 curstartlazer2_player_Trapal_Lazer2 = curstartlazer2.GetComponent<player_trapal_lazer2>();
        curstartlazer2_player_Trapal_Lazer2.damagenum = 8.8f;
        curstartlazer2_player_Trapal_Lazer2.target = gamemanger.GetComponent<battalemanager>().currentenemy;
        curstartlazer2_player_Trapal_Lazer2.look = false;
        curstartlazer2_player_Trapal_Lazer2.cammanager = cammanager;
        yield return new WaitForSeconds(0.7f);
        curstartlazer2_player_Trapal_Lazer2.ShootNotDes();
        yield return new WaitForSeconds(2.2f);
        curstartlazer2_player_Trapal_Lazer2.Shoot();
    }

    public void Trapal_Attack3_round()
    {
        StartCoroutine(Trapal_Attack3_round_co());
    }

    IEnumerator Trapal_Attack3_round_co()
    {
        playerstatus.StackInstance instance = playerStackHandler.activeStacks.Find(s => s.stackData.effectName == "추론");
        if (instance != null)
        {

            int currentangle = 0;
            for (int i = 0; i < instance.currentStack; i++)
            {
                yield return new WaitForSeconds(0.03f);
                float radian = currentangle * Mathf.Deg2Rad;
                float spawnX = gamemanger.GetComponent<battalemanager>().currentenemy.transform.position.x + Mathf.Cos(radian) * 4f;
                float spawnY = gamemanger.GetComponent<battalemanager>().currentenemy.transform.position.y + Mathf.Sin(radian) * 4f;
                Vector2 spawnPosition = new Vector2(gamemanger.GetComponent<battalemanager>().currentenemy.transform.position.x + spawnX, gamemanger.GetComponent<battalemanager>().currentenemy.transform.position.y + spawnY);
                Vector3 pos = new Vector3(spawnPosition.x, spawnPosition.y, -1.5f);
                GameObject curlazer2 = Instantiate(lazer2, pos, Quaternion.identity);
                player_trapal_lazer2 curlazer2_player_Trapal_Lazer2 = curlazer2.GetComponent<player_trapal_lazer2>();
                curlazer2_player_Trapal_Lazer2.damagenum = 3.5f;
                curlazer2_player_Trapal_Lazer2.look = true;
                curlazer2_player_Trapal_Lazer2.cammanager = cammanager;
                curlazer2_player_Trapal_Lazer2.target = gamemanger.GetComponent<battalemanager>().currentenemy;
                

                currentangle = currentangle - 15;
            }
        }
    }

    public void Trapal_Attack3_Rainattack()
    {
        StartCoroutine(Trapal_Attack3_Rainattack_co());
    }

    IEnumerator Trapal_Attack3_Rainattack_co()
    {
        GameObject curlazer1 = Instantiate(trapal_lazer1, gamemanger.GetComponent<battalemanager>().currentenemy.transform.position, Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(0.1f);
        curlazer1.transform.DOScaleX(0, 1f).SetEase(Ease.OutQuart);
    }

    public void Trapal_Attack3_Effect()
    {
        StartCoroutine(Trapal_Attack3_Effect_co());
    }

    IEnumerator Trapal_Attack3_Effect_co()
    {
        GameObject curshock = Instantiate(trapal_shockwave, playereffectpos.transform);
        curshock.transform.localPosition = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(curshock);
    }

    public void Trapal_Attack4_Effect_Hand()
    {
        StartCoroutine(Trapal_Attack4_Effect_Hand_co());
    }

    IEnumerator Trapal_Attack4_Effect_Hand_co()
    {
        GameObject cureffect = Instantiate(trapal_shockwave, playereffectpos.transform);
        cureffect.transform.localPosition = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(cureffect);
    }

    public void Trapal_Attack4_Effect()
    {
        if (latereffect != null)
        {
            Destroy(latereffect);
        }
        GameObject cureffect = Instantiate(trapal_shockwave, playereffectpos.transform.position, Quaternion.identity);
        cureffect.transform.position = playereffectpos.transform.position;
        cureffect.transform.localScale = new Vector3(3f, 3f, 1f);
        cureffect.GetComponent<SpriteRenderer>().material.SetFloat("_strength", -5);
        var mat = cureffect.GetComponent<SpriteRenderer>().material;
        DOTween.To(() => mat.GetFloat("_strength"), x => mat.SetFloat("_strength", x), 0f, 0.5f).SetEase(Ease.OutQuart);
        latereffect = cureffect;
    }

    public void Trapal_Attack4_Glitch()
    {
        int count = UnityEngine.Random.Range(7, 15);
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(Trapal_Attack4_Glitch_co());
        }
    }

    IEnumerator Trapal_Attack4_Glitch_co()
    {
   
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.5f));
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + UnityEngine.Random.Range(-10f, 10f), 0);
        GameObject curglitch = Instantiate(trapal_glitch, pos, Quaternion.identity);
        curglitch.transform.localScale = new Vector3(300f, UnityEngine.Random.Range(0.12f, 0.6f), 1);
        curglitch.GetComponent<SpriteRenderer>().material.SetVector("_moveto", new Vector2(UnityEngine.Random.Range(-0.05f, 0.05f), 0));
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 2f));
        Destroy(curglitch);
        
    }

    public void Trapal_Attack4_box()
    {
        int count = UnityEngine.Random.Range(3, 7);
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(Trapal_Attack4_box_co());
        }
    }

    IEnumerator Trapal_Attack4_box_co()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.5f));
        Vector3 pos = new Vector3(transform.position.x + UnityEngine.Random.Range(-15f, 15f), transform.position.y + UnityEngine.Random.Range(-10f, 10f), 0);
        GameObject curbox = Instantiate(trapal_box, pos, Quaternion.identity);
        curbox.transform.localScale = new Vector3(UnityEngine.Random.Range(0.5f, 5f), UnityEngine.Random.Range(0.5f, 5f), 1);
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 2f));
        Destroy(curbox);
    }


    public void ExecuteCommand(List<string> commands)
    {
        foreach (string command in commands)
        {
            if (commandMap.TryGetValue(command, out Action action))
            {
                action.Invoke();
            }
            else
            {
                Debug.LogWarning($"Unknown command: {command}");
            }
        }
        
    }
}
