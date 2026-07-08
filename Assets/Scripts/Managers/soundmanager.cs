using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class soundmanager : MonoBehaviour
{
    public static soundmanager instance;

    public GameObject SFXobject;
    public List<GameObject> SFXlist = new List<GameObject>();

    public enum soundvariation { BGM, SFX }
    public enum soundposition { player, enemy, pos1 }
    public AudioSource sfxsoundplayer;
    public AudioSource bgmsoundplayer;

    public GameObject currentenemy;

    [Serializable]
    public class StringAudioPair
    {
        public soundvariation soundvariation;
        public soundposition soundposition;
        public string key;
        public AudioClip clip;
        public bool randompitch;

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
        if (sounddata == null)
        {
            return;
        }
        if (sounddata.soundvariation == soundvariation.BGM)
        {
            bgmsoundplayer.volume = sounddata.volume;
            bgmsoundplayer.clip = sounddata.clip;
            bgmsoundplayer.Play();
        }
        else if (sounddata.soundvariation == soundvariation.SFX)
        {
            GameObject currentSFX = null;
            if (sounddata.soundposition == soundposition.player)
            {
                currentSFX = Instantiate(SFXobject, battalemanager.Instance.player.transform.position, Quaternion.identity);
            }
            else if (sounddata.soundposition == soundposition.enemy)
            {
                currentSFX = Instantiate(SFXobject, currentenemy.transform.position, Quaternion.identity);
            }
            SFXlist.Add(currentSFX);

            AudioSource currentsfxsoundplayer = currentSFX.GetComponent<AudioSource>();

            currentsfxsoundplayer.clip = sounddata.clip;
            currentsfxsoundplayer.PlayOneShot(sounddata.clip, sounddata.volume);
            if (sounddata.randompitch)
            {
                currentsfxsoundplayer.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            }
            else
            {
                currentsfxsoundplayer.pitch = 1;
            }

            StartCoroutine(DestroySFX(currentSFX));
        }
    }

    IEnumerator DestroySFX(GameObject cursound)
    {
        yield return new WaitForSeconds(15f);
        Destroy(cursound);
    }
     

    public void SFXStop()
    {
        for (int i = SFXlist.Count - 1; i >= 0; i--)
        {
            Destroy(SFXlist[i]);
        }
        SFXlist.Clear();
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
