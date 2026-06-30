using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class numbertooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool playerbalance;
    public bool playerfocus;
    public bool enemybalance;
    public bool enemyfocus;
    public GameObject currentenemy;

    public void OnPointerEnter(PointerEventData eventData)
    {
        uimanager.Instance.tooltip2.SetActive(true);
        uimanager.Instance.tooltip2.transform.position = Input.mousePosition;
        Showtooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uimanager.Instance.tooltip2.SetActive(false);
    }

    public void Showtooltip()
    {
        if (playerbalance)
        {
            uimanager.Instance.tooltip2.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{battalemanager.Instance.player.GetComponent<playerstatus>().currentbalance}/{battalemanager.Instance.player.GetComponent<playerstatus>().maxbalance}";
        }

        if (playerfocus) 
        {
            uimanager.Instance.tooltip2.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{battalemanager.Instance.attackcore.GetComponent<attackcore>().currentfocus}/{battalemanager.Instance.player.GetComponent<playerstatus>().focus}";
        }

        if (enemybalance)
        {
            uimanager.Instance.tooltip2.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{currentenemy.GetComponent<boss_hpbar>().currentbalance}/{currentenemy.GetComponent<boss_hpbar>().maxbalance}";
        }

        if (enemyfocus)
        {
            uimanager.Instance.tooltip2.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{currentenemy.GetComponent<boss_hpbar>().currentfocus}/{currentenemy.GetComponent<boss_hpbar>().maxfocus}";
        }
    }
}
