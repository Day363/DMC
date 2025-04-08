using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class damagetext : MonoBehaviour
{
    public int damage;
    public float speeddown;
    public float movespeed;
    public float alphaspeed;
    
    TextMeshPro text;
    Color alpha;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = damage.ToString();
    }

    void Update()
    {
        if (movespeed > 0)
        {
            movespeed = movespeed - speeddown;
            transform.Translate(new Vector3(0, movespeed * Time.deltaTime, 0));
        }

        if (movespeed < 1)
        {
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaspeed);
            text.color = alpha;
        }
        
        if (alpha.a <= 0.01)
        {
            Destroy(gameObject);
        }
    }
}

