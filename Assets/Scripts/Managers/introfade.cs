using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class introfade : MonoBehaviour
{
    public float time;


    public void Awake()
    {
        StartCoroutine(Fade());
        
    }

    IEnumerator Fade()
    {
        GetComponent<Image>().DOFade(1f, 2.5f);
        yield return new WaitForSeconds(time);
        GetComponent<Image>().DOFade(0f, 2.5f);
        yield return new WaitForSeconds(2.6f);
        gameObject.SetActive(false);
    }
}
