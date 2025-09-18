using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemgive : MonoBehaviour
{
    public GameObject gamemanager;
    public GameObject me;

    public Rapport[] correctrapport;

    public GameObject player;
    public GameObject fbutton;
    public bool cangive = false;
    public bool uiopen = false;

    public GameObject giveui;
    public GameObject giveuiselect;
    public GameObject selectbutton;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fbutton.SetActive(true);
            cangive = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fbutton.SetActive(false);
            cangive = false;
        }
    }

    public void Update()
    {
        if (!uiopen && cangive && Input.GetButtonDown("fbutton"))
        {
            gamemanager.GetComponent<itemgivemanager>().currentobject = me;
            gamemanager.GetComponent<itemgivemanager>().corrects = correctrapport;

            uiopen = true;
            giveui.SetActive(true);

            if (giveuiselect.transform.childCount > 0)
            {
                for (int i = giveuiselect.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(giveuiselect.transform.GetChild(i).gameObject);
                }
            }

            if (player.GetComponent<player_inventory>().rapportinv.Count > 0)
            {
                foreach (Rapport rapport in player.GetComponent<player_inventory>().rapportinv)
                {
                    GameObject currentbutton = Instantiate(selectbutton, giveuiselect.transform);
                    currentbutton.GetComponent<Image>().sprite = rapport.itemImage;
                    currentbutton.GetComponent<itemgivebutton>().gamemanager = gamemanager;
                    currentbutton.GetComponent<itemgivebutton>().currentrapport = rapport;
                }
            }
        }
        else if (uiopen && cangive && Input.GetButtonDown("fbutton"))
        {
            uiopen = false;
            giveui.SetActive(false);
        }
    }
}
