using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_inventory : MonoBehaviour
{
    public List<Weapon> weaponinv;
    public List<Rapport> rapportinv;
    

    public GameObject invUi;
    public GameObject iteminv;

    public GameObject itemimage;

    public bool canopeninv = true;
    public bool invactive;

    public void Update()
    {
        if (!invactive && canopeninv && Input.GetButtonDown("ebutton"))
        {
            invactive = true;

            invUi.SetActive(true);
            if (iteminv.transform.childCount > 0)
            {
                for (int i = iteminv.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(iteminv.transform.GetChild(i).gameObject);
                }
            }
            
            if (rapportinv.Count > 0)
            {
                foreach (Rapport rapport in rapportinv)
                {
                    GameObject currentitemimage = Instantiate(itemimage, iteminv.transform);
                    currentitemimage.GetComponent<Image>().sprite = rapport.itemImage;
                }
            }
            
        }
        else if (invactive && canopeninv && Input.GetButtonDown("ebutton"))
        {
            invactive = false;
            invUi.SetActive(false);
        }
        
    }
}
