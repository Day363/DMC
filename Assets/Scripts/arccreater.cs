using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class arccreater : MonoBehaviour
{
    public float innerRadius = 0.5f;
    public float outerRadius = 1.0f;
    [Range(0, 360)]
    public float angle = 180f;
    public int segments = 30;

    MeshFilter meshFilter;

    void Awake()
    {
        Init();
        Generate();
    }

    void OnEnable()
    {
        Init();
        Generate();
    }

    void OnValidate()
    {
        Init();
        Generate();
    }

    void Init()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter.sharedMesh == null)
        {
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.name = "Arc Mesh";
        }
    }

    void Generate()
    {
        if (segments < 2) segments = 2;
        if (innerRadius < 0) innerRadius = 0;
        if (outerRadius <= innerRadius)
            outerRadius = innerRadius + 0.01f;

        Mesh mesh = meshFilter.sharedMesh;
        mesh.Clear();

        int vertCount = (segments + 1) * 2;

        Vector3[] vertices = new Vector3[vertCount];
        Vector2[] uv = new Vector2[vertCount];
        int[] triangles = new int[segments * 6];

        float step = angle / segments;

        for (int i = 0; i <= segments; i++)
        {
            float a =
                Mathf.Deg2Rad *
                (i * step - angle * 0.5f);

            float cos = Mathf.Cos(a);
            float sin = Mathf.Sin(a);

            // Outer
            vertices[i * 2] =
                new Vector3(
                    cos * outerRadius,
                    sin * outerRadius,
                    0);

            // Inner
            vertices[i * 2 + 1] =
                new Vector3(
                    cos * innerRadius,
                    sin * innerRadius,
                    0);

            float t = (float)i / segments;

            uv[i * 2] = new Vector2(t, 1);
            uv[i * 2 + 1] = new Vector2(t, 0);
        }

        int tri = 0;

        for (int i = 0; i < segments; i++)
        {
            int start = i * 2;

            triangles[tri++] = start;
            triangles[tri++] = start + 2;
            triangles[tri++] = start + 1;

            triangles[tri++] = start + 1;
            triangles[tri++] = start + 2;
            triangles[tri++] = start + 3;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    void OnDrawGizmosSelected()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null || mf.sharedMesh == null)
            return;

        Gizmos.color = Color.cyan;

        Vector3[] verts = mf.sharedMesh.vertices;
        int[] tris = mf.sharedMesh.triangles;

        for (int i = 0; i < tris.Length; i += 3)
        {
            Vector3 a =
                transform.TransformPoint(
                    verts[tris[i]]);
            Vector3 b =
                transform.TransformPoint(
                    verts[tris[i + 1]]);
            Vector3 c =
                transform.TransformPoint(
                    verts[tris[i + 2]]);

            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, a);
        }
    }
}
