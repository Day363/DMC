using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SortingGroupAnimation : MonoBehaviour
{
    public SortingGroup[] sortingGroup;
    public int orderInLayer;

    private void LateUpdate()
    {
        foreach (SortingGroup group in sortingGroup)
        {
            group.sortingOrder = orderInLayer;
        }
    }
}
