using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilingmove : MonoBehaviour
{
    public RectTransform[] tiles;

    public float speed = 200f;
    public int direction = -1; 

    float width;

    void Start()
    {
        width = tiles[0].rect.width;
    }

    void Update()
    {
        float move = direction * speed * Time.deltaTime;

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].anchoredPosition += new Vector2(move, 0);

            if (direction < 0 && tiles[i].anchoredPosition.x <= -width)
            {
                tiles[i].anchoredPosition += new Vector2(width * tiles.Length, 0);
            }

            if (direction > 0 && tiles[i].anchoredPosition.x >= width * (tiles.Length - 1))
            {
                tiles[i].anchoredPosition -= new Vector2(width * tiles.Length, 0);
            }
        }
    }
}
