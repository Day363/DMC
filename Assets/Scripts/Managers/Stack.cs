using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { Buff, Debuff, Stack }
public enum TriggerType { OnCirculStart, OnCirculEnd, OnAttack, OnHit, JustStack, Seconddown, Secondup }

[CreateAssetMenu]

public class Stack : ScriptableObject
{
    public string effectName;//이름
    public EffectType type;//버픈지 디버픈지 그냥 스텍인지
    public TriggerType trigger;//언제 발동할지
    public Sprite icon;//아이콘
    public bool stackable;//스텍 가능 여부
    public int maxStacks;//최대 스텍
    public int defaultstack;// 처음 부여할때 스텍

    [TextArea]
    public string description;
}
