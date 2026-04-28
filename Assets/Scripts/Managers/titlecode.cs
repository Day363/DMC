using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class titlecode : MonoBehaviour
{
    public string code;
    public GameObject[] objects;
    public float movespeed;
    public float time;

    public string[] CHO =
    {
        "r","R","s","e","E","f","a","q","Q","t",
        "T","d","w","W","c","z","x","v","g"
    };

    public string[] JUNG =
    {
        "k","o","i","O","j","p","u","P","h",
        "hk","ho","hl","y","n","nj","np","nl",
        "b","m","ml","l"
    };

    public string[] JONG =
    {
        "", "r","R","rt","s","sw","sg","e","f",
        "fr","fa","fq","ft","fx","fv","fg","a",
        "q","qt","t","T","d","w","c","z","x",
        "v","g"
    };

    public void Start()
    {
        Encode(code);
        Move();
    }

    public void Encode(string text)
    {
        List<string> result = new List<string>();

        foreach (char ch in text)
        {
            if (ch >= 0xAC00 && ch <= 0xD7A3)
            {
                int baseCode = ch - 0xAC00;

                int cho = baseCode / 588;
                int jung = (baseCode % 588) / 28;
                int jong = baseCode % 28;

                string english =
                    CHO[cho] +
                    JUNG[jung] +
                    JONG[jong];

                foreach (char e in english)
                {
                    int num = char.ToLower(e) - 'a';
                    result.Add(num.ToString("D2"));
                }

                result.Add("26");
            }
        }

        string encoded = string.Join("", result);

        //Debug.Log(encoded);
        SpawnObjects(encoded);
    }

    public void SpawnObjects(string code)
    {
        for (int i = 0; i < code.Length; i += 2)
        {
            string numStr = code.Substring(i, 2);
            int index = int.Parse(numStr);

            if (index >= 0 && index < objects.Length)
            {
                GameObject set = Instantiate(objects[index], transform);
                set.GetComponent<SpriteRenderer>().sortingLayerName = "uiobject";
            }
        }
        GetComponent<layouttext>().LayOut();
        foreach(Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().DOFade(0, 5f).SetUpdate(true).SetEase(Ease.InQuart);
        }
    }

    public void Move()
    {
        transform.DOLocalMoveX(transform.childCount * movespeed, time).SetUpdate(true);
    }
}
