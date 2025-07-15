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

    public List<GameObject> skillprefab;
    public string animationtrigger;
}
