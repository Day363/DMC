using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class skillbuttonsize : MonoBehaviour
{
    public TMP_Text text;
    public RectTransform buttonRect;

    public void ButtonSize()
    {
        float width = text.preferredWidth + 1f; 
        float height = text.preferredHeight + 1f;
        buttonRect.sizeDelta = new Vector2(width, 36);
    }
}
