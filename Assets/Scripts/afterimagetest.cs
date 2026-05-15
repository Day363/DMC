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
    public bool follow;
    public Color color;

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

        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2d))
        {
            currentimage.transform.localScale = transform.localScale;
        }
        else
        {
            currentimage.transform.localScale = transform.parent.localScale;
        }
            
        currentimage.GetComponent<SpriteRenderer>().sprite = sr.sprite;
        currentimage.GetComponent<fade_sprite>().fadetime = fadetime;
        currentimage.GetComponent<SpriteRenderer>().color = color;

        if (follow)
        {
            currentimage.GetComponent<fade_sprite>().follow = true;
            currentimage.GetComponent<fade_sprite>().original = transform;
        }

        yield return new WaitForSeconds(time);
        if (canspawn)
        {
            StartCoroutine(AfterImageGenerate());
        }
        
    }
}
