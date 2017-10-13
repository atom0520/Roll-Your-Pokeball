using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance { get; private set; }

    [Serializable]
    class AudioClipInfo
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public UnityEngine.AudioClip audioClip;
    }

    Dictionary<string, AudioClip> audioClipDict;
    [SerializeField]
    AudioSource bgmSrc;
    [SerializeField]
    AudioSource[] soundEffectsSrc;

    [SerializeField]
    AudioClipInfo[] audioClipsInfo;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        audioClipDict = new Dictionary<string, AudioClip>();
        foreach (AudioClipInfo audioClipInfo in audioClipsInfo)
        {
            audioClipDict.Add(audioClipInfo.name, audioClipInfo.audioClip);
        }
    }
    
    public void PlayBGM(string name, bool loop=true)
    {
        bgmSrc.loop = loop;
        bgmSrc.clip = audioClipDict[name];
        bgmSrc.Play();
    }

    public void PauseBGM()
    {
        bgmSrc.Pause();
    }

    public void UnPauseBGM()
    {
        bgmSrc.UnPause();
    }

    public void StopBGM()
    {
        bgmSrc.Stop();
    }

    public void PlaySoundEffect(string name)
    {
        AudioSource soundEffectSrc = GetAvailableSoundEffectSrc();
        if (soundEffectSrc != null)
        {
            soundEffectSrc.clip = audioClipDict[name];
            soundEffectSrc.Play();
        }
    }

    AudioSource GetAvailableSoundEffectSrc()
    {
        foreach (AudioSource soundEffectSrc in soundEffectsSrc)
        {
            if (!soundEffectSrc.isPlaying)
            {
                return soundEffectSrc;
            }
        }
        return null;
    }
}
