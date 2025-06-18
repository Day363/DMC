using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public bool range;

    public int magazine;
    public int magazinecycle;

    public string weaponname;
    public Image weaponimage;
    public bool slash;
    public bool blow;
    public bool penetrate;

    public List<Skill> skilllist;
    public List<cynthskill> cynthskilllist;
    public List<standbyskill> standbyskilllist;
}
