using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;


public class filer_light : MonoBehaviour
{
    public int wholerange;
    public int range;
    public bool doing = false;
    public int i;

    public void FixedUpdate()
    {
        i = Random.Range(1, wholerange);
        if (i <= range && !doing)
        {
            StartCoroutine(Light());
        }
    }

    IEnumerator Light()
    {
        doing = true;
        float intensity = GetComponent<Light2D>().intensity;
        GetComponent<Light2D>().intensity = 0;
        
        int x = Random.Range(1, 4);
        if (x < 2)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.15f));
            GetComponent<Light2D>().intensity = intensity;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.15f));
            GetComponent<Light2D>().intensity = 0;
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 3.5f));
        }
        DOTween.To(() => GetComponent<Light2D>().intensity, x => GetComponent<Light2D>().intensity = x, intensity, 1.5f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(1.6f);
        doing = false;
    }
}
