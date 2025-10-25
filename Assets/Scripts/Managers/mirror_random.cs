using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror_random : MonoBehaviour
{
    public GameObject[] mirrors_original;

    public List<GameObject> available_mirror;

    public void Start()
    {
        foreach (GameObject mirror in mirrors_original)
        {
            available_mirror.Add(mirror);
        }
        SetRandom();
    }

    public void SetRandom()
    {
        Transform pos1 = transform.GetChild(0);
        Transform pos2 = transform.GetChild(1);
        Transform pos3 = transform.GetChild(2);

        if (pos1.childCount > 0)
        {
            pos1.GetChild(0).gameObject.SetActive(false);
        }
        if (pos2.childCount > 0)
        {
            pos2.GetChild(0).gameObject.SetActive(false);
        }
        if (pos3.childCount > 0)
        {
            pos3.GetChild(0).gameObject.SetActive(false);
        }


        int ran1 = Random.Range(0, available_mirror.Count);
        available_mirror[ran1].SetActive(true);
        available_mirror[ran1].transform.position = pos1.transform.position;
        available_mirror.RemoveAt(ran1);
        int ran2 = Random.Range(0, available_mirror.Count);
        available_mirror[ran2].SetActive(true);
        available_mirror[ran2].transform.position = pos2.transform.position;
        available_mirror.RemoveAt(ran2);
        int ran3 = Random.Range(0, available_mirror.Count);
        available_mirror[ran3].SetActive(true);
        available_mirror[ran3].transform.position = pos3.transform.position;
        available_mirror.RemoveAt(ran3);
    }
}
