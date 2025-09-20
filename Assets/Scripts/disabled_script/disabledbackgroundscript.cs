using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledbackgroundscript : MonoBehaviour
{
    public Transform cameraTransform; // 시네머신 카메라
    public float parallaxFactor = 0.5f; // 원근 정도 (0 = 고정, 1 = 카메라와 동일)
    public float smoothSpeed = 100f; // 부드러움 정도

    private Vector3 previousCameraPosition;
    private Vector3 targetPosition;


    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        previousCameraPosition = cameraTransform.position;
        targetPosition = transform.position;
    }

    void Update()
    {
        Vector3 delta = cameraTransform.position - previousCameraPosition;
        targetPosition += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor * 0.3f, 0f);

        // MoveTowards로 프레임 독립적 부드러운 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        previousCameraPosition = cameraTransform.position;
    }
}
