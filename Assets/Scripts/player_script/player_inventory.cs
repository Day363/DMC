using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class player_inventory : MonoBehaviour
{
    public static player_inventory instance;

    public GameObject gamemanager;

    public List<Weapon> weaponinv;
    public List<Rapport> rapportinv;
    public List<item> iteminv_;


    public GameObject invUi;
    public GameObject iteminv;
    public GameObject iteminv2;

    public GameObject itemimage;

    public bool canopeninv = true;
    public bool invactive;

    public Image ditemimage;
    public TMP_Text ditemname;
    public TMP_Text ditemdescription;
    public TMP_Text ditemtag;
    public TMP_Text ditemstory;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        battalemanager.WhenDataSave += DataSave;
        gamemanager = battalemanager.Instance.gameObject;
        weaponinv = gamemanager.GetComponent<battalemanager>().playerweaponinv;
        rapportinv = gamemanager.GetComponent<battalemanager>().playerrapportinv;
        iteminv_ = battalemanager.Instance.playeriteminv;
    }

    public void DataSave()
    {
        battalemanager.Instance.playerweaponinv = weaponinv;
        battalemanager.Instance.playerrapportinv = rapportinv;
        battalemanager.Instance.playeriteminv = iteminv_;
    }

    public void Update()
    {
        if (!invactive && canopeninv && Input.GetButtonDown("ebutton"))
        {
            invactive = true;

            uimanager.Instance.invall.SetActive(true);
            if (uimanager.Instance.iteminv1.transform.childCount > 0)
            {
                for (int i = uimanager.Instance.iteminv1.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(uimanager.Instance.iteminv1.transform.GetChild(i).gameObject);
                }
            }

            if (uimanager.Instance.iteminv2.transform.childCount > 0)
            {
                for (int i = uimanager.Instance.iteminv2.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(uimanager.Instance.iteminv2.transform.GetChild(i).gameObject);
                }
            }

            if (rapportinv.Count > 0)
            {
                foreach (Rapport rapport in rapportinv)
                {
                    GameObject currentitemimage = Instantiate(itemimage, uimanager.Instance.iteminv1.transform);
                    currentitemimage.GetComponent<Image>().sprite = rapport.itemImage;
                    itemdescription currentitemimageitemdescription = currentitemimage.GetComponent<itemdescription>();
                    currentitemimageitemdescription.rapportdata = rapport;
                    currentitemimageitemdescription.itemimage = ditemimage;
                    currentitemimageitemdescription.itemname = ditemname;
                    currentitemimageitemdescription.itemdescription_ = ditemdescription;
                    currentitemimageitemdescription.itemtag = ditemtag;
                    currentitemimageitemdescription.itemstory = ditemstory;
                }
            }

            if (iteminv_.Count > 0)
            {
                foreach (item item in iteminv_)
                {
                    GameObject currentitemimage = Instantiate(itemimage, uimanager.Instance.iteminv2.transform);
                    currentitemimage.GetComponent<Image>().sprite = item.itemImage;
                    itemdescription currentitemimageitemdescription = currentitemimage.GetComponent<itemdescription>();
                    currentitemimageitemdescription.itemdata = item;
                    currentitemimageitemdescription.itemimage = ditemimage;
                    currentitemimageitemdescription.itemname = ditemname;
                    currentitemimageitemdescription.itemdescription_ = ditemdescription;
                    currentitemimageitemdescription.itemtag = ditemtag;
                    currentitemimageitemdescription.itemstory = ditemstory;
                }
            }

        }
        else if (invactive && canopeninv && Input.GetButtonDown("ebutton"))
        {
            invactive = false;
            uimanager.Instance.invall.SetActive(false);
        }
        
    }

    public void AddRapport(Rapport rapport)
    {
        rapportinv.Add(rapport);
        GetComponent<playerstatus>().RapportAdd();
    }

    public void ItemAdd(item item)
    {
        iteminv_.Add(item);

    }

    public void AddWeapon(Weapon weapon)
    {
        weaponinv.Add(weapon);
    }
}
