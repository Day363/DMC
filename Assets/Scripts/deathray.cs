using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class deathray : MonoBehaviour
{
    public GameObject target;

    public void Start()
    {
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        foreach (Transform child in gameObject.transform)
        {

            float z = Mathf.Clamp(Gaussian(0f, 7f), -30f, 30f);
            child.transform.DOLocalMoveZ(z, 0.1f).SetEase(Ease.OutExpo);
            float t = Mathf.InverseLerp(0f, 30f, Mathf.Abs(z));
            float maxScale = Mathf.Lerp(0.75f, 0.2f, t);
            float scale = Random.Range(0.1f, maxScale);

            child.transform.DOScale(scale, 0.1f).SetEase(Ease.OutExpo);
            child.GetComponent<trapalhaloturn>().turnspeed = Random.Range(-0.5f, 0.5f);
            
        }

        yield return new WaitForSeconds(0.1f);

        int count = 0;
        foreach (Transform child2 in gameObject.transform)
        {
            child2.gameObject.SetActive(true);
            count++;

            if (count >= 3)
            {
                count = 0;
                yield return null;
            }
        }
    }

    float Gaussian(float mean = 0f, float stdDev = 1f)
    {
        float u1 = 1.0f - Random.value; 
        float u2 = 1.0f - Random.value;
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                              Mathf.Sin(2.0f * Mathf.PI * u2);

        return mean + stdDev * randStdNormal;
    }

    public void Update()
    {
        transform.LookAt(new Vector3(target.transform.position.x, 0, 0));
    }
}
