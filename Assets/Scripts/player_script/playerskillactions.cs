using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerskillactions : MonoBehaviour
{
    public int dir;
    public float dash1power;
    public float dash2power;
    public float dash3power;
    public float dash4power;

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

    public void Dash3()
    {
        if (dir == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(dash3power * Vector2.right, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(-dash3power * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void Dash4()
    {
        if (dir == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(dash4power * Vector2.right, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(-dash4power * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void SlowStop()
    {
        DOTween.To(() => GetComponent<Rigidbody2D>().velocity, x => GetComponent<Rigidbody2D>().velocity = x, new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y), 0.2f);
    }

    public void LinerDragUp()
    {
        GetComponent<Rigidbody2D>().drag = 10f;
    }

    public void LinerDragDown()
    {
        GetComponent<Rigidbody2D>().drag = 0f;
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
