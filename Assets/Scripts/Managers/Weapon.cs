using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    
    [TextArea(20, 1)]
    public string passive_description;
    [TextArea(20, 1)]
    public string normalskill_description;
    [TextArea(20, 1)]
    public string arreyskill_description;

    public bool range;

    public int magazine;
    public int magazinecycle;

    public string weaponname;
    public Sprite weaponimage;
    public bool slash;
    public bool blow;
    public bool penetrate;

    public List<string> passivelist;
    public DefenseSkill defenseskill;
    public dashskill dashskill;
    public List<Skill> skilllist;
    public List<bool> skillactivelist;
    public List<cynthskill> cynthskilllist;
    public List<standbyskill> standbyskilllist;
}
