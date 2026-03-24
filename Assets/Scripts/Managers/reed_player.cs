using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reed_player : MonoBehaviour
{
    public Material mat;
    public Color color1;

    void Start()
    {
        GetComponent<SpriteRenderer>().material.SetColor("_Color", color1);
    }

}
