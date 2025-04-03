using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhit : MonoBehaviour
{
    public void Hit(int damage)
    {
        GetComponent<playerstatus>().Damage(damage);
    }

    public void StrongHit(int damage, Transform attacktransform)
    {
        GetComponent<Animator>().SetBool("knockback", true);
        GetComponent<playerstatus>().Damage(damage);
        int dir = GetComponent<Transform>().position.x - attacktransform.position.x > 0 ? 3 : -3;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 3), ForceMode2D.Impulse);
    }

    public void FlyAway(float power, float up)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(power, up), ForceMode2D.Impulse);
    }

    public void Update()
    {
        
    }

}
