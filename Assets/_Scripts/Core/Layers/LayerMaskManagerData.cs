using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LayerMaskManagerData : OdinSerializedScriptableObject
{
    [Layer]
    public List<int> splitScreenLayers = new List<int>();

#if UNITY_EDITOR
    [DictionaryLayer(DictionaryAttributeTarget.Value, typeof(LayerAttribute))]
#endif
    public Dictionary<Faction, int> factionsHitBoxLayer = new Dictionary<Faction, int>();

    [Layer]
    public List<int> factionsVisualsLayer = new List<int>();

    public Dictionary<LayerMaskType, LayerMask> layerMaskTypes = new Dictionary<LayerMaskType, LayerMask>();
}