using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afterimage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color color;
    public float fadeSpeed = 1.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    void Update()
    {
        color.a -= fadeSpeed * Time.deltaTime;
        spriteRenderer.color = color;

        if (color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
