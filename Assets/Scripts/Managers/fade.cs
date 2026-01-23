using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class fade : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 1f;
        GetComponent<Image>().DOFade(0, 1.5f);
    }
}
