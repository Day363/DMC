using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class trapal_passive : MonoBehaviour
{
    public GameObject player;
    public GameObject camtarget;
    public GameObject Cammanager;
    public GameObject denydistorsion;
    public int denycount;
    public int certaincount;
    public Stack deny;
    public Stack certain;
    public GameObject denyeye;

    public bool canApplystack = true;

    public GameObject[] backgrounds;
    public GameObject[] etcs;

    private void OnEnable()
    {
        boss_hpbar.OnHitCalled += Deny;
        playerhit.OnHitCalled += Certain;
    }

    public void FixedUpdate()
    {
        boss_hpbar.StackInstance DenyInstance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "부정");
        boss_hpbar.StackInstance CertainInstance = GetComponent<boss_hpbar>().activeStacks.Find(s => s.stackData.effectName == "확신");
        
        if (DenyInstance != null)
        {
            if (DenyInstance.currentStack >= 12)
            {
                GetComponent<Animator>().SetBool("deny", true);
            }
            else if (DenyInstance.currentStack < 12)
            {
                GetComponent<Animator>().SetBool("deny", false);
            }

            if (DenyInstance.currentStack >= 24)
            {
                GetComponent<boss_hpbar>().RemoveStack(deny, 24);
                GetComponent<Animator>().SetBool("idle", false);
                GetComponent<Animator>().SetTrigger("deny24");
                canApplystack = false;
                
            }
        }

        if (CertainInstance != null)
        {
            if (CertainInstance.currentStack >= 12)
            {
                GetComponent<Animator>().SetBool("certain", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("certain", false);
            }

            if (CertainInstance.currentStack >= 24)
            {
                GetComponent<boss_hpbar>().RemoveStack(certain, 24);

                foreach (GameObject background in backgrounds)
                {
                    background.SetActive(false);
                }

                GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);

                foreach (GameObject etc in etcs)
                {
                    etc.SetActive(false);
                }
            }
        }

        

        

        
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
                GetComponent<boss_hpbar>().ApplyStack(deny, 1);
            }
        }
        
    }

    public void SmallCamIn()
    {
        Cammanager.GetComponent<CameraManager>().Looksmallpoint(camtarget);
    }

    public void SmallcamoutToPlayerCamDelay_()
    {
        StartCoroutine(PlayerCam());
    }

    IEnumerator PlayerCam()
    {
        yield return new WaitForSeconds(1f);
        Cammanager.GetComponent<CameraManager>().LookPlayer();
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
        GetComponent<trapal_script>().lazer2time = true;
        GetComponent<Animator>().SetBool("idle", true);
        yield return new WaitForSeconds(19f);
        Cammanager.GetComponent<CameraManager>().LookPlayer();
        deny.transform.DOScale(new Vector3(0, 0, 1), 0.8f).SetEase(Ease.InQuart);
        Destroy(eye);
        yield return new WaitForSeconds(0.9f);
        Destroy(deny);
    }
}
