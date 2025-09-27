using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMappedRatio : MonoBehaviour
{
    public Transform cameraTransform; // 따라갈 카메라
    public float minX = -10f; // 카메라 이동 최소
    public float maxX = 10f;  // 카메라 이동 최대
    [Range(0f, 1f)]
    public float parallaxRatio = 1f; // 0~1: 배경 이동 비율

    private Vector3 leftPos;  // 배경 왼쪽 끝
    private Vector3 rightPos; // 배경 오른쪽 끝

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("SpriteRenderer가 필요합니다.");
            return;
        }

        // 배경 이동 범위를 월드 좌표로 계산
        leftPos = transform.position; // 초기 위치 = 왼쪽 끝
        rightPos = leftPos + new Vector3(sr.bounds.size.x, 0, 0); // 오른쪽 끝
    }

    void LateUpdate()
    {
        // 카메라 이동 비율 (0~1)
        float t = Mathf.InverseLerp(minX, maxX, cameraTransform.position.x);

        // parallaxRatio 적용
        float ratioT = t * parallaxRatio;

        // 배경 위치 보간
        Vector3 newPos = Vector3.Lerp(leftPos, rightPos, ratioT);
        transform.position = newPos;
    }
}
