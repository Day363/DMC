using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class soundmanager : MonoBehaviour
{
    public static soundmanager instance;

    public enum soundvariation { BGM, SFX }
    public AudioSource sfxsoundplayer;
    public AudioSource bgmsoundplayer;

    [Serializable]
    public class StringAudioPair
    {
        public soundvariation soundvariation;
        public string key;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1;
    }

    public List<StringAudioPair> audioclips = new List<StringAudioPair> { };

    private void Awake()
    {
        instance = this;
    }

    public void SoundPlay(string name)
    {
        StringAudioPair sounddata = StringAudioPairData(name);
        if (sounddata.soundvariation == soundvariation.BGM)
        {
            bgmsoundplayer.volume = sounddata.volume;
            bgmsoundplayer.clip = sounddata.clip;
            bgmsoundplayer.Play();
        }
        else if (sounddata.soundvariation == soundvariation.SFX)
        {
            sfxsoundplayer.volume = sounddata.volume;
            sfxsoundplayer.clip = sounddata.clip;
            sfxsoundplayer.PlayOneShot(sounddata.clip);
        }
    }

    public void SFXStop()
    {
        sfxsoundplayer.Stop();
    }

    public void BGMStop()
    {
        bgmsoundplayer.Stop();
    }

    public StringAudioPair StringAudioPairData(string audioname)
    {
        foreach (var data in audioclips)
        {
            if (data.key == audioname)
                return data;
        }

        Debug.LogWarning($"오디오 키를 찾을 수 없음: {audioname}");
        return null;
    }
}
