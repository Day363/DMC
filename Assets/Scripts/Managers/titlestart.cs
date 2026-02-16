using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class titlestart : MonoBehaviour
{
    public List<GameObject> uis = new List<GameObject> { };
    public GameObject currentobject;
    public GameObject text;
    public GameObject titletotal;
    public GameObject cammanager;
    public GameObject player;

    public void Start()
    {
        cammanager = battalemanager.Instance.cameramanager;
        player = battalemanager.Instance.player;
    }

    public void Startgame()
    {
        List<GameObject> currentui = uis;
        currentui.Remove(currentobject);
        foreach (GameObject ui in currentui)
        {
            ui.GetComponent<RectTransform>().DOAnchorPosX(-600f, 0.7f).SetEase(Ease.OutQuart);
        }

        //cammanager.GetComponent<CameraManager>().LookPlayer();

        currentobject.GetComponent<Image>().DOFade(0f, 3f).SetEase(Ease.OutQuart);
        text.GetComponent<TMP_Text>().DOFade(0f, 3f).SetEase(Ease.OutQuart);
        StartCoroutine(Disable_title());

        uimanager.Instance.tutorialui.SetActive(true);
    }

    IEnumerator Disable_title()
    {
        yield return new WaitForSeconds(3.1f);
        titletotal.SetActive(false);
        //player.GetComponent<PlayerMove>().canmove = true;
    }
}
