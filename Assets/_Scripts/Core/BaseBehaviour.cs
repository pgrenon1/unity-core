using System;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    protected T GetCachedComponent<T>(ref T cache)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return GetComponent<T>();
        }
#endif

        if (cache == null)
        {
            cache = GetComponent<T>();
        }

        return cache;
    }

    protected T GetCachedComponentInChildren<T>(ref T cache)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return GetComponentInChildren<T>();
        }
#endif

        if (cache == null)
        {
            cache = GetComponentInChildren<T>();
        }

        return cache;
    }

    protected T GetCachedComponentInParent<T>(ref T cache)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return GetComponentInParent<T>();
        }
#endif

        if (cache == null)
        {
            cache = GetComponentInParent<T>();
        }

        return cache;
    }

    private RectTransform _rectTransformCache;
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransformCache == null)
                _rectTransformCache = (RectTransform)transform;

            return _rectTransformCache;
        }
    }
}