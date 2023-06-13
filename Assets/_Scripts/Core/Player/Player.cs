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

    public Character Character { get; private set; }
    
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

    public void SpawnCharacter(Vector3 position, Quaternion rotation)
    {
        var playerController = Instantiate(PlayerManager.Instance.characterPrefab,
            position,
            rotation);

        AssignCharacter(playerController);

        if (PlayerControllerSpawned != null)
            PlayerControllerSpawned();
    }

    public void AssignCharacter(Character character)
    {
        Character = character;
        SceneManager.MoveGameObjectToScene(Character.gameObject, gameObject.scene);
        Character.Init(this);
    }

    public void DestroyPlayerController()
    {
        // CameraController.Instance.targetGroupControllerPlayers.RemoveTargetFromGroup(PlayerController.transform);
        Destroy(Character.gameObject);
    }
}
