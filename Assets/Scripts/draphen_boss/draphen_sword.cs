using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class draphen_sword : MonoBehaviour
{
    public GameObject parts1;
    public GameObject parts2;
    public GameObject parts3;
    public GameObject parts4;
    public GameObject parts5;
    public GameObject parts6;
    public GameObject parts7;

    public void Handle()
    {
        parts2.GetComponent<draphen_parts>().turn = false;
        parts2.transform.DORotate(new Vector3(0, 0, -90), 1).SetEase(Ease.OutQuart);


    }

    public void AllStop()
    {
        DOTween.To(() => parts3.GetComponent<draphen_parts>().speed, x => parts3.GetComponent<draphen_parts>().speed = x, 0f, 1f).SetEase(Ease.OutQuart);
        DOTween.To(() => parts4.GetComponent<draphen_parts>().speed, x => parts4.GetComponent<draphen_parts>().speed = x, 0f, 1f).SetEase(Ease.OutQuart);
        DOTween.To(() => parts5.GetComponent<draphen_parts>().speed, x => parts5.GetComponent<draphen_parts>().speed = x, 0f, 1f).SetEase(Ease.OutQuart);
        DOTween.To(() => parts6.GetComponent<draphen_parts>().speed, x => parts6.GetComponent<draphen_parts>().speed = x, 0f, 1f).SetEase(Ease.OutQuart);
        DOTween.To(() => parts7.GetComponent<draphen_parts>().speed, x => parts7.GetComponent<draphen_parts>().speed = x, 0f, 1f).SetEase(Ease.OutQuart);

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(1.1f);
        parts3.transform.DORotate(new Vector3(0, 0, -90), 1).SetEase(Ease.OutQuart);
        parts4.transform.DORotate(new Vector3(0, 0, -90), 1).SetEase(Ease.OutQuart);
        parts5.transform.DORotate(new Vector3(0, 0, -90), 1).SetEase(Ease.OutQuart);
        parts6.transform.DORotate(new Vector3(0, 0, -90), 1).SetEase(Ease.OutQuart);
        parts7.transform.DORotate(new Vector3(0, 0, -90), 1).SetEase(Ease.OutQuart);

    }
}
