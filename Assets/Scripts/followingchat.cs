using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followingchat : MonoBehaviour
{
    public GameObject chat;
    public GameObject canvas;

    public RectTransform chatbar;

    public float height;
    public float larg;

    private void Update()
    {
        Vector3 chatpos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + larg, transform.position.y + height, 0));
        chatbar.position = chatpos;
    }
}
