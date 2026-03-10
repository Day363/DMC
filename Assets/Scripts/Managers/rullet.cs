using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rullet : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public RectTransform center;   // 원형 UI 중심
    public Transform target;       // 회전할 오브젝트

    float prevAngle;

    public void OnBeginDrag(PointerEventData eventData)
    {
        prevAngle = GetAngle(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float currentAngle = GetAngle(eventData.position);

        float delta = currentAngle - prevAngle;

        target.Rotate(0, 0, delta);

        prevAngle = currentAngle;
    }

    float GetAngle(Vector2 screenPos)
    {
        Vector2 dir = screenPos - RectTransformUtility.WorldToScreenPoint(null, center.position);

        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
