using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_eyeflash : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Destroy());
    }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }    
}
