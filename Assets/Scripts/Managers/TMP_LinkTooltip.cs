using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TMP_LinkTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text;
    private bool isHovering = false;

    void Update()
    {
        if (!isHovering) return;

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
            string linkID = linkInfo.GetLinkID();

            ShowTooltip(linkID);
        }
        else
        {
            HideTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        HideTooltip();
    }

    void ShowTooltip(string id)
    {
        GameObject tooltip = uimanager.Instance.tooltip;
        tooltip.SetActive(true);
        tooltip.transform.position = Input.mousePosition;

        Stack stackdata = ToolTipManager.Instance.tooltipMap[id];
        tooltip.transform.GetChild(0).GetComponent<Image>().sprite = stackdata.icon;
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = stackdata.effectName;
        tooltip.transform.GetChild(1).GetComponent<TMP_Text>().text = stackdata.description;
    }

    void HideTooltip()
    {
        uimanager.Instance.tooltip.SetActive(false);

    }
}
