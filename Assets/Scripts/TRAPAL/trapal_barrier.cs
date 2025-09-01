using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_barrier : MonoBehaviour
{
    public GameObject particle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyattack"))
        {
            particle.GetComponent<ParticleSystem>().Play();
            GetComponent<PolygonCollider2D>().enabled = false;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 0f;
            GetComponent<SpriteRenderer>().color = color;
            
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<PolygonCollider2D>().enabled = true;
        gameObject.SetActive(false);
    }
}
