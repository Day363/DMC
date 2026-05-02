using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiredflash : MonoBehaviour
{
    private void OnEnable()
    {
        Flash();
    }

    public void Flash()
    {
        DOTween.Kill(transform);
        GetComponent<Image>().DOFade(0f, 0f).SetUpdate(true).SetId("pauseuitween");
        GetComponent<Image>().DOFade(0.4f, 1.5f).SetUpdate(true).SetId("pauseuitween").SetEase(Ease.InQuad);
        //GetComponent<Image>().material.SetFloat("_power", 15f);
        //GetComponent<Image>().material.DOFloat(1f, "_power", 1f).SetUpdate(true).SetId("pauseuitween");
    }

    public void UnFlash()
    {
        
        GetComponent<Image>().DOFade(0f, 0f).SetUpdate(true).SetId("pauseuitween");
    }
}
