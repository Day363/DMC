using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class afterimagetest : MonoBehaviour
{
    public bool generatewhenstart = false;

    public bool canspawn;
    public GameObject imageobject;
    public float time;
    public float fadetime;

    public SpriteRenderer sr;

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (generatewhenstart)
        {
            StartGenerate();
        }
    }

    [ContextMenu("StartGenerate")]
    public void StartGenerate()
    {
        canspawn = true;
        StartCoroutine(AfterImageGenerate());
    }

    [ContextMenu("EndGenerate")]
    public void EndGenerate()
    {
        canspawn = false;
    }

    IEnumerator AfterImageGenerate()
    {
        GameObject currentimage = Instantiate(imageobject, transform.position, transform.rotation);

        currentimage.transform.localScale = transform.parent.localScale;
        currentimage.GetComponent<SpriteRenderer>().sprite = sr.sprite;
        currentimage.GetComponent<fade_sprite>().fadetime = fadetime;

        yield return new WaitForSeconds(time);
        if (canspawn)
        {
            StartCoroutine(AfterImageGenerate());
        }
        
    }
}
