using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_trapal_weapon_arrey : MonoBehaviour
{
    public int count = 3;

    public float radius = 7f; // 중심에서 떨어진 거리
    public float angle;

    public GameObject target;
    public float movespeed;

    public float x;
    public float y;

    public void Start()
    {
        target = battalemanager.Instance.player;
    }

    public void FixedUpdate()
    {
        angle = angle + 1f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.position = new Vector3(target.transform.position.x + x, target.transform.position.y + y, 0);

    }

    public void ArrangeExistingChildren()
    {
        int childCount = transform.childCount;
        if (childCount == 0) return;

        float angleStep = 360f / count;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // 각도 계산 (도 → 라디안)
            float angle = i * angleStep * Mathf.Deg2Rad;

            // 위치 계산
            Vector3 localPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            child.localPosition = localPos;

            // 바깥 방향으로 회전 설정
            Vector3 dir = (child.position - transform.position).normalized;
            float angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            child.rotation = Quaternion.Euler(0, 0, angleZ);
        }
    }

}
