using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curser : MonoBehaviour
{
    public Camera mainCamera;
    public float delay = 0.5f; // 지연 시간 (초)
    public float moveSpeed = 20f; // 따라가는 속도
    public float zDepth = 10f; // 카메라에서의 깊이 (3D 오브젝트용)

    public GameObject skill;
    public GameObject defense;

    public float side1;
    public float height1;
    public float side2;
    public float height2;

    private Queue<Vector3> positionQueue = new Queue<Vector3>();
    private float timer;

    void Update()
    {
        if (skill != null && defense != null)
        {
            Vector3 skillpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + side1, transform.position.y + height1, 0));
            skill.transform.position = skillpos;

            Vector3 defensepos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + side2, transform.position.y + height2, 0));
            defense.transform.position = defensepos;
        }
        

        

        timer += Time.unscaledDeltaTime;

        // 현재 마우스 위치 저장
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = zDepth;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        positionQueue.Enqueue(worldPos);

        // 큐에서 delay만큼 지난 위치 꺼내기
        if (timer >= delay)
        {
            Vector3 targetPos = positionQueue.Dequeue();

            // 부드럽게 이동
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.unscaledDeltaTime * moveSpeed);
        }
    }
}
