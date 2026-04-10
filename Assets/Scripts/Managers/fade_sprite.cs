using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class fade_sprite : MonoBehaviour
{
    public float fadetime = 1.5f;

    public void Start()
    {
        GetComponent<SpriteRenderer>().DOFade(0, fadetime);
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(fadetime + 0.1f);
        Destroy(gameObject);
    }
}
