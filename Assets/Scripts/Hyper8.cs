using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Hyper8 : MonoBehaviour
{
    [Header("Controls")]
    [Range(0, 1)] public float hyperRotation = 0.3f;
    [Range(-1, 1)] public float slice = 0f;
    [Range(0, 1)] public float morph = 0.5f;
    [Range(0, 1)] public float symmetry = 0.8f;

    public float radius = 2f;
    public int resolution = 64;

    Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        mesh.name = "Hyper8Ramiel";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        BuildMesh();
    }

    void BuildMesh()
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        float t = Time.time * hyperRotation * 2f;

        for (int i = 0; i < resolution; i++)
        {
            float u = i / (float)(resolution - 1);

            for (int j = 0; j < resolution; j++)
            {
                float v = j / (float)(resolution - 1);

                float theta = u * Mathf.PI;
                float phi = v * Mathf.PI * 2f;

                Vector3 p = SampleHyperPoint(theta, phi, t);

                verts.Add(p);
            }
        }

        for (int y = 0; y < resolution - 1; y++)
        {
            for (int x = 0; x < resolution - 1; x++)
            {
                int i0 = y * resolution + x;
                int i1 = i0 + 1;
                int i2 = i0 + resolution;
                int i3 = i2 + 1;

                tris.Add(i0);
                tris.Add(i2);
                tris.Add(i1);

                tris.Add(i1);
                tris.Add(i2);
                tris.Add(i3);
            }
        }

        mesh.Clear();
        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        mesh.RecalculateNormals();
    }

    Vector3 SampleHyperPoint(float theta, float phi, float t)
    {
        float x = Mathf.Sin(theta) * Mathf.Cos(phi);
        float y = Mathf.Cos(theta);
        float z = Mathf.Sin(theta) * Mathf.Sin(phi);

        float[] d = new float[8];

        d[0] = x;
        d[1] = y;
        d[2] = z;

        d[3] = Mathf.Sin(theta * 2 + t);
        d[4] = Mathf.Cos(phi * 2 + t * 0.7f);
        d[5] = Mathf.Sin(phi * 3 + t * 1.3f);
        d[6] = slice;
        d[7] = Mathf.Cos(theta * 4 + t);

        Rotate(d, 0, 3, t);
        Rotate(d, 1, 4, t * 0.7f);
        Rotate(d, 2, 5, t * 1.1f);
        Rotate(d, 6, 7, t * 0.5f);

        float scale = 1f;

        for (int k = 7; k >= 3; k--)
        {
            float s = 2.2f / (2.2f - d[k] * 0.9f);
            scale *= s;
        }

        Vector3 p = new Vector3(
            d[0],
            d[1],
            d[2]
        ) * scale;

        float cube =
            Mathf.Max(
                Mathf.Abs(p.x),
                Mathf.Abs(p.y),
                Mathf.Abs(p.z));

        float diamond =
            Mathf.Abs(p.x) +
            Mathf.Abs(p.y) +
            Mathf.Abs(p.z);

        float crystal =
            Mathf.Pow(
                Mathf.Abs(p.x * p.y * p.z),
                0.35f);

        float shape = Mathf.Lerp(cube, diamond, morph * 0.3f); // cube 비중 높임
        shape = Mathf.Lerp(shape, Mathf.Round(shape * 12f) / 12f, symmetry); // 면 수 2배
        p = p.normalized * radius * (1f + shape * 0.5f); // 돌출 강도 증가

        return p;
    }

    void Rotate(float[] p, int a, int b, float ang)
    {
        float c = Mathf.Cos(ang);
        float s = Mathf.Sin(ang);

        float x = p[a] * c - p[b] * s;
        float y = p[a] * s + p[b] * c;

        p[a] = x;
        p[b] = y;
    }
}
