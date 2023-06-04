using System;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialPropertyBlockController
{
    public List<Transform> rendererParents = new List<Transform>();
    public List<Renderer> renderers = new List<Renderer>();

    private bool _isInit;
    private Dictionary<Renderer, MaterialPropertyBlock> MaterialPropertyBlocks = new Dictionary<Renderer, MaterialPropertyBlock>();

    public void Init()
    {
        MaterialPropertyBlocks = new Dictionary<Renderer, MaterialPropertyBlock>();

        if (rendererParents != null)
        {
            foreach (var parentTransform in rendererParents)
            {
                var newRenderers = new List<Renderer>();
                parentTransform.GetComponentsInChildren(true, newRenderers);

                renderers.AddRangeUnique(newRenderers);
            }
        }

        if (renderers != null)
        {
            foreach (var renderer in renderers)
            {
                var mpb = new MaterialPropertyBlock();
                MaterialPropertyBlocks.Add(renderer, mpb);
                renderer.SetPropertyBlock(mpb);
            }
        }

        _isInit = true;
    }

    public void SetMatrix(string propertyName, Matrix4x4 value)
    {
        TryInit();

        foreach (var entry in MaterialPropertyBlocks)
        {
            var renderer = entry.Key;
            var mpb = entry.Value;

            renderer.GetPropertyBlock(mpb);
            {
                mpb.SetMatrix(propertyName, value);
            }
            renderer.SetPropertyBlock(mpb);
        }
    }

    public void SetColor(string propertyName, Color value)
    {
        TryInit();

        foreach (var entry in MaterialPropertyBlocks)
        {
            var renderer = entry.Key;
            var mpb = entry.Value;

            renderer.GetPropertyBlock(mpb);
            {
                mpb.SetColor(propertyName, value);
            }
            renderer.SetPropertyBlock(mpb);
        }
    }

    public void SetFloat(string propertyName, float value)
    {
        TryInit();

        foreach (var entry in MaterialPropertyBlocks)
        {
            var renderer = entry.Key;
            var mpb = entry.Value;

            renderer.GetPropertyBlock(mpb);
            {
                mpb.SetFloat(propertyName, value);
            }
            renderer.SetPropertyBlock(mpb);
        }
    }

    public void SetInt(string propertyName, int value)
    {
        TryInit();

        foreach (var entry in MaterialPropertyBlocks)
        {
            var renderer = entry.Key;
            var mpb = entry.Value;

            renderer.GetPropertyBlock(mpb);
            {
                mpb.SetInt(propertyName, value);
            }
            renderer.SetPropertyBlock(mpb);
        }
    }

    public void SetTexture(string propertyName, Texture value)
    {
        TryInit();

        foreach (var entry in MaterialPropertyBlocks)
        {
            var renderer = entry.Key;
            var mpb = entry.Value;

            renderer.GetPropertyBlock(mpb);
            {
                mpb.SetTexture(propertyName, value);
            }
            renderer.SetPropertyBlock(mpb);
        }
    }

    public void SetVector(string propertyName, Vector3 value)
    {
        TryInit();

        foreach (var entry in MaterialPropertyBlocks)
        {
            var renderer = entry.Key;
            var mpb = entry.Value;

            renderer.GetPropertyBlock(mpb);
            {
                mpb.SetVector(propertyName, value);
            }
            renderer.SetPropertyBlock(mpb);
        }
    }

    public void DoColor(string propertyName, Color endValue, float duration)
    {
        TryInit();

        foreach (var entry in MaterialPropertyBlocks)
        {
            var renderer = entry.Key;
            var mpb = entry.Value;

            {
                var tween = DOTween.To(() => 
                {
                    renderer.GetPropertyBlock(mpb);
                    return mpb.GetColor(propertyName);
                }, x => 
                {
                    mpb.SetColor(propertyName, x);
                    renderer.SetPropertyBlock(mpb);
                },
                endValue, duration);

                tween.Play();
            }
        }
    }

    public Color GetColor(string propertyName, int index)
    {
        TryInit();

        var renderer = renderers[index];

        Color value;
        var hasPropBlock = renderer.HasPropertyBlock();
        var propBlockIsEmpty = false;
        if (hasPropBlock)
        {
            renderer.GetPropertyBlock(MaterialPropertyBlocks[renderer]);
            propBlockIsEmpty = MaterialPropertyBlocks[renderer].isEmpty;
        }
        if (hasPropBlock && !propBlockIsEmpty)
        {
            value = MaterialPropertyBlocks[renderer].GetColor(propertyName);
        }
        else
        {
            value = renderer.material.GetColor(propertyName);
        }

        return value;
    }

    private void TryInit()
    {
        if (!_isInit)
            Init();
    }

    public int GetInt(string propertyName, int index)
    {
        TryInit();

        var renderer = renderers[index];
        MaterialPropertyBlock mpb = MaterialPropertyBlocks[renderer];
        renderer.GetPropertyBlock(mpb);
        var value = mpb.GetInt(propertyName);

        return value;
    }

    public Vector3 GetVector(string propertyName, int index)
    {
        TryInit();

        var renderer = renderers[index];
        MaterialPropertyBlock mpb = MaterialPropertyBlocks[renderer];
        renderer.GetPropertyBlock(mpb);
        var value = mpb.GetVector(propertyName);

        return value;
    }

    public float GetFloat(string propertyName, int index)
    {
        TryInit();

        var renderer = renderers[index];
        MaterialPropertyBlock mpb = MaterialPropertyBlocks[renderer];
        renderer.GetPropertyBlock(mpb);
        var value = mpb.GetFloat(propertyName);

        return value;
    }
}
