using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlelayout : MonoBehaviour
{
    public bool lookright;
    public float rotateSpeed = 200f;
    public float radius = 3f;
    public bool notequel;
    public float startAngle;
    public float angleStep;

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
