using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layouttext : MonoBehaviour
{
    
    public float spacing = 1.5f;

    public void LayOut()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.localPosition = new Vector3(i * spacing, 0, 0);
        }
    }
    
}
