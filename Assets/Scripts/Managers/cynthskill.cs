using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class cynthskill : ScriptableObject
{
    public string skillname;
    public Skill skill;
    public int cycle;
    public string keyword;
    public List<string> condition;
}
