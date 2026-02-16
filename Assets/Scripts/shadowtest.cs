using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class shadowtest : MonoBehaviour
{
    public Transform target;          // 그림자 생성 대상
    public Transform lightSource;     // 광원
    public float height = 1f;         // 물체 높이
    public float maxLength = 20f;     // 그림자 최대 길이
    public bool realtimeUpdate = true;

    Mesh mesh;
    MeshFilter mf;

    Vector3 lastTargetPos;
    Vector3 lastLightPos;

    void Awake()
    {
        mf = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mf.mesh = mesh;
    }

    void LateUpdate()
    {
        if (!target || !lightSource) return;

        if (!realtimeUpdate &&
            target.position == lastTargetPos &&
            lightSource.position == lastLightPos)
            return;

        lastTargetPos = target.position;
        lastLightPos = lightSource.position;

        GenerateShadow();
    }

    void GenerateShadow()
    {
        SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
        if (!sr || !sr.sprite) return;

        Vector2[] spriteVerts = sr.sprite.vertices;
        Vector3[] verts = new Vector3[spriteVerts.Length * 2];
        int[] tris = new int[(spriteVerts.Length - 1) * 6];

        Vector3 lightDir = (target.position - lightSource.position).normalized;

        float angle = Vector3.Angle(Vector3.down, lightDir);
        float length = height / Mathf.Tan(angle * Mathf.Deg2Rad);
        length = Mathf.Clamp(length, 0.1f, maxLength);

        for (int i = 0; i < spriteVerts.Length; i++)
        {
            Vector3 world = target.TransformPoint(spriteVerts[i]);
            Vector3 local = transform.InverseTransformPoint(world);

            verts[i] = local;
            verts[i + spriteVerts.Length] = local + (Vector3)(lightDir * length);
        }

        int t = 0;
        for (int i = 0; i < spriteVerts.Length - 1; i++)
        {
            int a = i;
            int b = i + 1;
            int c = i + spriteVerts.Length;
            int d = i + 1 + spriteVerts.Length;

            tris[t++] = a;
            tris[t++] = b;
            tris[t++] = c;

            tris[t++] = b;
            tris[t++] = d;
            tris[t++] = c;
        }

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateBounds();
    }
}
