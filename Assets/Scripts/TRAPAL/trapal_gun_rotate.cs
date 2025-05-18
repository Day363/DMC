using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_gun_rotate : MonoBehaviour
{
    public Transform target;
    public float rotationDuration = 1f;

    private float rotationTimeElapsed = 0f;
    private float startAngle;
    private float targetAngle;
    private bool isRotating = false;

    private void Start()
    {
        StartRotation();
    }

    void Update()
    {
        if (isRotating)
        {
            rotationTimeElapsed += Time.deltaTime;
            float t = rotationTimeElapsed / rotationDuration;
            t = Mathf.Clamp01(t);

            // Ease-out 보간: 빠르게 시작해서 천천히 끝남
            float easedT = 1f - Mathf.Pow(1f - t, 2f); // Quadratic ease-out

            float angle = Mathf.LerpAngle(startAngle, targetAngle, easedT);
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (t >= 1f)
            {
                isRotating = false;
            }
        }
    }

    public void StartRotation()
    {
        Vector2 direction = target.position - transform.position;
        targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        startAngle = transform.eulerAngles.z;
        rotationTimeElapsed = 0f;
        isRotating = true;
    }
}
