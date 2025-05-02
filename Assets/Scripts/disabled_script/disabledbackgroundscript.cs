using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabledbackgroundscript : MonoBehaviour
{
    public Transform cameraTransform; // 시네머신 카메라를 따라갈 카메라 Transform
    public float parallaxFactor = 0.5f; // 원근 정도 (0 = 고정, 1 = 카메라와 동일)

    private Vector3 previousCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        previousCameraPosition = cameraTransform.position;
    }

    void FixedUpdate()
    {
        Vector3 delta = cameraTransform.position - previousCameraPosition;
        transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor * 0.3f, 0f);
        previousCameraPosition = cameraTransform.position;
    }
}
