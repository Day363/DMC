using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class dashskill : ScriptableObject
{
    public Weapon currentweapon;
    public Sprite dashready;
    public float dashafterpower;

    public string skillmarkname;
    public string skillcode;

    public bool prefabskill;
    public bool animationskill;
    public bool functionskill;

    public List<GameObject> skillprefab;
    public string animationtrigger;
    public List<string> function = new List<string> { };
}
