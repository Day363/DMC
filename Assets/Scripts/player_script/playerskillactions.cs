using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerskillactions : MonoBehaviour
{
    public int dir;
    public float dash1power;
    public float dash2power;

    public void Update()
    {
        dir = GetComponent<PlayerMove>().dir;
    }

    public void Dash1()
    {
        if (dir == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(dash1power * Vector2.right, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(-dash1power * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void BackDash1()
    {
        if (dir == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(-dash1power * Vector2.right, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(dash1power * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void Dash2()
    {
        if (dir == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(dash2power * Vector2.right, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(-dash2power * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void BackDash2()
    {
        if (dir == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(-dash2power * Vector2.right, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(dash2power * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void StartAttack()
    {
        GetComponent<PlayerMove>().canmove = false;
    }

    public void EndAttack()
    {
        GetComponent<PlayerMove>().canmove = true;
    }
}
