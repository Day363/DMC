using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class trapallightamaager : MonoBehaviour
{
    private Light2D light2D;

    public float minIntensity = 0.5f;
    public float maxIntensity = 3f;

    // 사이렌의 주기 (시간)
    public float flashSpeed = 2.5f;

    // 타이머 변수
    private float timer = 0f;

    void Start()
    {
        // Light2D 컴포넌트를 찾음
        light2D = GetComponent<Light2D>();

        // Light2D가 없으면 경고 메시지 출력
        if (light2D == null)
        {
            Debug.LogWarning("Light2D 컴포넌트가 이 객체에 없습니다.");
        }
    }

    void Update()
    {
        // 타이머 증가
        timer += Time.deltaTime * flashSpeed;

        // 빛의 강도는 sin 함수로 주기적으로 변동
        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(timer, 1f));
    }
}
