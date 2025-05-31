using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_barrier : MonoBehaviour
{
    public GameObject particle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ãæµ¹");
        if (collision.gameObject.CompareTag("enemybullet"))
        {
            if (collision.gameObject.GetComponent<trapal_bullet>().reflected)
            {
                Destroy(collision.gameObject);
                particle.GetComponent<ParticleSystem>().Play();
                GetComponent<PolygonCollider2D>().enabled = false;
                Color color = GetComponent<SpriteRenderer>().color;
                color.a = 0f;
                GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<PolygonCollider2D>().enabled = true;
        gameObject.SetActive(false);
    }
}
