using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class stacktooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Stack stackdata;

    public void OnPointerEnter(PointerEventData eventData)
    {
        uimanager.Instance.tooltip.SetActive(true);
        uimanager.Instance.tooltip.transform.position = Input.mousePosition;
        Showtooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uimanager.Instance.tooltip.SetActive(false);
    }

    public void Showtooltip()
    {
        Debug.Log("eriuh");
        GameObject tooltip = uimanager.Instance.tooltip;
        tooltip.transform.GetChild(0).GetComponent<Image>().sprite = stackdata.icon;
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = stackdata.systemeffectName;
        tooltip.transform.GetChild(1).GetComponent<TMP_Text>().text = stackdata.description;
    }
}
