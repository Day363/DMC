using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashtestobject : MonoBehaviour
{
    public GameObject slash;

    public float time;
    public float maxscale;
    public float minscale;

    public void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        GameObject curslash = Instantiate(slash, transform);
        float scale = Random.Range(minscale, maxscale);
        curslash.transform.localScale = new Vector3(scale, scale, 1);
        curslash.transform.rotation = Quaternion.Euler(Random.Range(0f, 180f), Random.Range(0f, 180f), Random.Range(0f, 360f));
        yield return new WaitForSeconds(time);
        StartCoroutine(Spawn());
    }
}
