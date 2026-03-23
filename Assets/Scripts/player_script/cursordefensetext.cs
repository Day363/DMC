using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cursordefensetext : MonoBehaviour
{
    public void Start()
    {
        attackcore.attackcoreInstance.cursordefense = GetComponent<TMP_Text>();
    }
}
