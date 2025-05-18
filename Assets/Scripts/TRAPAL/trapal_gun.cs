using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_gun : MonoBehaviour
{
    public float radius = 5f;                  // 반지름
    public float moveDuration = 2f;            // 이동 시간 (초)
    public Transform player;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime = 0f;
    private bool moving = true;
    private Quaternion startRotation;

    void Start()
    {
        // 상단 반원에서 랜덤 각도
        float angle = Random.Range(0f, 180f) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;

        startPosition = transform.position;
        targetPosition = player.position + offset;

        Vector2 toPlayer = (player.position - transform.position).normalized;
        Vector2 awayFromPlayer = -toPlayer;
        float awayAngle = Mathf.Atan2(awayFromPlayer.y, awayFromPlayer.x) * Mathf.Rad2Deg + 90;
        startRotation = Quaternion.Euler(0f, 0f, awayAngle);

        transform.rotation = startRotation;
    }

    void Update()
    {
        if (!moving) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / moveDuration);

        // 감속 곡선 (Ease Out: 빠르게 시작해서 느리게 끝남)
        float curvedT = 1f - Mathf.Pow(1f - t, 2f); // 감속(2차) 커브

        transform.position = Vector3.Lerp(startPosition, targetPosition, curvedT);

        if (t >= 1f)
        {
            moving = false;
            OnArrival();
        }
    }

    void OnArrival()
    {
        Debug.Log("지정된 시간에 감속하며 도착 완료");
    }
}
