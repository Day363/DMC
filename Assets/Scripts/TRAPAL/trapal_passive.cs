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
    public GameObject mask;
    public GameObject glitch;

    public bool canApplystack = true;

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
                GetComponent<trapal_script>().canattack = false;
                GetComponent<boss_hpbar>().RemoveStack(deny, 24);
                GetComponent<Animator>().SetBool("idle", false);
                GetComponent<Animator>().SetTrigger("deny24");
                canApplystack = false;

            }
        }
        else
        {
            GetComponent<Animator>().SetBool("deny", false);
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
                canApplystack = false;
                StartCoroutine(Certain24());
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("certain", false);
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
    }

    IEnumerator Glitch()
    {
        
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + UnityEngine.Random.Range(-14f, 14f), 0);
        GameObject curglitch = Instantiate(glitch, pos, Quaternion.identity);
        curglitch.transform.localScale = new Vector3(300f, Random.Range(0.12f, 0.6f), 1);
        curglitch.GetComponent<SpriteRenderer>().material.SetVector("_moveto", new Vector2(UnityEngine.Random.Range(-0.05f, 0.05f), 0));
        yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        Destroy(curglitch);
        
    }


    IEnumerator Certain24()
    {
        GetComponent<trapal_script>().canattack = false;

        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(1);

            for (int x = 0; x < Random.Range(7, 15); x++)
            {

                StartCoroutine(Glitch());
            }

            
        }
        GameObject currentmask = Instantiate(mask, transform.position, Quaternion.identity);
        currentmask.transform.localScale = new Vector3(200, 50, 1);

        yield return new WaitForSeconds(15f);
        GetComponent<trapal_script>().canattack = true;
        canApplystack = true;
        Destroy(currentmask);
    }
}
