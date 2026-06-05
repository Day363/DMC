using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class windeffectmesh : MonoBehaviour
{
    public int segments = 48;

    public float bottomRadius = 0.7f;
    public float topRadius = 1.6f;
    public float height = 0.8f;

    public float noiseStrength = 0.15f;
    public float noiseScale = 3f;

    MeshFilter mf;

    void OnValidate()
    {
        if (segments < 3) segments = 3;

        if (mf == null)
            mf = GetComponent<MeshFilter>();

        Generate();
    }

    void Awake()
    {
        mf = GetComponent<MeshFilter>();
        Generate();
    }

    void Generate()
    {
        Mesh mesh = new Mesh();
        mesh.name = "WindCone";

        Vector3[] verts = new Vector3[(segments + 1) * 2];
        Vector2[] uv = new Vector2[verts.Length];
        int[] tris = new int[segments * 6];

        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            float ang = t * Mathf.PI * 2f;

            float c = Mathf.Cos(ang);
            float s = Mathf.Sin(ang);

            Vector2 dir = new Vector2(c, s);

            float noise = Mathf.PerlinNoise(
                c * noiseScale + 100,
                s * noiseScale + 100
            );

            float noisyTop =
                topRadius +
                (noise - 0.5f) * 2f * noiseStrength;

            verts[i * 2] =
                new Vector3(
                    dir.x * bottomRadius,
                    0,
                    dir.y * bottomRadius
                );

            verts[i * 2 + 1] =
                new Vector3(
                    dir.x * noisyTop,
                    height,
                    dir.y * noisyTop
                );

            uv[i * 2] = new Vector2(t, 0);
            uv[i * 2 + 1] = new Vector2(t, 1);
        }

        int ti = 0;

        for (int i = 0; i < segments; i++)
        {
            int b0 = i * 2;
            int t0 = i * 2 + 1;
            int b1 = (i + 1) * 2;
            int t1 = (i + 1) * 2 + 1;

            tris[ti++] = b0;
            tris[ti++] = t0;
            tris[ti++] = b1;

            tris[ti++] = t0;
            tris[ti++] = t1;
            tris[ti++] = b1;
        }

        mesh.vertices = verts;
        mesh.uv = uv;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        mf.sharedMesh = mesh;
    }
}
