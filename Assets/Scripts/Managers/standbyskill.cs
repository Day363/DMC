using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    [System.Serializable]
    public class Skillcell
    {
        public enum Attacktype
        {
            slash,
            penetrate,
            blow,
            fix
        };

        public float calculation;
        public Attacktype attacktype;
        public int attackmuch = 1;
    }

    public List<Skillcell> skillcells;

}
