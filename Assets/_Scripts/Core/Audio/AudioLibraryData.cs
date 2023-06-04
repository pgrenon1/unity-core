using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioLibraryData : OdinSerializedScriptableObject
{
    public Dictionary<Keyword, AudioClip> clips = new Dictionary<Keyword, AudioClip>();
}
