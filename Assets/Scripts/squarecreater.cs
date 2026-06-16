using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class squarecreater : MonoBehaviour
{
    public float size = 1f;

    void Start()
    {
        Generate();
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        Mesh mesh = new Mesh();

        float half = size * 0.5f;

        Vector3[] vertices =
        {
            new Vector3(-half,  half, 0), // 0 ÁÂ»ó
            new Vector3( half,  half, 0), // 1 ¿ì»ó
            new Vector3( half, -half, 0), // 2 ¿ìÇÏ
            new Vector3(-half, -half, 0)  // 3 ÁÂÇÏ
        };

        int[] triangles =
        {
            0, 1, 2,
            0, 2, 3
        };

        Vector2[] uv =
        {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
