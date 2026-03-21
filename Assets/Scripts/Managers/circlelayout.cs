using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class circlelayout : MonoBehaviour
{
    public bool lookright;
    public float rotateSpeed = 200f;
    public float radius = 3f;
    public bool notequel;
    public float startAngle;
    public float angleStep;

    public void Update()
    {
        float step = 0;

        foreach (Transform child in transform)
        {
            Image img = child.GetComponent<Image>();

            float angle = transform.parent.GetComponent<rullet>().totalAngle + step;

            step += angleStep;

            if (img.material == null || img.material.name.Contains("(Instance)") == false)
            {
                img.material = new Material(img.material);
            }

            if (angle > 0)
            {
                img.material.SetFloat("_Stencil", -1);
            }
            else if (angle < -180)
            {
                img.material.SetFloat("_Stencil", 1);
            }
            else
            {
                img.material.SetFloat("_Stencil", 0);
            }
             
            if (angle < -360 || angle > 180)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }

            foreach (Transform child2 in child)
            {
                if (child2.TryGetComponent<Image>(out Image ig) || child2.TryGetComponent<TMP_Text>(out TMP_Text tmp))
                {
                    child2.GetComponent<stencilequelwithparent>().SetMaterial();
                }
            }
        }
    }

    public void Arrange()
    {
        int count = transform.childCount;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);

            float angle = angleStep * i * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );

            child.localPosition = pos;

            Vector2 dir = pos.normalized;
            child.up = dir;
            if (lookright)
            {
                child.right = dir;
            }
        }
    }
    
    public void ArrangeNotEquel()
    {
        int count = transform.childCount;
        Debug.Log("awrw");
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);

            float angle = (startAngle + angleStep * i) * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );

            child.localPosition = pos;

            Vector2 direction = child.position - transform.position;
            float angle2 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            child.rotation = Quaternion.Euler(0f, 0f, angle2);
        }
    }
}
