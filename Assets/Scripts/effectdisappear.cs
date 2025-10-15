using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectdisappear : MonoBehaviour
{
    public bool disappearbytime = false;
    public float time;

    public void Start()
    {
        if (disappearbytime)
        {
            StartCoroutine(Disappear_co());
        }
    }

   IEnumerator Disappear_co()
   {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
   }

    public void EffectDisappear()
    {
        Destroy(gameObject);
    }
}
