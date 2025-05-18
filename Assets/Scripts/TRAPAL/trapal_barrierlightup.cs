using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_barrierlightup : MonoBehaviour
{
    public float fadeDuration = 2f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void StartCo()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        float timer = 0f;

        Color startColor = originalColor;
        startColor.a = 0f;
        spriteRenderer.color = startColor;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            Color newColor = originalColor;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
            yield return null;
        }

        Color finalColor = originalColor;
        finalColor.a = 1f;
        spriteRenderer.color = finalColor;
    }
}
