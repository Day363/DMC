using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DefenseSkill : ScriptableObject
{
    public Weapon currentweapon;

    public string skillmarkname;
    public string skillcode;

    public bool prefabskill;
    public bool animationskill;
    public bool functionskill;

    public bool counter;

    public List<GameObject> skillprefab;
    public string animationtrigger;
    public List<string> function = new List<string> { };
    public string countertrigger;
}
