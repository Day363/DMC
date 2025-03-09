using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class RowText
{
    public string[] text;
}

public class counselsystem : MonoBehaviour
{
    public RowText[] textlist;
    public TMP_Text counseltext;
    public float delay;
    public bool telling = false;
    public GameObject playerchat;
    public GameObject selfchat;


    IEnumerator Typing(string text, float d)
    {
        int count = 0;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                counseltext.text += text[count].ToString();
                count++;
            }

            yield return new WaitForSeconds(d);
        }

        telling = false;
    }
}
