using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class filer_textflow : MonoBehaviour
{
    public GameObject text;
    public GameObject canvas;

    public Vector2 xrange;
    public Vector2 yrange;

    public string[] texts;

    public bool yelling = true;

    public void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(yelling)
        {
            SpawnText();
            yield return new WaitForSeconds(Random.Range(0.7f, 3.5f));
            
        }
        
    }

    public void SpawnText()
    {
        GameObject currenttext = Instantiate(text, canvas.transform);
        TMP_Text tmp = currenttext.transform.GetChild(0).GetComponent<TMP_Text>();
        tmp.fontSize = Random.Range(6.5f, 10f);
        tmp.color = new Color(1, 1, 1, Random.Range(0.45f, 0.8f));
        Vector3 chatpos = new Vector3(transform.position.x + Random.Range(xrange.x, xrange.y), transform.position.y + Random.Range(yrange.x, yrange.y), 0);
        currenttext.transform.position = chatpos;
        currenttext.transform.GetChild(0).transform.DOShakePosition(30f, new Vector3(1.2f, 1.2f, 0), 2, 90, false, false, ShakeRandomnessMode.Harmonic);
        StartCoroutine(Textstep(currenttext.transform.GetChild(0).gameObject, currenttext));
    }

    IEnumerator Textstep(GameObject target, GameObject delt)
    {
        TMP_Text tmp = target.GetComponent<TMP_Text>();

        tmp.text = texts[Random.Range(0, texts.Length)];
        tmp.ForceMeshUpdate();
        int totalvisible = tmp.textInfo.characterCount;
        tmp.maxVisibleCharacters = 0;

        int visible = 0;

        while (visible < totalvisible)
        {
            visible++;
            tmp.maxVisibleCharacters = visible;

            char c = GetPrintedCharAtIndex(tmp, visible - 1);
            float wait = 0.1f;
            if (IsPunctuation(c)) wait += 0.5f;

            yield return new WaitForSeconds(wait);
        }

        yield return new WaitForSeconds(0.5f);
        tmp.DOColor(new Color(1, 1, 1, 0), 0.8f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.9f);
        Destroy(delt);
    }

    char GetPrintedCharAtIndex(TMP_Text t, int visibleIndex)
    {
        // 안전장치
        if (visibleIndex < 0 || visibleIndex >= t.textInfo.characterCount) return '\0';
        var ci = t.textInfo.characterInfo[visibleIndex];
        return ci.character;
    }

    bool IsPunctuation(char c)
    {
        return c == '.' || c == ',' || c == '!' || c == '?' || c == ';' || c == ':' || c == '…';
    }
}
