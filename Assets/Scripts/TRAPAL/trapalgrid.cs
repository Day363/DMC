using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class trapalgrid : MonoBehaviour
{
    public int columns = 3;
    public float cellWidth = 1f;
    public float cellHeight = 1f;
    public Vector2 spacing = Vector2.zero;
    public bool autoUpdate = true;

    private int lastChildCount = -1;

    public int min = 5;
    public int max = 15;

    private void Start()
    {
        columns = Random.Range(min, max);
    }

    void Update()
    {
        if (!autoUpdate) return;

        if (transform.childCount != lastChildCount)
        {
            ArrangeChildren();
            lastChildCount = transform.childCount;
        }
    }

    public void ArrangeChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            int row = i / columns;
            int col = i % columns;

            float x = col * (cellWidth + spacing.x);
            float y = -row * (cellHeight + spacing.y);

            child.localPosition = new Vector3(x, y, 0);
        }
    }
}

