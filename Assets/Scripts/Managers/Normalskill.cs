using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Normalskill : ScriptableObject
{
    public string normalskillname;
    public GameObject skill;
    public bool slash;
    public bool blow;
    public bool penetrate;

    public float attackpower;
    public float boutpower;
}
