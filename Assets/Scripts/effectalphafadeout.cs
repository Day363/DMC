using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class effectalphafadeout : MonoBehaviour
{
    public float fadeDuration = 1f; // 페이드 아웃 시간 (초)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        StartCoroutine(DestroyEffect());

        spriteRenderer = GetComponent<SpriteRenderer>();

        // 알파값 0으로 페이드 아웃
        spriteRenderer.DOFade(0f, fadeDuration)
            .SetEase(Ease.Linear); // 원하는 이징 적용 가능
    }

    IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
