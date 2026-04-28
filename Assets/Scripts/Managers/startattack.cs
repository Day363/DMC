using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class startattack : MonoBehaviour
{
    public GameObject attackcore;
    public GameObject selectUi;
    public GameObject warning;

    public void Start()
    {
        attackcore = battalemanager.Instance.attackcore;
    }

    public void StartFight()
    {
        attackcore = battalemanager.Instance.attackcore;
        if (attackcore.GetComponent<attackcore>().attacklist_original.Find(x => x.normalskill == true) && attackcore.GetComponent<attackcore>().attacklist_original[attackcore.GetComponent<attackcore>().attacklist_original.Count - 1].normalskill == true)
        {
            Time.timeScale = 1f;
            selectUi.SetActive(false);
            attackcore.GetComponent<attackcore>().ArreyComplete();
            attackcore.GetComponent<attackcore>().StartCircum();
            if (battalemanager.Instance.cronometer.GetComponent<cronometer_script>().tutorial)
            {
                Time.timeScale = 0f;
                uimanager.Instance.TutorialGo2();
         
            }

            foreach (GameObject ui in uimanager.Instance.activewhenbattlestart)
            {
                ui.SetActive(true);
            }
        }
        else
        {
            StartCoroutine(Fade());
        }

        
    }

    IEnumerator Fade()
    {
        warning.SetActive(true);
        warning.GetComponent<Image>().DOFade(1, 0).SetUpdate(true);
        warning.transform.GetChild(0).GetComponent<TMP_Text>().DOFade(1, 0).SetUpdate(true);
        yield return new WaitForSecondsRealtime(2.1f);
        warning.GetComponent<Image>().DOFade(0, 1).SetUpdate(true);
        warning.transform.GetChild(0).GetComponent<TMP_Text>().DOFade(0, 1).SetUpdate(true);
        yield return new WaitForSecondsRealtime(1.1f);
        warning.SetActive(false);
    }
}
