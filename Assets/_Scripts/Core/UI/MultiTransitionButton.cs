using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultiTransitionButton : Button
{
	public bool holdToClick;
	[ShowIf(nameof(holdToClick))]
	public float requiredHoldTime;
	[ShowIf(nameof(holdToClick))]
	public Image fillImage;

	public List<TransitionTarget> transitionTargets;

    public delegate void OnButtonSelect();
    public event OnButtonSelect OnButtonSelected;

	[ShowIf(nameof(holdToClick))]
	public UnityEvent onLongClick;

	private bool _buttonDown;
	private float _buttonDownTimer;
    private bool _isSubmitting;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (transitionTargets != null)
        {
            foreach (var target in transitionTargets)
            {
                if (target != null)
                    target.DoStateTransition((IDSelectionState)state, instant);
            }
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        if (OnButtonSelected != null)
            OnButtonSelected.Invoke();
    }

    private void Update()
	{
		if (!holdToClick)
			return;

        if (IsPressed())
            _buttonDown = true;
        else
            ResetHold();

        if (_buttonDown)
		{
			_buttonDownTimer += Time.deltaTime;

			if (_buttonDownTimer >= requiredHoldTime)
			{
				if (onLongClick != null)
					onLongClick.Invoke();

				ResetHold();
			}

			if (fillImage)
				fillImage.fillAmount = _buttonDownTimer / requiredHoldTime;
		}
	}

	private void ResetHold()
	{
		_buttonDown = false;
		_buttonDownTimer = 0f;

		if (fillImage)
			fillImage.fillAmount = 0f;
	}
}