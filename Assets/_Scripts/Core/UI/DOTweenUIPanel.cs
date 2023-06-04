using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DOTweenUIPanel : DOTweenUIElement
{
    [Header("Panel")]
    public float delayBetweenShowElements = 0f;
    public float delayBetweenHideElements = 0f;

    private CanvasGroup _canvasGroupCache;
    private CanvasGroup _canvasGroup => GetCachedComponent(ref _canvasGroupCache);

    private List<DOTweenUIElement> _uiElements = new List<DOTweenUIElement>();

    protected override void Awake()
    {
        base.Awake();

        GetComponentsInChildren(_uiElements);

        ShowComplete += () => SetMenusActive(true);
        HideBegin += () => SetMenusActive(false);
    }

    private void SetMenusActive(bool active)
    {
        _canvasGroup.blocksRaycasts = active;
        _canvasGroup.interactable = active;
    }

    public virtual void Show(Action extraShownCallback = null, bool instantaneous = false)
    {
        StartCoroutine(DoToggleElements(true, () => TriggerShowComplete(extraShownCallback), instantaneous));
    }

    public virtual void Hide(Action hiddenCallback = null, bool instantaneous = false)
    {
        StartCoroutine(DoToggleElements(false, () => TriggerOnHideComplete(hiddenCallback), instantaneous));
    }

    private IEnumerator DoToggleElements(bool show, Action completedCallback = null, bool instantaneous = false)
    {
        bool allComplete = instantaneous;
        var delayBetweenElements = 0f;

        if (!instantaneous)
            delayBetweenElements = show ? delayBetweenShowElements : delayBetweenHideElements;

        foreach (var uiElement in _uiElements)
        {
            uiElement.Toggle(show, instantaneous);

            yield return new WaitForSeconds(delayBetweenElements);
        }

        while (!allComplete)
        {
            allComplete = true;
            foreach (var uiElement in _uiElements)
            {
                if (!uiElement.IsComplete())
                {
                    allComplete = false;
                }
            }

            yield return null;
        }

        completedCallback.Invoke();
    }
}