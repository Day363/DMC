using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ReedRandomSeed : MonoBehaviour
{
    void Start()
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.GetPropertyBlock(block);
        block.SetFloat("_RandomSeed", Random.Range(0f, 100f));
        sr.SetPropertyBlock(block);
    }
}
