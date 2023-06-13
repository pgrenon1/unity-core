using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Character : BaseBehaviour
{
    public float rayCastMaxDistance;
    public LayerMask rayCastLayerMask;
    
    public Player Player { get; private set; }
    
    private Camera _cameraCache;
    public Camera Camera => GetCachedComponentInChildren(ref _cameraCache);

    public virtual void Init(Player player)
    {
        Player = player;
        Player.PlayerInput.camera = Camera;
        InitInputs();
    }

    protected virtual void InitInputs()
    {
        // my own inputs
        Player.PlayerInputReader.PauseEvent += TogglePause;

        // inputs for CMF controller?
    }

    private void TogglePause()
    {
        GameManager.Instance.TogglePause(Player);
    }
}