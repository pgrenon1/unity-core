using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Sirenix.OdinInspector;
using DG.Tweening;

[HideLabel, Serializable]
public class TransitionTarget : MonoBehaviour
{
    public Transition transition;

    [HideIf("transition", Transition.None), HideIf("transition", Transition.Animation)]
    public Graphic targetGraphic;
    [ShowIf("transition", Transition.Animation)]
    public Animator targetAnimator;
    [ShowIf("transition", Transition.TextMeshProUGUI)]

    // ColorTint
    [ShowIf("transition", Transition.ColorTint)]
    public Color normalColor = Color.white;
    [ShowIf("transition", Transition.ColorTint)]
    public Color highlightedColor = new Color(245f, 245f, 245f);
    [ShowIf("transition", Transition.ColorTint)]
    public Color pressedColor = new Color(200f, 200f, 200f);
    [ShowIf("transition", Transition.ColorTint)]
    public Color selectedColor = new Color(245f, 245f, 245f);
    [ShowIf("transition", Transition.ColorTint)]
    public Color disablededColor = new Color(240f, 240f, 240f, 128f);
    [ShowIf("transition", Transition.ColorTint)]
    public float fadeDuration = 0.1f;

    // SpriteSwap
    [ShowIf("transition", Transition.SpriteSwap)]
    public Sprite highlightedSprite;
    [ShowIf("transition", Transition.SpriteSwap)]
    public Sprite pressedSprite;
    [ShowIf("transition", Transition.SpriteSwap)]
    public Sprite selectedSprite;
    [ShowIf("transition", Transition.SpriteSwap)]
    public Sprite disabledSprite;

    // Animator
    [ShowIf("transition", Transition.Animation)]
    public string normalTrigger = "Normal";
    [ShowIf("transition", Transition.Animation)]
    public string highlightedTrigger = "Highlighted";
    [ShowIf("transition", Transition.Animation)]
    public string pressedTrigger = "Pressed";
    [ShowIf("transition", Transition.Animation)]
    public string selectedTrigger = "Selected";
    [ShowIf("transition", Transition.Animation)]
    public string disabledTrigger = "Disabled";

    private Image _targetImage;
    private Sprite _normalSprite;

    private void Start()
    {
        if (transition == Transition.SpriteSwap)
        {
            _targetImage = targetGraphic as Image;
            if (_targetImage != null)
                _normalSprite = _targetImage.sprite;
        }
    }

    public void DoStateTransition(IDSelectionState state, bool instant)
    {
        var targetSprite = _normalSprite;
        var animationTrigger = normalTrigger;
        var targetColor = normalColor;

        switch (state)
        {
            case IDSelectionState.Normal:
                targetSprite = _normalSprite;
                animationTrigger = normalTrigger;
                targetColor = normalColor;
                break;
            case IDSelectionState.Highlighted:
                targetSprite = highlightedSprite;
                animationTrigger = highlightedTrigger;
                targetColor = highlightedColor;
                break;
            case IDSelectionState.Pressed:
                targetSprite = pressedSprite;
                animationTrigger = pressedTrigger;
                targetColor = pressedColor;
                break;
            case IDSelectionState.Selected:
                targetSprite = selectedSprite;
                animationTrigger = selectedTrigger;
                targetColor = selectedColor;
                break;
            case IDSelectionState.Disabled:
                targetSprite = disabledSprite;
                animationTrigger = disabledTrigger;
                targetColor = disablededColor;
                break;
        }

        switch (transition)
        {
            case Transition.ColorTint:
                FadeTo(targetColor, instant);
                break;
            case Transition.SpriteSwap:
                SwapSprite(targetSprite);
                break;
            case Transition.Animation:
                SetAnimatorTrigger(animationTrigger);
                break;
            default:
                return;
        }
    }

    private void SwapSprite(Sprite targetSprite)
    {
        if (!_targetImage)
            return;

        _targetImage.sprite = targetSprite;
    }

    private void SetAnimatorTrigger(string animationTrigger)
    {
        if (!targetAnimator)
            return;

        targetAnimator.SetTrigger(animationTrigger);
    }

    private void FadeTo(Color targetColor, bool instant)
    {
        if (!targetGraphic)
            return;

        var duration = 0f;
        if (!instant)
            duration = fadeDuration;

        targetGraphic.CrossFadeColor(targetColor, duration, false, true);
    }
}
