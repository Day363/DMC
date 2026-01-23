using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscenestatusuimanager : MonoBehaviour
{
    public void Start()
    {
        uimanager.Instance.playerscenestatus = gameObject;
    }
}
