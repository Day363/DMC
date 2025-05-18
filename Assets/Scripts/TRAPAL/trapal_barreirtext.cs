using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_barreirtext : MonoBehaviour
{
    public float radius;
    public GameObject[] texts;
    public float waittime;
    public Vector3 center;
    public int charge;

    public void StartBarrier()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("text"))
            {
                Destroy(child.gameObject);
            }
        }
        center = transform.position;
        StartCoroutine(SpawnObjectsInCircle());
            
    }

    private IEnumerator SpawnObjectsInCircle()
    {
        charge = 0;

        int objectCount = texts.Length;

        float angleStep = 360f / objectCount;

        for (int i = 0; i < objectCount; i++)
        {
            GameObject prefab = texts[i];
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

            yield return new WaitForSeconds(waittime);
            charge++;
            if (charge == 24)
            {
                GetComponent<trapal_barrierActive>().ActiveCo();
            }
        }
    }
}
