using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public string weaponname;
    public Sprite weaponimage;
    public float damageCoe;
    public GameObject core;
}
