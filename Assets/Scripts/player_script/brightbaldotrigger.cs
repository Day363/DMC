using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brightbaldotrigger : MonoBehaviour
{
    public GameObject baldo;

    private void Update()
    {
        baldo.GetComponent<Animator>().SetBool("baldo", true);
        gameObject.SetActive(false);
    }
}
