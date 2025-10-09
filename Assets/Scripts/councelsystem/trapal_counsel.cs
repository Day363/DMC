using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_counsel : MonoBehaviour
{
    public GameObject player;
    public GameObject attackcore;
    public GameObject cammanager;
    public GameObject gamemanager;
    public GameObject campos;
    public GameObject letterbox;
    public GameObject chat;
    public GameObject deny;
    public GameObject[] trapal_halo;

    public bool firstmet;

    PlayerMove playerPlayerMove;
    CameraManager cammanagerCameraManager;

    

    public void Start()
    {
        boss_hpbar.Die += GoTo2Phase;
        playerPlayerMove = player.GetComponent<PlayerMove>();
        cammanagerCameraManager = cammanager.GetComponent<CameraManager>();
    }

    public void FixedUpdate()
    {
        if (!firstmet && transform.position.x - player.transform.position.x < 10)
        {
            firstmet = true;

            campos.transform.position = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0);
            cammanagerCameraManager.LookCounsel(campos);
            cammanagerCameraManager.CinemachineInvalidateCache();
            letterbox.GetComponent<letterboxin>().PlayLetterboxIn();
            playerPlayerMove.canmove = false;
            playerPlayerMove.Stop();
            gamemanager.GetComponent<chatmanager>().enemychatbox = chat;
            gamemanager.GetComponent<chatmanager>().CallDialogue(3);

        }
    }

    public void Setdeny()
    {
        deny.SetActive(true);
        deny.transform.localScale = new Vector3(0, 0, 1);
        deny.transform.DOScale(new Vector3(0.38f, 0.38f, 1), 0.7f).SetEase(Ease.InOutQuart);
    }

    public void BattleStart()
    {
        StartCoroutine(BattleStart_co());
        
    }

    IEnumerator BattleStart_co()
    {
        yield return new WaitForSeconds(3);
        GetComponent<trapal_script>().canattack = true;
        yield return new WaitForSeconds(2);
        attackcore.GetComponent<attackcore>().BattleStart();
    }

    public void LookSelf()
    {
        cammanagerCameraManager.Looksmallpoint(gameObject);
    }

    public void Lookplaer()
    {
        StartCoroutine(Lookplaer_co());
        
    }

    IEnumerator Lookplaer_co()
    {
        yield return new WaitForSeconds(3);
        cammanagerCameraManager.LookPlayer();
        letterbox.GetComponent<letterboxin>().PlayLetterboxOut();
    }

    public void GoTo2Phase()
    {
        player.GetComponent<playerhit>().canhit = false;

        GetComponent<trapal_script>().canattack = false;
        GetComponent<trapal_script>().phase2 = true;
        GetComponent<boss_hpbar>().candie = true;

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

    public void DenyPhase()
    {
        foreach (GameObject halo in trapal_halo)
        {
            StartCoroutine(Halo_Shutdown(halo));
        }
        transform.DOMoveY(1.74f, 5f).SetEase(Ease.OutQuad);
    }

    IEnumerator Halo_Shutdown(GameObject halo)
    {
        yield return new WaitForSeconds(Random.Range(1f, 4.5f));
        halo.SetActive(false);
    }

    public void CertainPhase()
    {

    }
}
