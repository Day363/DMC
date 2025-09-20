using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentscaleignore : MonoBehaviour
{
    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.lossyScale; // 월드 기준 스케일 기억
    }

    void LateUpdate()
    {
        // 부모가 스케일을 바꿔도 항상 같은 월드 스케일 유지
        Vector3 parentScale = transform.parent != null ? transform.parent.lossyScale : Vector3.one;
        transform.localScale = new Vector3(
            originalScale.x / parentScale.x,
            originalScale.y / parentScale.y,
            originalScale.z / parentScale.z
        );
    }
}
