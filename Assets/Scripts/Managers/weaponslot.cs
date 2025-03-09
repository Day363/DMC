using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponslot : MonoBehaviour
{
    public Sprite weaponimage;

    public void Update()
    {
        GetComponent<Image>().sprite = weaponimage;
    }
}
