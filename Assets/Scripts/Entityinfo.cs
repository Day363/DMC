using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entityinfo : MonoBehaviour
{
    public string entityname;
    public string entitycode;
    [TextArea]
    public string selectline;

    private void Start()
    {
        Debug.Log(entityname);
    }
}
