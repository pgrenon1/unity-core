using UnityEngine;

public class PlayerController : BaseBehaviour
{
    public Player Player { get; private set; }
    private Camera _cameraCache;
    public Camera Camera => GetCachedComponentInChildren(ref _cameraCache);

    public virtual void Init(Player player)
    {
        Player = player;
        Player.PlayerInput.camera = Camera;
        RegisterInputs();
    }

    protected virtual void RegisterInputs()
    {
        Player.PlayerInputReader.PauseEvent += TogglePause;
    }

    private void TogglePause()
    {
        GameManager.Instance.TogglePause(Player);
    }
}