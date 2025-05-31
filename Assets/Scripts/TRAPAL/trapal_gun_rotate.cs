using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_gun_rotate : MonoBehaviour
{
    public Transform target;        
    public float rotationSpeed = 180f; 
    public float duration = 5f; 
    private float timer = 0f;
    public bool shoot = false;

    void FixedUpdate()
    {
        if (!shoot)
        {
            Vector2 direction = target.position - transform.position;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

            float currentAngle = transform.eulerAngles.z;

            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0f, 0f, newAngle);

            if (timer < duration)
            {
                timer += Time.deltaTime;
                float t = Mathf.Clamp01(timer / duration);
                rotationSpeed = Mathf.Lerp(0f, 100f, t);
            }
        }
        
    }
}
