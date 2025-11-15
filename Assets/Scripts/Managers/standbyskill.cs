using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class standbyskill : ScriptableObject
{
    public string skillname;

    public List<string> skillarreyto;

    public string passive;

    public string animationtrigger;

    public float length;

    [TextArea(5, 100)]
    public string skilldescription;
}
