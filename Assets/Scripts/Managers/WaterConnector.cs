using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterConnector : MonoBehaviour
{
    public waterscript upperWater;
    public waterscript lowerWater;

    [Range(4, 256)]
    public int resolution = 64;

    Mesh mesh;

    Vector3[] vertices;
    Vector2[] uv;
    Color[] colors;       
    int[] triangles;

    [Header("Wave Transfer")]
    public bool transferWave = true;
    public float transferStrength = 0.05f;

    float[] lastLowerHeights;

    [Header("Bridge Simulation")]
    public float bridgeSpring = 0.03f;
    public float bridgeDamping = 0.02f;
    public float bridgeSpread = 0.15f;

    [Header("Vertical Transfer")]
    [Range(2, 64)]
    public int verticalResolution = 16;

    float[,] bridgeWave;
    float[,] bridgeVelocity;

    
    [Header("Water Color")]
    public float waveColorRange = 0.3f;   

    void OnEnable() { BuildMesh(); }
    void OnValidate() { BuildMesh(); }

    void Update()
    {
        if (upperWater == null || lowerWater == null) return;
        if (mesh == null) BuildMesh();

        UpdateMesh();

        if (Application.isPlaying && transferWave)
        {
            TransferWave();
            SimulateBridge();
        }
    }

    void BuildMesh()
    {
        resolution = Mathf.Max(4, resolution);

        MeshFilter mf = GetComponent<MeshFilter>();

        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.name = "Water Connector";
        }

        mf.sharedMesh = mesh;

        vertices = new Vector3[resolution * 2];
        uv = new Vector2[vertices.Length];
        colors = new Color[vertices.Length];     
        triangles = new int[(resolution - 1) * 6];

        int t = 0;
        for (int i = 0; i < resolution - 1; i++)
        {
            int tl = i * 2, bl = i * 2 + 1, tr = i * 2 + 2, br = i * 2 + 3;
            triangles[t++] = tl; triangles[t++] = tr; triangles[t++] = bl;
            triangles[t++] = tr; triangles[t++] = br; triangles[t++] = bl;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.colors = colors;                    

        lastLowerHeights = new float[resolution];
        bridgeWave = new float[resolution, verticalResolution];
        bridgeVelocity = new float[resolution, verticalResolution];

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    void UpdateMesh()
    {
        float left = Mathf.Max(
            upperWater.transform.position.x - upperWater.width * 0.5f,
            lowerWater.transform.position.x - lowerWater.width * 0.5f);

        float right = Mathf.Min(
            upperWater.transform.position.x + upperWater.width * 0.5f,
            lowerWater.transform.position.x + lowerWater.width * 0.5f);

        if (right <= left) return;

        
        float baseY = upperWater.transform.position.y;

        for (int i = 0; i < resolution; i++)
        {
            float t = (float)i / (resolution - 1);
            float worldX = Mathf.Lerp(left, right, t);

            float upperNorm = (worldX - (upperWater.transform.position.x - upperWater.width * 0.5f)) / upperWater.width;
            float lowerNorm = (worldX - (lowerWater.transform.position.x - lowerWater.width * 0.5f)) / lowerWater.width;

            float upperHeight = upperWater.SampleHeight(upperNorm) + bridgeWave[i, verticalResolution - 1];
            float lowerHeight = lowerWater.SampleHeight(lowerNorm) + bridgeWave[i, 0];

            Vector3 upperPos = new Vector3(
    worldX,
    upperWater.transform.position.y + upperHeight,
    0);

            Vector3 lowerPos = new Vector3(
                worldX,
                lowerWater.transform.position.y + lowerHeight,
                0);

            vertices[i * 2] = transform.InverseTransformPoint(upperPos);
            vertices[i * 2 + 1] = transform.InverseTransformPoint(lowerPos);

            uv[i * 2] = new Vector2(t, 1);
            uv[i * 2 + 1] = new Vector2(t, 0);

            
            float waveT = Mathf.InverseLerp(-waveColorRange, waveColorRange, upperHeight);
            colors[i * 2] = new Color(waveT, 0, 0, 1);
            
            colors[i * 2 + 1] = new Color(0, 0, 0, 1);
        }

        mesh.vertices = vertices;
        mesh.colors = colors;                     
        mesh.RecalculateBounds();
    }

    void TransferWave()
    {
        float left = Mathf.Max(
            upperWater.transform.position.x - upperWater.width * 0.5f,
            lowerWater.transform.position.x - lowerWater.width * 0.5f);
        float right = Mathf.Min(
            upperWater.transform.position.x + upperWater.width * 0.5f,
            lowerWater.transform.position.x + lowerWater.width * 0.5f);

        if (right <= left) return;

        for (int x = 0; x < resolution; x++)
        {
            float t = (float)x / (resolution - 1);
            float worldX = Mathf.Lerp(left, right, t);
            float lowerNorm = (worldX - (lowerWater.transform.position.x - lowerWater.width * 0.5f)) / lowerWater.width;
            float current = lowerWater.SampleHeight(lowerNorm);
            float delta = current - lastLowerHeights[x];
            lastLowerHeights[x] = current;
            bridgeVelocity[x, 0] += delta * 0.5f;
        }

        for (int x = 0; x < resolution; x++)
        {
            float t = (float)x / (resolution - 1);
            float worldX = Mathf.Lerp(left, right, t);
            float upperNorm = (worldX - (upperWater.transform.position.x - upperWater.width * 0.5f)) / upperWater.width;
            upperWater.AddVelocityAtNormalized(upperNorm, bridgeWave[x, verticalResolution - 1] * transferStrength);
        }
    }

    void SimulateBridge()
    {
        for (int x = 0; x < resolution; x++)
            for (int y = 0; y < verticalResolution; y++)
            {
                float force = -bridgeWave[x, y] * bridgeSpring;
                bridgeVelocity[x, y] += force;
                bridgeVelocity[x, y] *= (1f - bridgeDamping);
                bridgeWave[x, y] += bridgeVelocity[x, y];
            }

        for (int k = 0; k < 4; k++)
            for (int x = 0; x < resolution; x++)
                for (int y = 0; y < verticalResolution - 1; y++)
                {
                    float delta = (bridgeWave[x, y] - bridgeWave[x, y + 1]) * bridgeSpread;
                    bridgeVelocity[x, y + 1] += delta;
                    bridgeVelocity[x, y] -= delta;
                }
    }

    void OnDrawGizmos()
{
    if (mesh == null) return;

    Gizmos.color = Color.cyan;
    Gizmos.matrix = transform.localToWorldMatrix;
    Gizmos.DrawWireMesh(mesh);
}
}