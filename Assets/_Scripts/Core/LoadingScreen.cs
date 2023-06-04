using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEditor;

public class LoadingScreen : DOTweenUIPanel
{
    public TextMeshProUGUI loadingText;

    protected override void Awake()
    {
        base.Awake();

        ToggleIsVisibleFlag(true);

        ShowComplete += () => ToggleLoadingText(true);
        HideBegin += () => ToggleLoadingText(false);
    }

    private void ToggleLoadingText(bool enabled)
    {
        loadingText.enabled = enabled;
    }
}
