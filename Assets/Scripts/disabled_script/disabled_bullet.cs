using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disabled_bullet : MonoBehaviour
{
    public float bulletspeed;
    public GameObject disabled;
    public int time;
    public int cooltime = 300;

    public void Awake()
    {
        disabled = GameObject.Find("disabled");
        if (disabled.GetComponent<disabled_rushmanage>().direction == -1)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * bulletspeed, ForceMode2D.Impulse);
        }

        if (disabled.GetComponent<disabled_rushmanage>().direction == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * bulletspeed, ForceMode2D.Impulse);
        }
    }

    public void Update()
    {
        time = time + 1;
        if (time == cooltime)
        {
            Destroy(gameObject);
        }
    }
}
