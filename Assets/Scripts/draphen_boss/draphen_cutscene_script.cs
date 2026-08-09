using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Playables;
using UnityEngine.UI;
using DG.Tweening;

public class draphen_script : MonoBehaviour
{
    public bool firstmet;
    public GameObject player;
    public GameObject boss;
    public GameObject fadeout;

    public void End()
    {
        fadeout.GetComponent<Image>().DOFade(1, 1.5f);
        StartCoroutine(End_co());
    }

    IEnumerator End_co()
    {
        yield return new WaitForSeconds(2f);
        fadeout.GetComponent<Image>().DOFade(0, 1f);
        gameObject.SetActive(false);
    }

    public void Update()
    {
        if (!firstmet && Vector3.Distance(transform.position, player.transform.position) < 24)
        {
            firstmet = true;
            GetComponent<PlayableDirector>().Play();
        }
    }
}
