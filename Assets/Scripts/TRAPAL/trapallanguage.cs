using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapallanguage : MonoBehaviour
{
    public GameObject[] texts;
    public int randint;

    public int max;
    public int current = 0;

    public GameObject currenttext;

    private void Start()
    {
        max = Random.Range(10, 71);
        StartCoroutine(DeleteALL());
    }

    private void FixedUpdate()
    {
        if (current < max)
        {
            current++;
            randint = Random.Range(0, 32);
            currenttext = Instantiate(texts[randint], transform);
            StartCoroutine(Delete(currenttext));
        }
        
    }

    IEnumerator Delete(GameObject text)
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(text);
    }

    IEnumerator DeleteALL()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
