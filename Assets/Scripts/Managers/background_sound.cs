using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_sound : MonoBehaviour
{
    public string bgmname;

    private void Start()
    {
        soundmanager.instance.SoundPlay(bgmname);
    }
}
