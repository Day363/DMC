using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class trapal_passive : MonoBehaviour
{
    public static Action OnCertainEnd;

    public GameObject light1;
    public GameObject light2;

    public GameObject attackcore;
    public GameObject gamemanager;
    public GameObject cammanager;
    public GameObject denyhandy;
    public GameObject tearlazer;
    public GameObject trapal_point;
    public GameObject player;
    public GameObject lazer2;
    public GameObject lazer2_small;
    public GameObject camtarget;
    public GameObject Cammanager;
    public GameObject denydistorsion;
    public int denycount;
    public int certaincount;
    public Stack deny;
    public Stack certain;
    public GameObject denyeye;
    public GameObject mask;
    public GameObject glitch;
    public GameObject certaindeny;
    public GameObject diffusion_fragment;
    public GameObject convergence_fragment;
    public GameObject eye2;

    public boss_hpbar BossstackHander;

    public bool whiledeny;
    public bool whilecertain;
    public bool canApplystack = true;
    public bool whilecertain24 = false;

    public int deny24count;
    public int certain24count;

    public int certaindestroy;

    public trapal_script ts;
    public trapal_counsel tc;

    private void OnEnable()
    {
        boss_hpbar.OnHitCalled += Deny;
        playerhit.OnHitCalled += Onhit;
        trapal_lazer1.OnLazerHitCalled += Lazer1_Hit;
        trapal_certain_eye.OnDie += Certain24plus;

        GetComponent<boss_hpbar>().ApplyStack(deny, 1);
        GetComponent<boss_hpbar>().ApplyStack(certain, 1);

        GameObject fragment1 = Instantiate(diffusion_fragment);
        fragment1.GetComponent<fragment_script>().trapal = gameObject;
        normal_enemy_hp fragment1normal_enemy_hp = fragment1.GetComponent<normal_enemy_hp>();
        fragment1normal_enemy_hp.gammanager = gamemanager;
        fragment1normal_enemy_hp.cammanager = cammanager;
        fragment1normal_enemy_hp.attackcore = attackcore;
        fragment1.transform.position = new Vector3(-6f, -1.9f, 0);
        GameObject fragment2 = Instantiate(convergence_fragment);
        fragment2.GetComponent<fragment_script>().trapal = gameObject;
        normal_enemy_hp fragment2normal_enemy_hp = fragment2.GetComponent<normal_enemy_hp>();
        fragment2normal_enemy_hp.gammanager = gamemanager;
        fragment2normal_enemy_hp.cammanager = cammanager;
        fragment2normal_enemy_hp.attackcore = attackcore;
        fragment2.transform.position = new Vector3(6f, -1.9f, 0);

        
    }

    public void Start()
    {
        ts = GetComponent<trapal_script>();
        tc = GetComponent<trapal_counsel>();
    }

    public void FixedUpdate()
    {
        boss_hpbar.StackInstance DenyInstance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "부정");
        boss_hpbar.StackInstance CertainInstance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "확신");
        
        if (!ts.phase2)
        {
            if (DenyInstance != null)
            {
                if (DenyInstance.currentStack >= CertainInstance.currentStack && DenyInstance.currentStack >= 12)
                {
                    GetComponent<Animator>().SetBool("deny", true);
                    whiledeny = true;
                    trapal_point.GetComponent<trapal_weapon_point>().count = 12;
                }
                else 
                {
                    GetComponent<Animator>().SetBool("deny", false);
                    whiledeny = false;
                    trapal_point.GetComponent<trapal_weapon_point>().count = 3;
                }

                if (DenyInstance.currentStack >= 24)
                {
                    GetComponent<trapal_script>().canattack = false;
                    GetComponent<boss_hpbar>().RemoveStack(deny, 24);
                    GetComponent<Animator>().SetBool("idle", false);
                    GetComponent<Animator>().SetTrigger("deny24");
                    canApplystack = false;
                    deny24count++;

                }
            }
            else
            {
                GetComponent<Animator>().SetBool("deny", false);
                whiledeny = false;
                trapal_point.GetComponent<trapal_weapon_point>().count = 3;
            }

            if (CertainInstance != null)
            {
                if (CertainInstance.currentStack >= DenyInstance.currentStack && CertainInstance.currentStack >= 12)
                {
                    GetComponent<Animator>().SetBool("certain", true);
                    whilecertain = true;
                }
                else
                {
                    GetComponent<Animator>().SetBool("certain", false);
                    whilecertain = false;
                }

                if (CertainInstance.currentStack >= 24)
                {
                    GetComponent<boss_hpbar>().RemoveStack(certain, 24);
                    canApplystack = false;
                    StartCoroutine(Certain24());
                    certain24count++;
                }
            }
            else
            {
                GetComponent<Animator>().SetBool("certain", false);
                whilecertain = false;
            }
        }
    }

    public void Fragment()
    {
        StartCoroutine(Fragment_co());
        tc.AttackEnd();
    }

    IEnumerator Fragment_co()
    {
        boss_hpbar.StackInstance DenyInstance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "부정");
        boss_hpbar.StackInstance CertainInstance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "확신");

        GameObject currentfragment;

        yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 10f));
        if (DenyInstance != null && CertainInstance != null)
        {
            if (DenyInstance.currentStack > CertainInstance.currentStack)
            {
                currentfragment = Instantiate(diffusion_fragment);
                currentfragment.GetComponent<fragment_script>().trapal = gameObject;
                normal_enemy_hp currentfragmentnormal_enemy_hp = currentfragment.GetComponent<normal_enemy_hp>();
                currentfragmentnormal_enemy_hp.gammanager = gamemanager;
                currentfragmentnormal_enemy_hp.cammanager = cammanager;
                currentfragmentnormal_enemy_hp.attackcore = attackcore;
                currentfragment.transform.position = new Vector3(UnityEngine.Random.Range(-25f, 25f), -1.9f, 0);
            }
            else if (DenyInstance.currentStack < CertainInstance.currentStack)
            {
                currentfragment = Instantiate(convergence_fragment);
                currentfragment.GetComponent<fragment_script>().trapal = gameObject;
                normal_enemy_hp currentfragmentnormal_enemy_hp = currentfragment.GetComponent<normal_enemy_hp>();
                currentfragmentnormal_enemy_hp.gammanager = gamemanager;
                currentfragmentnormal_enemy_hp.cammanager = cammanager;
                currentfragmentnormal_enemy_hp.attackcore = attackcore;
                currentfragment.transform.position = new Vector3(UnityEngine.Random.Range(-25f, 25f), -1.9f, 0);
            }
        }
        else if (DenyInstance != null && CertainInstance == null)
        {
            currentfragment = Instantiate(diffusion_fragment);
            currentfragment.GetComponent<fragment_script>().trapal = gameObject;
            normal_enemy_hp currentfragmentnormal_enemy_hp = currentfragment.GetComponent<normal_enemy_hp>();
            currentfragmentnormal_enemy_hp.gammanager = gamemanager;
            currentfragmentnormal_enemy_hp.cammanager = cammanager;
            currentfragmentnormal_enemy_hp.attackcore = attackcore;
            currentfragment.transform.position = new Vector3(UnityEngine.Random.Range(-25f, 25f), -1.9f, 0);
        }
        else if (DenyInstance == null && CertainInstance != null)
        {
            currentfragment = Instantiate(convergence_fragment);
            currentfragment.GetComponent<fragment_script>().trapal = gameObject;
            normal_enemy_hp currentfragmentnormal_enemy_hp = currentfragment.GetComponent<normal_enemy_hp>();
            currentfragmentnormal_enemy_hp.gammanager = gamemanager;
            currentfragmentnormal_enemy_hp.cammanager = cammanager;
            currentfragmentnormal_enemy_hp.attackcore = attackcore;
            currentfragment.transform.position = new Vector3(UnityEngine.Random.Range(-25f, 25f), -1.9f, 0);
        }
        else if (DenyInstance == null && CertainInstance == null)
        {
            int i = UnityEngine.Random.Range(0, 2);
            if (i == 0)
            {
                currentfragment = Instantiate(diffusion_fragment);
                currentfragment.GetComponent<fragment_script>().trapal = gameObject;
                normal_enemy_hp currentfragmentnormal_enemy_hp = currentfragment.GetComponent<normal_enemy_hp>();
                currentfragmentnormal_enemy_hp.gammanager = gamemanager;
                currentfragmentnormal_enemy_hp.cammanager = cammanager;
                currentfragmentnormal_enemy_hp.attackcore = attackcore;
                currentfragment.transform.position = new Vector3(UnityEngine.Random.Range(-25f, 25f), -1.9f, 0);
            }
            else
            {
                currentfragment = Instantiate(convergence_fragment);
                currentfragment.GetComponent<fragment_script>().trapal = gameObject;
                normal_enemy_hp currentfragmentnormal_enemy_hp = currentfragment.GetComponent<normal_enemy_hp>();
                currentfragmentnormal_enemy_hp.gammanager = gamemanager;
                currentfragmentnormal_enemy_hp.cammanager = cammanager;
                currentfragmentnormal_enemy_hp.attackcore = attackcore;
                currentfragment.transform.position = new Vector3(UnityEngine.Random.Range(-25f, 25f), -1.9f, 0);
            }
        }
    }

    public void Onhit()
    {
        Certain();
        WhenHitplayer();
    }

    public void Deny()
    {
        if (canApplystack)
        {
            denycount++;
            if (denycount >= 3)
            {
                denycount = 0;
                GetComponent<boss_hpbar>().ApplyStack(deny, 1);
            }
        }
        
    }

    public void Certain()
    {
        if (canApplystack)
        {
            certaincount++;
            if (certaincount >= 3)
            {
                certaincount = 0;
                GetComponent<boss_hpbar>().ApplyStack(certain, 1);
            }
        }
        
    }

    public void SmallCamIn()
    {
        Cammanager.GetComponent<CameraManager>().Looksmallpoint(camtarget);
        Cammanager.GetComponent<CameraManager>().CamStable();
    }

    public void SmallcamoutToPlayerCamDelay_()
    {
        StartCoroutine(PlayerCam());
    }

    IEnumerator PlayerCam()
    {
        yield return new WaitForSeconds(1f);
        Cammanager.GetComponent<CameraManager>().LookPlayer();
        Cammanager.GetComponent<CameraManager>().CamStable();
        yield return new WaitForSeconds(2.5f);
        SpawnDeny();
    }

    public void SpawnDeny()
    {
        GameObject currentdeny = Instantiate(denydistorsion, camtarget.transform.position, Quaternion.identity);
        currentdeny.transform.localScale = new Vector3(0, 0, 1);
        currentdeny.transform.DOScale(new Vector3(3, 3, 1), 0.5f).SetEase(Ease.OutQuart);
        GameObject currenteye = Instantiate(denyeye, camtarget.transform.position, Quaternion.identity);
        currenteye.GetComponent<trapal_deny_eye>().player = player.transform;
        currenteye.GetComponent<trapal_deny_eye>().centerObject = currentdeny.transform;
        StartCoroutine(Lazer2(currentdeny, currenteye));
    }

    IEnumerator Lazer2(GameObject deny, GameObject eye)
    {
        yield return new WaitForSeconds(0.6f);
        Cammanager.GetComponent<CameraManager>().LookBigCam();
        Cammanager.GetComponent<CameraManager>().CamStable();
        GetComponent<trapal_script>().lazer2time = true;
        GetComponent<Animator>().SetBool("idle", true);
        yield return new WaitForSeconds(19f);
        canApplystack = true;
        Cammanager.GetComponent<CameraManager>().LookPlayer();
        deny.transform.DOScale(new Vector3(0, 0, 1), 0.8f).SetEase(Ease.InQuart);
        Destroy(eye);
        GetComponent<trapal_script>().canattack = true;
        yield return new WaitForSeconds(0.9f);
        Destroy(deny);
        tc.AttackEnd();
    }

    IEnumerator Glitch()
    {
        
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.5f));
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + UnityEngine.Random.Range(-14f, 14f), 0);
        GameObject curglitch = Instantiate(glitch, pos, Quaternion.identity);
        curglitch.transform.localScale = new Vector3(300f, UnityEngine.Random.Range(0.12f, 0.6f), 1);
        curglitch.GetComponent<SpriteRenderer>().material.SetVector("_moveto", new Vector2(UnityEngine.Random.Range(-0.05f, 0.05f), 0));
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 1f));
        Destroy(curglitch);
        
    }


    IEnumerator Certain24()
    {
        whilecertain24 = true;

        GetComponent<boss_hpbar>().canhit = false;

        certaindestroy = 0;

        GetComponent<trapal_script>().canattack = false;

        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(1);

            for (int x = 0; x < UnityEngine.Random.Range(7, 15); x++)
            {

                StartCoroutine(Glitch());
            }

            if (i == 4)
            {
                GameObject currentdeny = Instantiate(denydistorsion, transform.position, Quaternion.identity);
                currentdeny.transform.DOScale(new Vector3(125, 125, 1), 3.5f).SetEase(Ease.OutQuart);
                certaindeny = currentdeny;
            }
        }
        Destroy(certaindeny);
        light1.SetActive(false);
        light2.SetActive(false);
        GameObject currentmask = Instantiate(mask, transform.position, Quaternion.identity);
        currentmask.transform.localScale = new Vector3(200, 50, 1);

        GameObject currenteye = Instantiate(eye2, transform.position, Quaternion.identity);
        currenteye.transform.localScale = Vector3.zero;
        float scale = UnityEngine.Random.Range(0.5f, 1.5f);
        currenteye.transform.DOScale(new Vector3(scale, scale, 1), 0.5f);
        trapal_certain_eye currenteyetrapal_certain_eye = currenteye.GetComponent<trapal_certain_eye>();
        currenteyetrapal_certain_eye.trapal = gameObject;
        currenteyetrapal_certain_eye.tp = this;
        currenteyetrapal_certain_eye.player = player;
        currenteyetrapal_certain_eye.gamemanager = gamemanager;
        currenteyetrapal_certain_eye.cammanager = cammanager;
        currenteyetrapal_certain_eye.attackcore = attackcore;
        currenteye.transform.GetChild(0).GetComponent<trapal_deny_eye>().centerObject = currenteye.transform.GetChild(1);
        currenteye.transform.GetChild(0).GetComponent<trapal_deny_eye>().player = player.transform;
        normal_enemy_hp currenteyenormal_enemy_hp = currenteye.GetComponent<normal_enemy_hp>();
        currenteyenormal_enemy_hp.gammanager = gamemanager;
        currenteyenormal_enemy_hp.cammanager = cammanager;
        currenteyenormal_enemy_hp.attackcore = attackcore;

        yield return new WaitForSeconds(15f);
        light1.SetActive(true);
        light2.SetActive(true);
        GetComponent<trapal_script>().canattack = true;
        canApplystack = true;
        Destroy(currentmask);
        GetComponent<boss_hpbar>().canhit = true;
        WhenCertain24End();
    }

    public void Certain24plus()
    {
        certaindestroy++;
    }

    public void WhenCertain24End()
    {
        OnCertainEnd?.Invoke();
        whilecertain24 = false;

        if (certaindestroy >= 12)
        {
            StartCoroutine(BosshpbarBalanceCollapse());
        }
        else
        {
            StartCoroutine(PlayerstatusBalanceCollapse());
        }
        tc.AttackEnd();
    }

    IEnumerator BosshpbarBalanceCollapse()
    {
        yield return new WaitForSeconds(0.17f);
        GetComponent<boss_hpbar>().BalanceCollapse();
    }

    IEnumerator PlayerstatusBalanceCollapse()
    {
        yield return new WaitForSeconds(0.17f);
        player.GetComponent<playerstatus>().BalanceCollapse();
    }

    public void Lazer1_Hit(Vector2 direction)
    {
        if (whiledeny)
        {
            StartCoroutine(Lazer1_Hit_co());
        }
        else if (whilecertain)
        {
            StartCoroutine(Lazer1_Hit_co2(direction));
        }
       
    }

    IEnumerator Lazer1_Hit_co()
    {
        int i = 0;
        while (i < UnityEngine.Random.Range(7, 15))
        {
            i++;
            yield return new WaitForSeconds(0.06f);
            Vector3 spawnPosition = new Vector3(player.transform.position.x + UnityEngine.Random.Range(-1, 1), 8, -6.5f);
            GameObject curlazer2 = Instantiate(lazer2_small, spawnPosition, Quaternion.identity);
            curlazer2.transform.position = new Vector3(curlazer2.transform.position.x, curlazer2.transform.position.y, -6.5f);
            curlazer2.GetComponent<lazer2lookat>().player = player;
            curlazer2.GetComponent<lazer2lookat>().look = true;
            curlazer2.GetComponent<lazer2lookat>().cammanager = Cammanager;
        }
        
    }

    IEnumerator Lazer1_Hit_co2(Vector2 direction)
    {
        yield return new WaitForSeconds(0.75f);
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject currentlazer = Instantiate(tearlazer, denyhandy.transform.position, Quaternion.Euler(0, 0, targetAngle + 90));

    }

    public void WhenHitplayer()
    {
        Debug.Log("아앆!!!!!공습경!보!!!!!!");
        StartCoroutine(Trapal_Penetrate_When_attacked_co());
    }

    IEnumerator Trapal_Penetrate_When_attacked_co()
    {
        if (trapal_point.transform.childCount > 0)
        {
            int index = trapal_point.transform.childCount - 1;
            Transform curpenetrate = trapal_point.transform.GetChild(index);
            curpenetrate.SetParent(null);
            Vector3 direction = (player.transform.position - curpenetrate.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            curpenetrate.DORotate(new Vector3(0, 0, targetAngle), 1f).SetEase(Ease.OutQuart);
            yield return new WaitForSeconds(1.2f);
            float opposangle = curpenetrate.eulerAngles.z + 90; //일단 각도에 180을 더함
            float rad = opposangle * Mathf.Deg2Rad;//라디안으로 변환
            Vector2 direction1 = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * -7f;//거따가 5씩 곱해서 그뭐냐 그 원
            Vector2 spawnPos = (Vector2)curpenetrate.position + direction1; //그걸이제 위치에 더해서 어따소환할지 정함 어잠만
            GameObject curlazer2 = Instantiate(lazer2_small, spawnPos, Quaternion.identity);
            curlazer2.transform.position = new Vector3(curlazer2.transform.position.x, curlazer2.transform.position.y, -6.5f);
            lazer2lookat curlazer2_player_Trapal_Lazer2 = curlazer2.GetComponent<lazer2lookat>();
            curlazer2_player_Trapal_Lazer2.canshoot = false;
            curlazer2_player_Trapal_Lazer2.player = curpenetrate.gameObject;
            curlazer2_player_Trapal_Lazer2.look = true;
            curlazer2_player_Trapal_Lazer2.cammanager = cammanager;
            curlazer2_player_Trapal_Lazer2.ShootNotDes();
            enemyattack curpenetrate_playerattackdamage = curpenetrate.GetComponent<enemyattack>();
            curpenetrate_playerattackdamage.player = player;
            curpenetrate_playerattackdamage.enemy = gameObject;
            curpenetrate_playerattackdamage.damage = 40;
            curpenetrate.GetComponent<Rigidbody2D>().AddForce(90f * direction, ForceMode2D.Impulse);
            curpenetrate.GetComponent<enemyattack>().canattack = true;
            curpenetrate.GetComponent<enemyattack>().heavyattack = true;
            curpenetrate.GetComponent<enemyattack>().hit = true;
            yield return new WaitForSeconds(0.1f);
            curlazer2.GetComponent<lazer2lookat>().look = false;
            boss_hpbar.StackInstance instance = BossstackHander.activeStacks.Find(s => s.stackData.effectName == "부정");
            if (instance != null)
            {
                if (instance.currentStack >= 12)
                {
                    Debug.Log(instance.currentStack);
                    yield return new WaitForSeconds(0.4f);
                    curlazer2.GetComponent<lazer2lookat>().player = null;
                    curlazer2.GetComponent<lazer2lookat>().Shoot();

                }
                else
                {
                    curlazer2.GetComponent<lazer2lookat>().Dest();
                }
            }
            else
            {
                curlazer2.GetComponent<lazer2lookat>().Dest();
            }
        }
        
        
    }
}
