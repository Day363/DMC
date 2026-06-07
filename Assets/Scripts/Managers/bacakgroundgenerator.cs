using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class bacakgroundgenerator : MonoBehaviour
{
    [ContextMenu("Generate")]
    public void Generate()
    {
        SpriteRenderer original = GetComponent<SpriteRenderer>();

        if (original == null)
        {
            Debug.LogError("SpriteRenderer가 필요합니다.");
            return;
        }

        for (int i = 1; i <= 9; i++)
        {
            GameObject obj = new GameObject($"{gameObject.name}_{i}");

            obj.transform.SetParent(transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;

            SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();

            sr.sprite = original.sprite;
            sr.material = original.sharedMaterial;
            sr.sortingLayerID = original.sortingLayerID;
            sr.sortingOrder = original.sortingOrder - i;

            Color c = original.color;

            float add = (16f * i) / 255f;

            sr.color = new Color(
                Mathf.Clamp01(c.r + add),
                Mathf.Clamp01(c.g + add),
                Mathf.Clamp01(c.b + add),
                c.a
            );
        }
    }
}
