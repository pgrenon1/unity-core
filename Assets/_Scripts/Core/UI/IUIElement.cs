using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIElement
{
    void Show(bool instantaneous = false, Action shownCallback = null);
    void Hide(bool instantaneous = false, Action hiddenCallback = null);
}
