using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tocounselmanager : MonoBehaviour
{
    public GameObject box;
    public GameObject particle;

    public void Start()
    {
        transform.localScale = new Vector3(0, 64, 1);
        transform.DOScaleX(1, 0.6f).SetEase(Ease.InQuart);
        StartCoroutine(SetParticle());

    }

    public void Spawn()
    {
        StartCoroutine(Spawn_co());
    }

    IEnumerator SetParticle()
    {
        yield return new WaitForSeconds(0.7f);
        particle.SetActive(true);
        InvokeRepeating("Spawn", 0f, 0.4f);
    }

    IEnumerator Spawn_co()
    {
        GameObject currentbox = Instantiate(box, transform.position, Quaternion.identity);
        currentbox.transform.DOScaleX(5f, 5f).SetEase(Ease.OutQuart);
        currentbox.GetComponent<SpriteRenderer>().DOFade(0f, 5f);//.SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(5.1f);
        Destroy(currentbox);
    }
}
