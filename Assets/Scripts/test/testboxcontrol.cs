using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testboxcontrol : MonoBehaviour
{
    public sqaureeffectloss1 slash1_1;
    public sqaureeffectloss1 slash1_2;
    public sqaureeffectloss1 slash1_3;
    public sqaureeffectloss1 slash1_4;


    public void BoxSlash1()
    {
        slash1_1.Decrease();
        slash1_2.Decrease();
        slash1_3.Decrease();
        slash1_4.Decrease();
    
    }
}
