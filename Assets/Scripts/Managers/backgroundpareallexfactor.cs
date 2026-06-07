using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class backgroundpareallexfactor : MonoBehaviour
{
    public float factorStep = 0.1f;

    [ContextMenu("Update Below Objects")]
    public void UpdateBelowObjects()
    {
        ParallaxLayer myLayer = GetComponent<ParallaxLayer>();

        if (myLayer == null)
            return;

        Transform parent = transform.parent;

        if (parent == null)
            return;

        int myIndex = transform.GetSiblingIndex();

        for (int i = 1; i <= 9; i++)
        {
            int targetIndex = myIndex + i;

            if (targetIndex >= parent.childCount)
                break;

            ParallaxLayer target = parent.GetChild(targetIndex).GetComponent<ParallaxLayer>();

            if (target != null)
            {
                target.parallaxFactor = myLayer.parallaxFactor + factorStep * i;
            }
        }
    }
}
