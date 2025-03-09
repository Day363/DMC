using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectManager : MonoBehaviour
{
    public GameObject[] entitylist;
    private int randomvalue;
    public List<int> selectnumber = new List<int>() {1, 2, 3};
    public List<GameObject> selectedobjects;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject councelbar;
    public GameObject TV;
    
    
    


    void Start()
    {
        

        for (int i = 0; i < 3; i++)
        {
            randomvalue = Random.Range(1, entitylist.Length);
            selectnumber[i] = randomvalue;
        }
        
        for (int n = 0; n < 3; n++)
        {
            selectedobjects[n] = entitylist[selectnumber[n]];
        }

        button1.GetComponent<TextMeshProUGUI>().text = $"1. [{selectedobjects[0].GetComponent<Entityinfo>().entitycode}] \n    {selectedobjects[0].GetComponent<Entityinfo>().selectline}";
        button2.GetComponent<TextMeshProUGUI>().text = $"2. [{selectedobjects[1].GetComponent<Entityinfo>().entitycode}] \n    {selectedobjects[1].GetComponent<Entityinfo>().selectline}";
        button3.GetComponent<TextMeshProUGUI>().text = $"3. [{selectedobjects[2].GetComponent<Entityinfo>().entitycode}] \n    {selectedobjects[2].GetComponent<Entityinfo>().selectline}";

    }

    public void entityspawn1()
    {
        selectedobjects[0].SetActive(true);
        TV.SetActive(false);
        GetComponent<Selecttime>().selection.SetActive(false);
        GetComponent<Selecttime>().s1active = false;
        GetComponent<Selecttime>().player.GetComponent<PlayerMove>().canmove = true;
        councelbar.SetActive(true);
        
    }

    public void entityspawn2()
    {
        selectedobjects[1].SetActive(true);
        TV.SetActive(false);
        GetComponent<Selecttime>().selection.SetActive(false);
        GetComponent<Selecttime>().s1active = false;
        GetComponent<Selecttime>().player.GetComponent<PlayerMove>().canmove = true;
        councelbar.SetActive(true);
    }

    public void entityspawn3()
    {
        selectedobjects[2].SetActive(true);
        TV.SetActive(false);
        GetComponent<Selecttime>().selection.SetActive(false);
        GetComponent<Selecttime>().s1active = false;
        GetComponent<Selecttime>().player.GetComponent<PlayerMove>().canmove = true;
        councelbar.SetActive(true);
    }


}
