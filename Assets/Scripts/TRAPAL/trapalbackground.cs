using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapalbackground : MonoBehaviour
{
    public GameObject floor;
    public GameObject sbackground;
    public GameObject background;
    public GameObject backlight;
    public bool enterfight = false;
    public bool lightentering = false;

    public void Awake()
    {
        sbackground.SetActive(true);
        background.SetActive(true);
    }

    public void Update()
    {
        if (enterfight)
        {
            SpriteRenderer renderer1 = background.GetComponent<SpriteRenderer>();
            SpriteRenderer renderer = floor.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            Color color1 = renderer1.color;
            color.a = color.a - 0.001f;
            color1.a = color1.a + 0.001f;
            renderer.color = color;
            renderer1.color = color1;
        }
        
        if (lightentering)
        {
            SpriteRenderer renderer2 = backlight.GetComponent<SpriteRenderer>();
            Color color2 = renderer2.color;
            color2.a = color2.a + 0.001f;
            renderer2.color = color2;
        }
    }
}
