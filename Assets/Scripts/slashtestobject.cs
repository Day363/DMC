using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashtestobject : MonoBehaviour
{
    public GameObject slash;

    public float time;
    public float maxscale;
    public float minscale;

    public bool disappeartime = true;
    public float whendisappear;

    public void Start()
    {
        StartCoroutine(Spawn());
        if (disappeartime)
        {
            StartCoroutine(Disappear());
        }
        
    }

    IEnumerator Spawn()
    {
        GameObject curslash = Instantiate(slash, transform.position, Quaternion.identity);
        float scale = Random.Range(minscale, maxscale);
        curslash.transform.localScale = new Vector3(scale, scale, 1);
        curslash.transform.rotation = Quaternion.Euler(Random.Range(0f, 180f), Random.Range(0f, 180f), Random.Range(0f, 360f));
        if (curslash.transform.GetChild(0).TryGetComponent<playerattackdamage>(out playerattackdamage pa))
        {
            pa.player = battalemanager.Instance.player;
        }
        yield return new WaitForSeconds(time);
        StartCoroutine(Spawn());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(whendisappear);
        Destroy(gameObject);
    }
}
