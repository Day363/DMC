using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror_random : MonoBehaviour
{
    public List<GameObject> available_mirror = new List<GameObject> { };

    public void Start()
    {
        available_mirror = new List<GameObject>(battalemanager.Instance.available_mirror);
        SetRandom();
    }

    public void SetRandom()
    {
        Transform pos1 = transform.GetChild(0);
        Transform pos2 = transform.GetChild(1);
        Transform pos3 = transform.GetChild(2);

        int ran1 = Random.Range(0, available_mirror.Count);
        GameObject currentmirror1 = Instantiate(available_mirror[ran1], pos1);
        currentmirror1.transform.localPosition = new Vector3(0, 0, 0);
        available_mirror.RemoveAt(ran1);
        int ran2 = Random.Range(0, available_mirror.Count);
        GameObject currentmirror2 = Instantiate(available_mirror[ran2], pos2);
        currentmirror2.transform.localPosition = new Vector3(0, 0, 0);
        available_mirror.RemoveAt(ran2);
        int ran3 = Random.Range(0, available_mirror.Count);
        GameObject currentmirror3 = Instantiate(available_mirror[ran3], pos3);
        currentmirror3.transform.localPosition = new Vector3(0, 0, 0);
        available_mirror.RemoveAt(ran3);
    }
}
