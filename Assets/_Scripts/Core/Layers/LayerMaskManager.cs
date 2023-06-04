using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LayerMaskManager : OdinSerializedSingletonBehaviour<LayerMaskManager>
{
    private LayerMaskManagerData _layerMaskManagerData;
    [ShowInInspector, InlineEditor]
    public LayerMaskManagerData LayerMaskManagerData
    {
        get
        {
            if (_layerMaskManagerData == null)
                _layerMaskManagerData = Index.Instance.layerMaskManagerData;

            return _layerMaskManagerData;
        }
        set
        {
            _layerMaskManagerData = Index.Instance.layerMaskManagerData;
        }
    }

    public int GetHitBoxLayerForFaction(Faction faction)
    {
        int layer = 0;

        if (LayerMaskManagerData.factionsHitBoxLayer.TryGetValue(faction, out layer))
        {
            return layer;
        }

        return layer;
    }

    public LayerMask GetLayerMask(LayerMaskType layerMaskType)
    {
        LayerMask layerMask;
        if (LayerMaskManagerData.layerMaskTypes.TryGetValue(layerMaskType, out layerMask))
        {
            return layerMask;
        }

        return ~0;
    }

    public LayerMask GetHitBoxLayerMaskForFactions(List<Faction> targetFactions)
    {
        LayerMask layerMask = 0;

        foreach (var targetFaction in targetFactions)
        {
            layerMask |= 1 << GetHitBoxLayerForFaction(targetFaction);
        }

        return layerMask;
    }

    public int GetVisualsLayerForFaction(Faction faction)
    {
        if (LayerMaskManagerData.factionsVisualsLayer.Count - 1 > (int)faction)
        {
            return LayerMaskManagerData.factionsVisualsLayer[(int)faction];
        }

        return 0;
    }

    public int GetSplitScreenLayerForPlayerIndex(int playerIndex)
    {
        if (LayerMaskManagerData.splitScreenLayers.Count - 1 > playerIndex)
        {
            return LayerMaskManagerData.splitScreenLayers[playerIndex];
        }

        return 0;
    }


}
