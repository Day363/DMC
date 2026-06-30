using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DefenseSkill : ScriptableObject
{
    public enum DefenseType
    {
        defense, evasion, counter
    };
    public enum CounterType
    {
        slash, penetrate, blow, fix
    };
    public Weapon currentweapon;
    public DefenseType defenseType;
    public CounterType counterType;
    public float calculation;

    public string skillmarkname;
    public string skillcode;

    public GameObject skillprefab;
    public string animationtrigger;
    public List<string> function = new List<string> { };
    public string countertrigger;
}
