using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afterimagespawner : MonoBehaviour
{
    public GameObject afterImagePrefab;
    public float spawnInterval = 0.1f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAfterImage();
            timer = 0f;
        }
    }

    void SpawnAfterImage()
    {
        GameObject image = Instantiate(afterImagePrefab, transform.position, transform.rotation);
        SpriteRenderer sr = image.GetComponent<SpriteRenderer>();
        SpriteRenderer currentSR = GetComponent<SpriteRenderer>();

        sr.sprite = currentSR.sprite;
        sr.flipX = currentSR.flipX; // 좌우 반전 유지
    }
}
