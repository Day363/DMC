using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class trapallightamaager : MonoBehaviour
{
    private Light2D light2D;

    public float minIntensity = 0.5f;
    public float maxIntensity = 3f;

    public float flashSpeed = 2.5f;

    private float timer = 0f;

    void Start()
    {
        light2D = GetComponent<Light2D>();

        if (light2D == null)
        {
            Debug.LogWarning("Light2D 컴포넌트가 이 객체에 없습니다.");
        }
    }

    void Update()
    {
        timer += Time.deltaTime * flashSpeed;

        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(timer, 1f));
    }
}
