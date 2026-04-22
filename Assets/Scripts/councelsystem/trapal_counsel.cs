using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class trapal_counsel : MonoBehaviour
{
    public GameObject player;
    public GameObject attackcore;
    public GameObject cammanager;
    public GameObject gamemanager;
    public GameObject campos;
    public GameObject campos2;
    public GameObject letterbox;
    public GameObject chat;
    public GameObject deny;
    public GameObject[] trapal_halo;
    public GameObject trapal_black_fade;
    public GameObject chatpre;
    public GameObject canvus;
    public GameObject blackbox;
    public GameObject floattextmanager;
    public GameObject glitchboxmanager;
    public GameObject lazerpos;
    public GameObject denytospawn;
    public GameObject dielazer;
    public GameObject counselmirror;
    public GameObject background;
    public GameObject counselcollider;

    public bool firstmet;
    public bool cameraset;
    public float camerasize;

    public string[] whispering;
    public bool say;

    public int cycleint_;
    public int cycleint;

    PlayerMove playerPlayerMove;
    CameraManager cammanagerCameraManager;
    boss_hpbar bosshp;

    public bool phase2;
    

    public void Start()
    {
        bosshp = GetComponent<boss_hpbar>();
        gamemanager = battalemanager.Instance.gameObject;
        boss_hpbar.Die += GoTo2Phase;
        playerPlayerMove = player.GetComponent<PlayerMove>();
        cammanagerCameraManager = cammanager.GetComponent<CameraManager>();
        letterbox = uimanager.Instance.letterbox;
    }

    public void LateUpdate()
    {

        if (cameraset)
        {
            cammanager.GetComponent<CameraManager>().maincam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = camerasize;
        }
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 10)
        {
            firstmet = true;

            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanagerCameraManager.LookCounsel(campos);
            cammanagerCameraManager.CinemachineInvalidateCache();
            uimanager.Instance.letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            playerPlayerMove.canmove = false;
            playerPlayerMove.Stop();
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            //gamemanager.GetComponent<chatmanager>().CallDialogue(3);
            gamemanager.GetComponent<chatmanager>().CallDialogue(15);

        }
    }

    public void PlayerFightUiClose()
    {
        uimanager.Instance.CloseFightUi();
    }


    public void AttackEnd()
    {
        cycleint_++;
        if (cycleint_ >= cycleint)
        {
            bosshp.CycleEnd();
            bosshp.CycleStart();
            cycleint_ = 0;
        }
    }

    public void Setdeny()
    {
        deny.SetActive(true);
        deny.transform.localScale = new Vector3(0, 0, 1);
        deny.transform.DOScale(new Vector3(0.38f, 0.38f, 1), 0.7f).SetEase(Ease.InOutQuart);
    }

    public void DenyKill()
    {
        deny.transform.DOScale(new Vector3(0f, 0f, 1), 0.7f).SetEase(Ease.InOutQuart);
    }

    public void BattleStart()
    {
        StartCoroutine(BattleStart_co());
        
    }

    IEnumerator BattleStart_co()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<trapal_script>().canattack = true;
        attackcore.GetComponent<attackcore>().SetCronometer();
    }

    public void LookSelf()
    {
        cammanagerCameraManager.Looksmallpoint(gameObject);
    }

    public void LookCampos()
    {
        cammanagerCameraManager.Looksmallpoint(campos2);
    }

    public void Zommin()
    {
        canvus.GetComponent<Canvas>().overrideSorting = true;
        canvus.GetComponent<Canvas>().sortingLayerName = "background";
        StartCoroutine(Zommin_co());
        
    }


    IEnumerator Zommin_co()
    {
        cameraset = true;
        DOTween.Kill("CameraZoom");
        DOTween.To(() => camerasize, x => camerasize = x, 2f, 20f).SetEase(Ease.OutCubic).SetUpdate(UpdateType.Late).SetId("CameraZoom");
        cammanager.GetComponent<CameraManager>().CamVibration20();
        GameObject currentblack = Instantiate(trapal_black_fade, transform.position, Quaternion.identity);
        currentblack.GetComponent<SpriteRenderer>().DOFade(1, 16f);
        floattextmanager.GetComponent<trapal_textmanager>().going = false;
        glitchboxmanager.GetComponent<trapalmaskmanager>().going = false;
        yield return new WaitForSeconds(20.1f);
        Instantiate(blackbox, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        camerasize = 4;
        yield return new WaitForEndOfFrame();
        cameraset = false;
        canvus.GetComponent<Canvas>().sortingLayerName = "uiobject";
    }

    public void Whispering()
    {
        StartCoroutine(Whispering_co());
    }

    IEnumerator Whispering_co()
    {
        float delay = 1.5f;
        DOTween.To(() => delay, x => delay = x, 0.001f, 18f).SetEase(Ease.OutCubic);

        say = true;
        StartCoroutine(SayFalse());

        while (say)
        {
            StartCoroutine(Whispering_co_co());
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator SayFalse()
    {
        yield return new WaitForSeconds(20f);
        say = false;
    }

    IEnumerator Whispering_co_co()
    {
        GameObject currentchat = Instantiate(chatpre, canvus.transform);
        currentchat.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        currentchat.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0);
        currentchat.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));
        currentchat.transform.localScale = new Vector3(2.4f, 2.4f, 2.4f);
        TMP_Text tmp = currentchat.transform.GetChild(0).GetComponent<TMP_Text>();
        TextEffectManager shaker = tmp.GetComponent<TextEffectManager>();
        shaker.SetText(whispering[Random.Range(0, whispering.Length)]);
        tmp.fontSize = Random.Range(2f, 4f);
        tmp.color = new Color(0.5f, 0, 0, Random.Range(0.3f, 1f));
        tmp.ForceMeshUpdate();
        int totalvisible = tmp.textInfo.characterCount;
        tmp.maxVisibleCharacters = 0;
        int visible = 0;
        while (visible < totalvisible)
        {
            visible++;
            tmp.maxVisibleCharacters = visible;
            if (shaker != null)
                shaker.CheckEvents(visible);
            float wait = 0.15f;
            yield return new WaitForSeconds(wait);
        }
        yield return new WaitForSeconds(1.5f);
        tmp.DOFade(0, 1f);
        yield return new WaitForSeconds(1.5f);
        Destroy(currentchat);
    }

    public void Lookplaer()
    {
        StartCoroutine(Lookplaer_co());
        
    }

    public void Lookplayer()
    {
        cammanagerCameraManager.LookPlayer();
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
    }

    public void BattleStart2()
    {
        GetComponent<trapal_script>().canattack = true;
        attackcore.GetComponent<attackcore>().canattack = true;
        player.GetComponent<PlayerMove>().canmove = true;
        attackcore.GetComponent<attackcore>().SetCronometer();
    }

    IEnumerator Lookplaer_co()
    {
        yield return new WaitForSeconds(3);
        cammanagerCameraManager.LookPlayer();
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
    }

    public void GoTo2Phase()
    {
        if (!phase2)
        {
            phase2 = true;

            attackcore.GetComponent<attackcore>().canattack = false;
            player.GetComponent<PlayerMove>().canmove = false;

            player.GetComponent<playerhit>().canhit = false;
            GetComponent<trapal_script>().canattack = false;
            GetComponent<trapal_script>().phase2 = true;
            GetComponent<boss_hpbar>().candie = true;

            GetComponent<boss_hpbar>().currenthealth = GetComponent<boss_hpbar>().maxhealth;

            if (GetComponent<trapal_passive>().deny24count > GetComponent<trapal_passive>().certain24count)
            {
                DenyPhase();
            }
            else if (GetComponent<trapal_passive>().deny24count < GetComponent<trapal_passive>().certain24count)
            {
                CertainPhase();
            }
            else if (GetComponent<trapal_passive>().deny24count == GetComponent<trapal_passive>().certain24count)
            {
                boss_hpbar.StackInstance DenyInstance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "부정");
                boss_hpbar.StackInstance CertainInstance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "확신");
                if (DenyInstance.currentStack > CertainInstance.currentStack)
                {
                    DenyPhase();
                }
                else if (DenyInstance.currentStack < CertainInstance.currentStack)
                {
                    CertainPhase();
                }
                else if (DenyInstance.currentStack == CertainInstance.currentStack)
                {
                    int i = Random.Range(0, 2);
                    if (i == 0)
                    {
                        DenyPhase();
                    }
                    else if (i == 1)
                    {
                        CertainPhase();
                    }
                }
            }
        }
        else if (phase2)
        {
            attackcore.GetComponent<attackcore>().canattack = false;
            player.GetComponent<PlayerMove>().canmove = false;
            player.GetComponent<playerhit>().canhit = false;
            GetComponent<trapal_script>().canattack = false;

            GetComponent<Animator>().SetTrigger("trapal_phase2_die1");
            gamemanager.GetComponent<chatmanager>().CallDialogue(7);
        }
    }

    public void DenyPhase()
    {
        GetComponent<Animator>().SetTrigger("denyphase2entry");

        foreach (GameObject halo in trapal_halo)
        {
            StartCoroutine(Halo_Shutdown(halo));
        }
        transform.DOMoveY(1.74f, 5f).SetEase(Ease.OutQuad);
        StartCoroutine(SayCooltime());
    }

    IEnumerator SayCooltime()
    {
        yield return new WaitForSeconds(5f);
        campos.transform.position = transform.position;
        cammanagerCameraManager.LookEnemy();
        cammanagerCameraManager.CinemachineInvalidateCache();
        letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
        playerPlayerMove.canmove = false;
        playerPlayerMove.Stop();
        gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
        gamemanager.GetComponent<chatmanager>().CallDialogue(4);
        gamemanager.GetComponent<chatmanager>().CallDialogue(16);
    }

    IEnumerator Halo_Shutdown(GameObject halo)
    {
        yield return new WaitForSeconds(Random.Range(1f, 4.5f));
        halo.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.6f);
        halo.SetActive(false);
    }

    public void CertainPhase()
    {
        StartCoroutine(CertainPhase_co());
        
    }

    IEnumerator CertainPhase_co()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetTrigger("2phase-2_start");
    }

    public void Die1()
    {
        StartCoroutine(Die1_co());
    }

    IEnumerator Die1_co()
    {
        GameObject currentlazer = Instantiate(dielazer, new Vector3(transform.position.x, transform.position.y + 10f, -6.5f), Quaternion.identity);
        currentlazer.GetComponent<lazer2lookat>().look = true;
        currentlazer.GetComponent<lazer2lookat>().canwarning = false;
        currentlazer.GetComponent<lazer2lookat>().canshoot = false;
        currentlazer.GetComponent<lazer2lookat>().player = gameObject;
        currentlazer.GetComponent<lazer2lookat>().cammanager = cammanager;

        GameObject currentdeny = Instantiate(denytospawn, lazerpos.transform, true);
        currentdeny.transform.position = new Vector3(0, 0, 0);
        currentdeny.transform.localScale = new Vector3(0, 0, 1);
        currentdeny.transform.DOScale(1, 0.5f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetTrigger("trapal_phase2_die2");
        yield return new WaitForSeconds(1f);
        currentdeny.transform.DOScale(0, 1f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(1.1f);
        Destroy(currentdeny);
        yield return new WaitForSeconds(2f);

        currentlazer.GetComponent<lazer2lookat>().Shoot();
        GetComponent<SpriteRenderer>().DOFade(0, 0.1f);

        yield return new WaitForSeconds(3f);
        TOCounsel();
    }

    public void TOCounsel()
    {
        GameObject currentcounselmirror = Instantiate(counselmirror, new Vector3(-450f, 26.7f, 0), Quaternion.identity);
        tocounselmanager currentcounselmirrortocounselmanager = currentcounselmirror.GetComponent<tocounselmanager>();
        currentcounselmirrortocounselmanager.player = player;
        currentcounselmirrortocounselmanager.cammanager = cammanager;
    }
}
