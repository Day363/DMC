using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterscript : MonoBehaviour
{
    public int resolution = 20;          // 물 표면 분할
    public float springConstant = 0.02f; // 탄성 계수
    public float damping = 0.04f;        // 감쇠
    public float spread = 0.05f;         // 파동 전파 정도
    public float width = 10f;            // 물 가로 길이
    public float height = 3f;            // 물 영역 높이 (Collider용 + Mesh 아래 영역)
    public float baseHeight = 0f;        // 기본 수면 높이
    public float splashForce = -5f;      // 충격 세기
    public float triggeroffset = 0;      // 충돌 위치 보정

    private float[] heights;
    private float[] velocities;
    private float[] accelerations;

    private LineRenderer line;
    private BoxCollider2D boxCollider;
    private MeshFilter meshFilter;
    private Mesh mesh;

    void Start()
    {
        heights = new float[resolution];
        velocities = new float[resolution];
        accelerations = new float[resolution];

        line = GetComponent<LineRenderer>();
        line.positionCount = resolution;

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;

        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        UpdateColliderSize();
    }

    void Update()
    {
        // 탄성 + 감쇠
        for (int i = 0; i < resolution; i++)
        {
            float force = springConstant * (baseHeight - heights[i]) - velocities[i] * damping;
            accelerations[i] = force;
            velocities[i] += accelerations[i];
            heights[i] += velocities[i];
        }

        // 파동 전파
        float[] leftDeltas = new float[resolution];
        float[] rightDeltas = new float[resolution];

        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < resolution; i++)
            {
                if (i > 0)
                {
                    leftDeltas[i] = spread * (heights[i] - heights[i - 1]);
                    velocities[i - 1] += leftDeltas[i];
                }
                if (i < resolution - 1)
                {
                    rightDeltas[i] = spread * (heights[i] - heights[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
            }
        }

        // 라인 & 메쉬 업데이트
        UpdateLineAndMesh();

        // Collider 갱신
        UpdateColliderSize();
    }

    // 외부에서 충격 주기
    public void Splash(int index, float velocity)
    {
        if (index >= 0 && index < resolution)
        {
            velocities[index] = velocity;
        }
    }

    // 플레이어 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float relativeX = other.transform.position.x - (transform.position.x - width / 2f) + triggeroffset;
            int index = Mathf.RoundToInt((relativeX / width) * (resolution - 1));
            Splash(index, splashForce);
        }
    }

    // BoxCollider 크기 자동 조정
    private void UpdateColliderSize()
    {
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(width, height);
            boxCollider.offset = new Vector2(0, baseHeight - height / 2f);
        }
    }

    // 라인과 메쉬 업데이트
    private void UpdateLineAndMesh()
    {
        float startX = transform.position.x - width / 2f;

        // 버텍스: 윗부분(수면) + 아랫부분(고정)
        Vector3[] vertices = new Vector3[resolution * 2];
        Vector2[] uvs = new Vector2[resolution * 2];
        int[] triangles = new int[(resolution - 1) * 6];

        for (int i = 0; i < resolution; i++)
        {
            float x = startX + (i / (float)(resolution - 1)) * width;
            float y = heights[i] + baseHeight + transform.position.y;

            // LineRenderer (수면)
            line.SetPosition(i, new Vector3(x, y, 0));

            // 수면 윗쪽 버텍스
            vertices[i] = new Vector3(x, y, 0);

            // 수면 아래쪽 버텍스
            vertices[i + resolution] = new Vector3(x, transform.position.y - height, 0);

            // UV 좌표 (0~1 범위로 정규화)
            float u = i / (float)(resolution - 1);
            uvs[i] = new Vector2(u, 1f);            // 윗부분
            uvs[i + resolution] = new Vector2(u, 0f); // 아랫부분

            // 사각형을 두 개의 삼각형으로 나누기
            if (i < resolution - 1)
            {
                int idx = i * 6;
                triangles[idx] = i;
                triangles[idx + 1] = i + 1;
                triangles[idx + 2] = i + resolution;

                triangles[idx + 3] = i + 1;
                triangles[idx + 4] = i + resolution + 1;
                triangles[idx + 5] = i + resolution;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;  //  UV 좌표 적용
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
