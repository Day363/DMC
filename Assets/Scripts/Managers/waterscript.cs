using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class waterscript : MonoBehaviour
{
    [Header("Shape")]
    [Min(2)] public int pointCount = 64;
    public float width = 20f;
    public float depth = 5f;

    [Header("Wave")]
    public float springStrength = 0.02f;
    public float damping = 0.04f;
    public float spread = 0.05f;
    public int spreadIterations = 8;

    [Header("Interaction")]
    public string playerTag = "Player";

    [Tooltip("물 진입 시 파동")]
    public float enterSplashMultiplier = 0.5f;

    [Tooltip("물속 이동 시 파동")]
    public float moveSplashMultiplier = 0.005f;

    private readonly HashSet<Rigidbody2D> bodiesInWater = new();

    float[] heights;
    float[] velocities;
    float[] leftDeltas;
    float[] rightDeltas;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    int cachedPointCount;

    void OnEnable()
    {
        Initialize();
    }

    void OnValidate()
    {
        Initialize();
    }

    void Initialize()
    {
        pointCount = Mathf.Max(2, pointCount);

        if (heights == null || heights.Length != pointCount)
        {
            heights = new float[pointCount];
            velocities = new float[pointCount];
            leftDeltas = new float[pointCount];
            rightDeltas = new float[pointCount];
        }

        if (mesh == null)
        {
            MeshFilter mf = GetComponent<MeshFilter>();

            if (Application.isPlaying)
            {
                mesh = mf.mesh;
            }
            else
            {
                mesh = new Mesh();
                mesh.name = "Water";
                mf.sharedMesh = mesh;
            }
        }

        CreateMesh();
    }

    void Update()
    {
        if (cachedPointCount != pointCount)
        {
            cachedPointCount = pointCount;
            Initialize();
        }

        if (mesh == null || vertices == null)
        {
            Initialize();
            return;
        }

        if (Application.isPlaying)
        {
            Simulate();
        }

        UpdateMesh();
    }

    void CreateMesh()
    {
        if (mesh == null)
            return;

        vertices = new Vector3[pointCount * 2];
        uvs = new Vector2[pointCount * 2];
        triangles = new int[(pointCount - 1) * 6];

        float spacing = width / (pointCount - 1);

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * spacing - width * 0.5f;

            vertices[i * 2] = new Vector3(x, 0f, 0f);
            vertices[i * 2 + 1] = new Vector3(x, -depth, 0f);

            float u = (float)i / (pointCount - 1);

            uvs[i * 2] = new Vector2(u, 1);
            uvs[i * 2 + 1] = new Vector2(u, 0);
        }

        int t = 0;

        for (int i = 0; i < pointCount - 1; i++)
        {
            int tl = i * 2;
            int bl = i * 2 + 1;
            int tr = i * 2 + 2;
            int br = i * 2 + 3;

            triangles[t++] = tl;
            triangles[t++] = tr;
            triangles[t++] = bl;

            triangles[t++] = tr;
            triangles[t++] = br;
            triangles[t++] = bl;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    void Simulate()
    {
        for (int i = 0; i < pointCount; i++)
        {
            float force = -heights[i] * springStrength;

            velocities[i] += force;
            velocities[i] *= (1f - damping);

            heights[i] += velocities[i];
        }

        for (int j = 0; j < spreadIterations; j++)
        {
            for (int i = 0; i < pointCount; i++)
            {
                if (i > 0)
                {
                    leftDeltas[i] = spread * (heights[i] - heights[i - 1]);
                    velocities[i - 1] += leftDeltas[i];
                }

                if (i < pointCount - 1)
                {
                    rightDeltas[i] = spread * (heights[i] - heights[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
            }

            for (int i = 0; i < pointCount; i++)
            {
                if (i > 0)
                    heights[i - 1] += leftDeltas[i];

                if (i < pointCount - 1)
                    heights[i + 1] += rightDeltas[i];
            }
        }
    }

    void UpdateMesh()
    {
        float spacing = width / (pointCount - 1);

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * spacing - width * 0.5f;

            vertices[i * 2] = new Vector3(x, heights[i], 0f);
            vertices[i * 2 + 1] = new Vector3(x, -depth, 0f);
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }

    public void Splash(Vector3 worldPosition, float force)
    {
        Vector3 local = transform.InverseTransformPoint(worldPosition);

        float normalizedX = (local.x + width * 0.5f) / width;

        int center = Mathf.RoundToInt(normalizedX * (pointCount - 1));

        for (int i = -2; i <= 2; i++)
        {
            int index = center + i;

            if (index < 0 || index >= pointCount)
                continue;

            float falloff = 1f - Mathf.Abs(i) * 0.25f;

            velocities[index] += force * falloff;
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        Rigidbody2D rb = other.attachedRigidbody;

        if (rb == null)
            return;

        bodiesInWater.Add(rb);

        float impact = Mathf.Abs(rb.velocity.y) * enterSplashMultiplier;

        if (impact > 0.1f)
        {
            Splash(other.bounds.center, -impact);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        Rigidbody2D rb = other.attachedRigidbody;

        if (rb == null)
            return;

        float moveSpeed = Mathf.Abs(rb.velocity.x);

        if (moveSpeed < 0.5f)
            return;

        if (Time.frameCount % 5 == 0)
        {
            Splash(
                other.bounds.center,
                -moveSpeed * moveSplashMultiplier
            );
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;

        if (rb != null)
        {
            bodiesInWater.Remove(rb);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 lt = transform.TransformPoint(new Vector3(-width * 0.5f, 0, 0));
        Vector3 rt = transform.TransformPoint(new Vector3(width * 0.5f, 0, 0));

        Vector3 lb = transform.TransformPoint(new Vector3(-width * 0.5f, -depth, 0));
        Vector3 rb = transform.TransformPoint(new Vector3(width * 0.5f, -depth, 0));

        Gizmos.DrawLine(lt, rt);
        Gizmos.DrawLine(lt, lb);
        Gizmos.DrawLine(rt, rb);
        Gizmos.DrawLine(lb, rb);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        float spacing = width / (pointCount - 1);

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * spacing - width * 0.5f;

            Vector3 pos = transform.TransformPoint(new Vector3(x, 0, 0));

            Gizmos.DrawSphere(pos, 0.05f);
        }
    }

    public float GetHeight(int index)
    {
        index = Mathf.Clamp(index, 0, pointCount - 1);
        return heights[index];
    }

    public float SampleHeight(float normalizedX)
    {
        normalizedX = Mathf.Clamp01(normalizedX);

        float fIndex = normalizedX * (pointCount - 1);

        int a = Mathf.FloorToInt(fIndex);
        int b = Mathf.Min(a + 1, pointCount - 1);

        float t = fIndex - a;

        return Mathf.Lerp(
            heights[a],
            heights[b],
            t
        );
    }

    public void AddVelocityAtNormalized(float normalizedX, float force)
    {
        normalizedX = Mathf.Clamp01(normalizedX);

        int center = Mathf.RoundToInt(normalizedX * (pointCount - 1));

        for (int i = -2; i <= 2; i++)
        {
            int index = center + i;

            if (index < 0 || index >= pointCount)
                continue;

            float falloff = 1f - Mathf.Abs(i) * 0.25f;

            velocities[index] += force * falloff;
        }
    }
}
