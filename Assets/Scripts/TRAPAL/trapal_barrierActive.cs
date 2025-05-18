using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_barrierActive : MonoBehaviour
{
    public GameObject[] barriers;
    public float waittime;

    public void ActiveCo()
    {
        StartCoroutine(Active());
    }

    public IEnumerator Active()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].SetActive(true);
            barriers[i].GetComponent<trapal_barrierlightup>().StartCo();
            yield return new WaitForSeconds(waittime);
        }
    }
}
