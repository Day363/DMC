using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashcontrol_alt : MonoBehaviour
{
    public arceffectlossdraphen slash1;
    public arceffectlossdraphen slash2;

    public void Slash1()
    {
        slash1.Decrease();
    }

    public void Slash2()
    {
        slash2.Decrease();
    }
}

