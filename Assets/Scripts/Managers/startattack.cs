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

    public void StartFight()
    {
        if (attackcore.GetComponent<attackcore>().attacklist_original.Find(x => x.normalskill == true) && attackcore.GetComponent<attackcore>().attacklist_original[attackcore.GetComponent<attackcore>().attacklist_original.Count - 1].normalskill == true)
        {
            Time.timeScale = 1f;
            selectUi.SetActive(false);
            attackcore.GetComponent<attackcore>().ArreyComplete();
            attackcore.GetComponent<attackcore>().StartCircum();
        }
        else
        {
            StartCoroutine(Fade());
        }
        
    }

    IEnumerator Fade()
    {
        warning.SetActive(true);
        warning.GetComponent<Image>().DOFade(1, 0);
        warning.transform.GetChild(0).GetComponent<TMP_Text>().DOFade(1, 0);
        yield return new WaitForSeconds(2.1f);
        warning.GetComponent<Image>().DOFade(0, 1);
        warning.transform.GetChild(0).GetComponent<TMP_Text>().DOFade(0, 1);
        yield return new WaitForSeconds(1.1f);
        warning.SetActive(false);
    }
}
