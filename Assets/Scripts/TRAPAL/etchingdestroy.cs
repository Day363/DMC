using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class etchingdestroy : MonoBehaviour
{
    public GameObject particle;

    public void DestroySelf()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
