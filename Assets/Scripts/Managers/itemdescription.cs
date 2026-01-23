using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class itemdescription : MonoBehaviour
{
    public item itemdata;
    public Rapport rapportdata;

    public Image itemimage;
    public TMP_Text itemname;
    public TMP_Text itemdescription_;
    public TMP_Text itemtag;
    public TMP_Text itemstory;


    public void Start()
    {
        itemimage = uimanager.Instance.itemimage.GetComponent<Image>();
        itemname = uimanager.Instance.itemname.GetComponent<TMP_Text>();
        itemtag = uimanager.Instance.itemtag.GetComponent<TMP_Text>();
        itemstory = uimanager.Instance.itemstroy.GetComponent<TMP_Text>();
        itemdescription_ = uimanager.Instance.itemdescription.GetComponent<TMP_Text>();
    }

    public void ItemDescription()
    {
        if (itemdata != null)
        {
            itemimage.sprite = itemdata.itemImage;
            itemname.text = itemdata.itemName;
            itemdescription_.text = itemdata.itemdescription;
            itemstory.text = itemdata.stroydescription;
            if (itemdata.itemtag_ == item.itemtag.item)
            {
                itemtag.text = "아이템";
            }
            else if (itemdata.itemtag_ == item.itemtag.rapport)
            {
                itemtag.text = "라포";
            }
        }
        else if (rapportdata != null)
        {
            itemimage.sprite = rapportdata.itemImage;
            itemname.text = rapportdata.itemName;
            itemdescription_.text = rapportdata.itemdescription;
            itemstory.text = rapportdata.stroydescription;
            if (rapportdata.itemtag_ == item.itemtag.item)
            {
                itemtag.text = "아이템";
            }
            else if (rapportdata.itemtag_ == item.itemtag.rapport)
            {
                itemtag.text = "라포";
            }
        }
    }
}
