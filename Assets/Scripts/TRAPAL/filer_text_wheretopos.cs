using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class filer_text_wheretopos : MonoBehaviour
{

    public Vector3 pos;
    public void FixedUpdate()
    {
        transform.position = pos;
    }
}
