using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapal_eyeslash : MonoBehaviour
{
    public Transform player;
    public GameObject slash;
    public int slashint;

    private void FixedUpdate()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Startslash(int i)
    {
        StartCoroutine(Slash(i));
    }

    IEnumerator Slash(int i)
    {
        yield return new WaitForSeconds(1.5f);

        if (i == 1)
        {
            Instantiate(slash, transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        else if (i == 2)
        {
            Instantiate(slash, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.4f);
            Instantiate(slash, transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        else if (i == 3)
        {
            Instantiate(slash, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.4f);
            Instantiate(slash, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.4f);
            Instantiate(slash, transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}
