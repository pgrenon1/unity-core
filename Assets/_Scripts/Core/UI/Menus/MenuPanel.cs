using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPanel : DOTweenUIPanel
{
    public MainMenu MainMenu { get; set; }

    // called from Unity events
    public void GoBack()
    {
        GameManager.Instance.GoBack();
    }

    public void WaitAndSetupNavigation()
    {
        StartCoroutine(DoWaitAndSetupNavigation());
    }

    private IEnumerator DoWaitAndSetupNavigation()
    {
        yield return 1;

        SetupNavigation();

        SelectEntryPoint();
    }

    public virtual void SetupNavigation()
    {

    }

    public virtual void SelectEntryPoint()
    {
        var selectable = GetComponentInChildren<Selectable>();

        Select(selectable);
    }

    public void Select(Selectable selectable)
    {
        if (selectable == null)
            return;

        selectable.Select();
        selectable.OnSelect(null);
    }
}
