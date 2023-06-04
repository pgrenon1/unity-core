using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : OdinSerializedSingletonBehaviour<AudioManager>
{
    private AudioLibraryData _audioLibraryData;
    [ShowInInspector, InlineEditor]
    public AudioLibraryData AudioLibraryData
    {
        get
        {
            if (_audioLibraryData == null)
                _audioLibraryData = Index.Instance.audioLibrary;

            return _audioLibraryData;
        }
        set
        {
            _audioLibraryData = Index.Instance.audioLibrary;
        }
    }

    public AudioClip GetAudioClip(Keyword key)
    {
        if (!AudioLibraryData.clips.ContainsKey(key))
        {
            Debug.LogError($"Audio Library contains no audio clip for key {key}");
            return null;
        }

        return AudioLibraryData.clips[key];
    }
}
