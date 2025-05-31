using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killprefab : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
