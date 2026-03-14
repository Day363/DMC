using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class stencilequelwithparent : MonoBehaviour
{
    public void SetMaterial()
    {
        float stencil = transform.parent.GetComponent<Image>().material.GetFloat("_Stencil");

        if (TryGetComponent<Image>(out Image ig))
        {
            ig.material = new Material(ig.material);
            ig.material.SetFloat("_Stencil", stencil);
        }
        else if (TryGetComponent<TMP_Text>(out TMP_Text text))
        {
            Material mat = new Material(text.fontMaterial);
            text.fontMaterial = mat;

            mat.SetFloat("_Stencil", stencil);
            mat.SetFloat("_StencilComp", (float)CompareFunction.NotEqual);
            mat.SetFloat("_StencilReadMask", 255);
            mat.SetFloat("_StencilWriteMask", 255);
        }
    }
}
