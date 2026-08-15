using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashcontrol : MonoBehaviour
{
    public arceffectlossdraphen slash1_1;
    public arceffectlossdraphen slash1_2;
    

    public void Slash1()
    {
        slash1_1.Decrease();
    }

    public void Slash2()
    {
        slash1_2.Decrease();
    }

    public void Active()
    {
        slash1_1.Increase();
    }

    
}
