using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_pointattack : MonoBehaviour
{
    public GameObject[] rings;
    public GameObject warning;
    public GameObject shoot;
    public float fadeDuration = 5f;
    public float fadeoutDuration = 2f;

    private SpriteRenderer warningRenderer;
    private SpriteRenderer shootRenderer;

    void Start()
    {
        warningRenderer = warning.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeIn());
        shootRenderer = shoot.GetComponent<SpriteRenderer>();
    }

    IEnumerator FadeIn()
    {
        Color color = warningRenderer.color;
        color.a = 0f;
        warningRenderer.color = color;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            color.a = alpha;
            warningRenderer.color = color;
            yield return null;
        }

        color.a = 1f;
        warningRenderer.color = color;
        Startshoot();
    }

    void Startshoot()
    {
        rings[0].SetActive(false);
        rings[1].SetActive(false);
        rings[2].SetActive(false);
        rings[3].SetActive(false);
        warning.SetActive(false);
        shoot.SetActive(true);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = shootRenderer.color;
        float elapsed = 0f;

        while (elapsed < fadeoutDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsed / fadeoutDuration)); // 1 ¡æ 0
            color.a = alpha;
            shootRenderer.color = color;
            yield return null;
        }

        color.a = 0f;
        warningRenderer.color = color;
        Destroy(gameObject);
    }
}
