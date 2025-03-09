using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public string weaponname;
    public Sprite weaponimage;
    public float damage;
    public float balancedamage;
    public List<Normalskill> normalskilllist = new List<Normalskill>();
    public List<Enforceskill> enforceskilllist = new List<Enforceskill>();
}
