using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashline : MonoBehaviour
{
    public Transform player;                  // 선 시작점
    public float assistRadius = 2.0f;         // 마우스 주변 감지 범위
    public Camera mainCamera;
    public Sprite dot;
    public Sprite target;
    public bool nowtargetting = false;
    public Transform targetenemy;


    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (player == null || mainCamera == null) return;

        // 마우스 위치 (월드 좌표)
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(mainCamera.transform.position.z); // 2D용 깊이 보정
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        // 마우스 주변 감지
        Collider2D[] hits = Physics2D.OverlapCircleAll(mouseWorldPos, assistRadius);

        Transform nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("client") || hit.CompareTag("normalenemy"))
            {
                float dist = Vector2.Distance(mouseWorldPos, hit.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    nearestEnemy = hit.transform;
                    targetenemy = nearestEnemy;
                    GetComponent<SpriteRenderer>().sprite = target;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = dot;
                }
            }
        }



        // 위치 업데이트
        Vector3 targetPos = nearestEnemy != null ? nearestEnemy.position : mouseWorldPos;
        transform.position = targetPos;

        if (nearestEnemy != null)
        {
            // 적을 타겟
            targetPos = nearestEnemy.position;
            if (target != null) GetComponent<SpriteRenderer>().sprite = target;
            nowtargetting = true;
        }
        else
        {
            // 마우스를 타겟
            targetPos = mouseWorldPos;
            if (dot != null) GetComponent<SpriteRenderer>().sprite = dot;
            nowtargetting = false;
        }

        // 라인 렌더링 (플레이어 → dashline)
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    // 디버그: 감지 범위 시각화
    private void OnDrawGizmosSelected()
    {
        if (mainCamera == null) return;

        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(mainCamera.transform.position.z);
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mouseWorldPos, assistRadius);
    }
}
