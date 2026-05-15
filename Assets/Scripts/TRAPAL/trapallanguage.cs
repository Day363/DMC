using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapallanguage : MonoBehaviour
{
    public List<string> codes;
    public string code;
    public string encodedCode;
    public GameObject[] texts;
    public int randint;

    public int max;
    public int current = 0;
    public int spawnIndex;

    public GameObject currenttext;

    public bool autodisabled = true;

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

    private void Start()
    {
        code = codes[Random.Range(0, codes.Count)];
        Encode(code);
        StartCoroutine(DeleteALL());
    }

    private void FixedUpdate()
    {
        if (string.IsNullOrEmpty(encodedCode)) return;
        if (spawnIndex >= encodedCode.Length) return;

        string numStr = encodedCode.Substring(spawnIndex, 2);
        int index = int.Parse(numStr);

        if (index >= 0 && index < texts.Length)
        {
            //soundmanager.instance.SoundPlay("trapal_text");

            GameObject set = Instantiate(texts[index], transform);
            var sr = set.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "uiobject";
            sr.DOFade(0, 5f).SetUpdate(true).SetEase(Ease.InQuart);

            StartCoroutine(Delete(set));
        }

        spawnIndex += 2;
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
        encodedCode = encoded;
        spawnIndex = 0;

        Debug.Log(encoded);
    }

    IEnumerator Delete(GameObject text)
    {
        yield return new WaitForSeconds(3.5f);
        if (autodisabled)
        {
            Destroy(text);
        }
        
    }

    IEnumerator DeleteALL()
    {
        yield return new WaitForSeconds(10f);
        if (autodisabled)
        {
            Destroy(gameObject);
        }
        
    }
}
