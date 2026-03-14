using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rullet : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public RectTransform center;
    public Transform target;

    public float prevAngle;
    public float totalAngle;

    public void OnBeginDrag(PointerEventData eventData)
    {
        prevAngle = GetAngle(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float currentAngle = GetAngle(eventData.position);

        float delta = Mathf.DeltaAngle(prevAngle, currentAngle);

        totalAngle += delta;

        target.rotation = Quaternion.Euler(0, 0, totalAngle);

        prevAngle = currentAngle;

    }

    float GetAngle(Vector2 screenPos)
    {
        Vector2 centerScreen = RectTransformUtility.WorldToScreenPoint(null, center.position);

        Vector2 dir = screenPos - centerScreen;

        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
