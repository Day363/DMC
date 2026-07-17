using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class communicator_red_grad : MonoBehaviour
{
    public SpriteRenderer sr;
    public float range;
    public GameObject player;

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = battalemanager.Instance.player;

    }

    public void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        float alpha = Mathf.Clamp01(1 - (distance / range));

        Color color = sr.color;
        color.a = alpha;
        sr.color = color;
    }
}
