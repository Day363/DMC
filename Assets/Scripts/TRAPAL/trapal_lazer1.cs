using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_lazer1 : MonoBehaviour
{
    public Transform player;           // 따라볼 대상 (플레이어)
    public float rotationSpeed = 2f;   // 회전 속도
    public bool rotate = true;

    public void Start()
    {
        StartCoroutine(Delay());
    }

    void FixedUpdate()
    {
        if (rotate)
        {
            Vector2 direction = player.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float currentAngle = transform.eulerAngles.z;
            float angle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed);

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }

    public void RotaeStop()
    {
        rotate = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Animator>().SetBool("shoot", true); ;
    }
}
