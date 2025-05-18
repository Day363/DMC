using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_pointattackready : MonoBehaviour
{
    public GameObject pointattack;
    public GameObject[] prefabs;        
    public float radius = 3f;           
    public Vector3 center; 
    public float spawnInterval = 0.1f; 
    public int shoot = 0;

    void Start()
    {
        StartCoroutine(SpawnObjectsInCircle());
        center = transform.position;
    }

    private void FixedUpdate()
    {
        if (shoot == 2)
        {
            Instantiate(pointattack, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private IEnumerator SpawnObjectsInCircle()
    {
        int objectCount = prefabs.Length;

        float angleStep = 360f / objectCount;

        for (int i = 0; i < objectCount; i++)
        {
            GameObject prefab = prefabs[i];
            if (prefab == null)
            {
                Debug.LogWarning($"프리팹 배열의 {i}번째 요소가 비어 있습니다!");
                continue;
            }

            float angleRad = i * angleStep * Mathf.Deg2Rad;
            float x = center.x + radius * Mathf.Cos(angleRad);
            float y = center.y + radius * Mathf.Sin(angleRad);
            Vector3 spawnPos = new Vector3(x, y, center.z);

            Quaternion rotation = Quaternion.Euler(0, 0, i * angleStep);

            GameObject instance = Instantiate(prefab, spawnPos, rotation);
            instance.transform.SetParent(this.transform);

            yield return new WaitForSeconds(spawnInterval);
        }
        radius = radius + 0.5f;
        shoot++;
        StartCoroutine(SpawnObjectsInCircle());
    }
}
