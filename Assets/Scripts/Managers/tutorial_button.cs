using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_button : MonoBehaviour
{
    public GameObject[] uis;
    public int index;

    public bool timescale1;

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
            if (timescale1)
            {
                battalemanager.Instance.gameObject.GetComponent<PauseManager>().ispause = false;
                Time.timeScale = 1;
            }
            transform.parent.gameObject.SetActive(false);
        }
        
    }
}
