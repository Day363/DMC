using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startsoundplay : MonoBehaviour
{
    public string soundname;

    public void Start()
    {
        if (soundname != null)
        {
            battalemanager.Instance.gameObject.GetComponent<soundmanager>().SoundPlay(soundname);
        }
        
    }
}
