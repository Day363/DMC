using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class damagetext : MonoBehaviour
{
    public int damage;
    public float speeddown;
    public float movespeed;
    public float alphaspeed;
    public int wherexpos;

    public bool slash;
    public bool penetarte;
    public bool blow;

    public Sprite slashsprite;
    public Sprite penetratesprite;
    public Sprite blowsprite;

    SpriteRenderer img;
    TextMeshPro text;
    Color alpha;

    private void Start()
    {
        movespeed += Random.Range(-1f, 2f);

        text = GetComponentInChildren<TextMeshPro>();
        img = GetComponentInChildren<SpriteRenderer>();
        alpha = text.color;
        text.text = damage.ToString();

        if (damage > 100)
        {
            transform.localScale = new Vector3(damage / 100, damage / 100, 1);
        }

        float posx = transform.position.x;
        if (wherexpos == -1)
        {
            posx += Random.Range(0.5f, 2f);
        }
        else if (wherexpos == 1)
        {
            posx += Random.Range(-0.5f, -2f);
        }
        transform.DOMoveX(posx, 2f).SetEase(Ease.OutQuart);

        if (slash)
        {
            img.sprite = slashsprite;
        }
        else if (penetarte)
        {
            img.sprite = penetratesprite;
        }
        else if (blow)
        {
            img.sprite = blowsprite;
        }
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
            img.color = alpha;
        }
        
        if (alpha.a <= 0.01)
        {
            Destroy(gameObject);
        }
    }
}

