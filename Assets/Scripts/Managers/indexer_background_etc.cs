using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class indexer_background_etc : MonoBehaviour
{
    public GameObject worldlight;

    public void Start()
    {
        StartCoroutine(Lightning());
    }

    IEnumerator Lightning()
    {
        yield return new WaitForSeconds(Random.Range(10f, 30f));

        Light2D light = worldlight.GetComponent<Light2D>();

        int i = Random.Range(0, 3);

        if (i == 1)
        {
            DOTween.To(() => light.intensity, x => light.intensity = x, 2f, 0.1f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.11f);
            DOTween.To(() => light.intensity, x => light.intensity = x, 1f, 0.1f).SetEase(Ease.OutQuad);
        }
        else if (i == 2)
        {
            DOTween.To(() => light.intensity, x => light.intensity = x, 1.7f, 0.1f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.11f);
            DOTween.To(() => light.intensity, x => light.intensity = x, 1f, 0.1f).SetEase(Ease.OutQuad).SetId("lightning");
            yield return new WaitForSeconds(0.11f);
            DOTween.Kill("lightning");
            DOTween.To(() => light.intensity, x => light.intensity = x, 2f, 0.1f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.11f);
            DOTween.To(() => light.intensity, x => light.intensity = x, 1f, 0.1f).SetEase(Ease.OutQuad);
        }
        else if (i == 3)
        {
            DOTween.To(() => light.intensity, x => light.intensity = x, 1.7f, 0.1f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.11f);
            DOTween.To(() => light.intensity, x => light.intensity = x, 1f, 0.1f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.11f);
            DOTween.To(() => light.intensity, x => light.intensity = x, 1.3f, 0.1f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.11f);
            DOTween.To(() => light.intensity, x => light.intensity = x, 1f, 0.1f).SetEase(Ease.OutQuad).SetId("lightning");
            yield return new WaitForSeconds(0.25f);
            DOTween.Kill("lightning");
            DOTween.To(() => light.intensity, x => light.intensity = x, 2f, 0.1f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.11f);
            DOTween.To(() => light.intensity, x => light.intensity = x, 1f, 0.1f).SetEase(Ease.OutQuad);
        }

        yield return new WaitForSeconds(Random.Range(1.5f, 3f));

        i = Random.Range(0, 3);

        if (i == 0)
        {
            battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay("thunder1");
        }
        else if (i == 1)
        {
            battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay("thunder2");
        }
        else if (i == 1)
        {
            battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay("thunder3");
        }

        StartCoroutine(Lightning());
    }
}
