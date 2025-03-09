using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashtest : MonoBehaviour
{
    public GameObject self;

    public void SetEnd()
    {
        GetComponent<Animator>().SetBool("attack", false);
        self.SetActive(false);
    }
}
