using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testslashcontrol : MonoBehaviour
{
    public arceffectloss slash1_1;
    public arceffectloss slash1_2;
    public arceffectloss slash1_3;
    public arceffectloss slash1_4;
    public arceffectloss slash1_5;
    public arceffectloss slash1_6;

    public void Slash1()
    {
        slash1_1.Decrease();
        slash1_2.Decrease();
        slash1_3.Decrease();
        slash1_4.Decrease();
        slash1_5.Decrease();
        slash1_6.Decrease();
    }
}
