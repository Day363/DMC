using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cursorskilltext : MonoBehaviour
{
    public void Start()
    {
        attackcore.attackcoreInstance.cursorskill = GetComponent<TMP_Text>();
    }

}
