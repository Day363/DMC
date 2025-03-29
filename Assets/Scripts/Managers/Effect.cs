using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    public string effectname;
    public int effecttime;
    public GameObject player;

    public void Effects(string effectname, int effecttime)
    {
        this.effectname = effectname;
        this.effecttime = effecttime;
    }

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}

public class slowdown : Effect
{
    public float slowint;

    public slowdown(string effectname, int effecttime, float slowint)
    {
        this.slowint = slowint;
    }

    public override void ApplyEffect()
    {
        player.GetComponent<playerhealth>().speed = player.GetComponent<playerhealth>().speed - slowint;
    }

    public override void RemoveEffect()
    {
        player.GetComponent<playerhealth>().speed = player.GetComponent<playerhealth>().speed + slowint;
    }
}