using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HyperUIController : MonoBehaviour
{
    public Hyper8 target;

    public Slider rotSlider;
    public Slider sliceSlider;
    public Slider morphSlider;
    public Slider symSlider;

    void Update()
    {
        target.hyperRotation =
            rotSlider.value;

        target.slice =
            sliceSlider.value * 2f - 1f;

        target.morph =
            morphSlider.value;

        target.symmetry =
            symSlider.value;
    }
}
