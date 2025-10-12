using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteAlways] // 에디터 상태에서도 Update() 실행
public class ClockHierarchyRotator : MonoBehaviour
{
    

    public Transform hourHand;   // 시침 (부모)
    public Transform minuteHand; // 분침 (시침의 자식)
    public Transform secondHand; // 초침 (분침의 자식)


    public float hourRotationTime = 86400f;

    [Range(0f, 360f)]
    public float hourAngle = 0f;

    public bool normaltime;

    void FixedUpdate()
    {
        if (normaltime)
        {
            float hourSpeed = 360f / hourRotationTime;


            float delta = Application.isPlaying ? Time.deltaTime : (1f / 60f);

            hourAngle += hourSpeed * delta;
        }
        
        hourHand.eulerAngles = new Vector3(0, 0, -hourAngle);
        minuteHand.eulerAngles = new Vector3(0, 0, -(hourAngle * 12f));
        secondHand.eulerAngles = new Vector3(0, 0, -(hourAngle * 720f));
    }

    [ContextMenu("BattleStart")]
    public void BattleStart()
    {
        StartCoroutine(BattleStart_co());
    }

    IEnumerator BattleStart_co()
    {
        float currentAngle = hourHand.eulerAngles.z;

        float endAngle = 20f + 360f * 15f;

        DOTween.To(
            () => hourAngle,       
            x => hourAngle = x,    
            endAngle,              
            2.5f                     
        ).SetEase(Ease.InCubic).SetId("turn");

        yield return new WaitForSeconds(2.5f);

        DOTween.Kill("turn");

        DOTween.To(
            () => hourAngle,       
            x => hourAngle = x,    
            endAngle - 20f,              
            1f                     
        ).SetEase(Ease.OutCubic).SetId("turn");

        
    }


}

