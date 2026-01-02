using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public Weapon currentweapon;

    public bool normalskill;
    public bool enforceskill;

    public string chat;

    public bool prefabspawntoenemy = false;

    public string skillmarkname;
    public string skillcode;

    public bool prefabskill;
    public bool animationskill;
    public bool functionskill;

    public List<GameObject> skillprefab;
    public string animationtrigger;
    public List<string> funtionname;

    public bool backlink;
    public bool amalagam;
    
    public bool repeat;
    public bool speed;
    public bool force;
    public bool bout;
    public bool wide;
    public bool mental;
    public bool weight;
    public bool heat;
    public bool reversal;
    public bool space;
    public bool vibration;
    public bool crack;
    public bool explosion;

    [TextArea(5, 100)]
    public string skilldescription;
}
