using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_button : MonoBehaviour
{
    public GameObject[] uis;
    public int index;

    public void Next()
    {
        if (index != uis.Length)
        {
            foreach (GameObject ui in uis)
            {
                ui.SetActive(false);
            }
            uis[index].SetActive(true);
            index += 1;
        }
        else
        {
            transform.parent.gameObject.SetActive(false);
        }
        
    }
}
