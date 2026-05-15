using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class fade_sprite : MonoBehaviour
{
    public float fadetime = 1.5f;
    public bool follow;
    public Transform original;
    public Vector3 startPos;
    public float timer;

    public void Start()
    {
         startPos = transform.position;
        GetComponent<SpriteRenderer>().DOFade(0, fadetime);
        StartCoroutine(Destroy());

        if (TryGetComponent<GlitchController>(out var controller))
        {
            controller.burstInterval = Random.Range(0.15f, 0.7f);
        }
    }
    
    void Update()
    {
        if (follow)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / fadetime);

            t = EaseOutQuad(t);

            transform.position = Vector3.Lerp(
                startPos,
                original.position,
                t
            );
        }
        
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(fadetime + 0.1f);
        Destroy(gameObject);
    }

    float EaseOutQuad(float x)
    {
        return 1f - (1f - x) * (1f - x);
    }
}
