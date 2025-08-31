using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_deny_eye : MonoBehaviour
{
    public Transform centerObject;     // 중심이 되는 오브젝트
    public float radius = 5f;          // 원의 반지름
    public Transform player;           // 플레이어
    public float moveSpeed = 3f;       // 이동 속도

    void Update()
    {
        if (centerObject == null || player == null) return;

        // 현재 오브젝트의 위치
        Vector2 currentPos = transform.position;

        // 목표 위치는 플레이어 방향으로 이동하되, 원 범위 내로 제한
        Vector2 toPlayer = (player.position - centerObject.position).normalized;

        // 원하는 목표 위치 (플레이어 방향으로 약간 이동)
        Vector2 desiredPosition = (Vector2)transform.position + toPlayer * moveSpeed * Time.deltaTime;

        // 중심으로부터 거리 계산
        Vector2 offsetFromCenter = desiredPosition - (Vector2)centerObject.position;

        // 원의 반지름 안에 있는지 체크
        if (offsetFromCenter.magnitude > radius)
        {
            // 원의 테두리 위로 위치를 조정
            offsetFromCenter = offsetFromCenter.normalized * radius;
            desiredPosition = (Vector2)centerObject.position + offsetFromCenter;
        }

        // 실제 이동
        transform.position = desiredPosition;
    }
}
