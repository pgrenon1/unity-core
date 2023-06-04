using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class DOTweenUIElement : BaseBehaviour
{
    [Header("Element")]
    public bool hideOnAwake = true;

    private const string HIDE_ID = "hide";
    private const string SHOW_ID = "show";
    public DOTweenAnimation ShowAnimation { get; private set; }
    public DOTweenAnimation HideAnimation { get; private set; }

    private bool _isInit = false;
    public bool IsVisible { get; set; }

    public delegate void OnShowBegin();
    public OnShowBegin ShowBegin;

    public delegate void OnShowComplete();
    public event OnShowComplete ShowComplete;

    public delegate void OnHideBegin();
    public event OnHideBegin HideBegin;

    public delegate void OnHideComplete();
    public event OnHideComplete HideComplete;

    protected virtual void Awake()
    {
        ShowBegin += () => ToggleIsVisibleFlag(true);
        HideComplete += () => ToggleIsVisibleFlag(false);
    }

    protected virtual void Start()
    {
        if (hideOnAwake)
            Toggle(false, instantaneous: true);
    }

    protected void ToggleIsVisibleFlag(bool visible)
    {
        IsVisible = visible;
    }

    [Button("Create Show/Hide Anims")]
    public void CreateShowHideAnims()
    {
        var anims = new List<DOTweenAnimation>();
        GetComponents(anims);

        if (!anims.Any(a => a.id == SHOW_ID))
        {
            AddDOTweenAnimation(SHOW_ID);
        }

        if (!anims.Any(a => a.id == HIDE_ID))
        {
            AddDOTweenAnimation(HIDE_ID);
        }
    }

    private void AddDOTweenAnimation(string id)
    {
        var anim = gameObject.AddComponent<DOTweenAnimation>();
        anim.id = id;
        anim.loops = 0;
        anim.autoPlay = false;
        anim.autoKill = false;
    }

    private void Init()
    {
        foreach (var dotweenAnim in GetComponents<DOTweenAnimation>())
        {
            if (dotweenAnim.id.ToLower() == SHOW_ID)
            {
                ShowAnimation = dotweenAnim;
            }
            else if (dotweenAnim.id.ToLower() == HIDE_ID)
            {
                HideAnimation = dotweenAnim;
            }
        }

        _isInit = true;
    }

    public void Toggle(bool show, bool instantaneous = false)
    {
        if (!_isInit)
            Init();

        DOTweenAnimation dotweenAnimation;
        if (show)
        {
            // call the callbacks for the beginning of the transition
            if (ShowBegin != null)
                ShowBegin();

            dotweenAnimation = ShowAnimation;
        }
        else
        {
            // call the callbacks for the beginning of the transition
            if (HideBegin != null)
                HideBegin();

            dotweenAnimation = HideAnimation;
        }
        
        dotweenAnimation.target.DOKill();

        if (instantaneous)
        {
            dotweenAnimation.tween.Complete();
        }
        else
        {
            dotweenAnimation.tween.Restart(true);
        }

        IsVisible = show;
    }

    public bool IsComplete()
    {
        if (!_isInit)
            Init();

        return !ShowAnimation.tween.IsPlaying() && !HideAnimation.tween.IsPlaying();
    }

    public virtual void TriggerOnHideComplete(Action hiddenCallback = null)
    {
        if (HideComplete != null)
            HideComplete();

        if (hiddenCallback != null)
            hiddenCallback.Invoke();
    }

    public virtual void TriggerShowComplete(Action shownCallback = null)
    {
        if (ShowComplete != null)
            ShowComplete();

        if (shownCallback != null)
            shownCallback.Invoke();
    }
}
