using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer_rain_script : MonoBehaviour
{
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(5f);
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
