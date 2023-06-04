using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : OdinSerializedBehaviour
{
    public Keyword key;

    private AudioSource _audioSource;
    public AudioSource AudioSource => GetCachedComponent(ref _audioSource);

    private void Awake()
    {
        AudioSource.clip = AudioManager.Instance.GetAudioClip(key);
    }

    public void Play()
    {
        AudioSource.Play();
    }
}
