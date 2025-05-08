using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer_rain_script : MonoBehaviour
{
    public float delay = 5f;

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Animator>().SetBool("shoot", true);
    }

    public void StartCooltime()
    {
        StartCoroutine(ShootDelay());
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
