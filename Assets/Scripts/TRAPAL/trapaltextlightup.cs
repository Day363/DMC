using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapaltextlightup : MonoBehaviour
{
    public float fadeDuration = 2f; 

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        Color startColor = originalColor;
        startColor.a = 0f;
        spriteRenderer.color = startColor;
    }

    void Update()
    {
        if (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration); 
            Color newColor = originalColor;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
        }
    }
}
