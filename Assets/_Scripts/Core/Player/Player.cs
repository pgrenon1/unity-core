using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class Player : OdinSerializedBehaviour
{
    private PlayerInputReader _playerInputReaderCache;
    public PlayerInputReader PlayerInputReader => GetCachedComponent(ref _playerInputReaderCache);

    private PlayerInput _playerInputCache;
    public PlayerInput PlayerInput => GetCachedComponent(ref _playerInputCache);

    private InputSystemUIInputModule _inputSystemUIInputModuleCache;
    public InputSystemUIInputModule InputSystemUIInputModule => GetCachedComponent(ref _inputSystemUIInputModuleCache);

    public PlayerController PlayerController { get; private set; }
    
    public delegate void OnPlayerControllerSpawned();
    public event OnPlayerControllerSpawned PlayerControllerSpawned;
    
    private void Start()
    {
        PlayerManager.Instance.RegisterPlayer(this);
        SetupPlayerInputComponent();
    }

    private void SetupPlayerInputComponent()
    {
        PlayerInput.defaultActionMap = PlayerInputReader.PlayerActions.Menu.Get().name;
        
        var playerActionsAsset = PlayerInputReader.PlayerActions.asset;
        PlayerInput.actions = playerActionsAsset;
        InputSystemUIInputModule.actionsAsset = playerActionsAsset;

        PlayerInput.uiInputModule = InputSystemUIInputModule;
    }

    public void SpawnPlayerController(Vector3 position, Quaternion rotation)
    {
        var playerController = Instantiate(PlayerManager.Instance.playerControllerPrefab,
            position,
            rotation);

        SetPlayerController(playerController);

        if (PlayerControllerSpawned != null)
            PlayerControllerSpawned();
    }

    public void SetPlayerController(PlayerController playerController)
    {
        PlayerController = playerController;
        SceneManager.MoveGameObjectToScene(PlayerController.gameObject, gameObject.scene);
        PlayerController.Init(this);
    }

    public void DestroyPlayerController()
    {
        // CameraController.Instance.targetGroupControllerPlayers.RemoveTargetFromGroup(PlayerController.transform);
        Destroy(PlayerController.gameObject);
    }
}
