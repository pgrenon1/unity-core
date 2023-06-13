using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerManager : OdinSerializedSingletonBehaviour<PlayerManager>
{
    public Character characterPrefab;

    public List<Player> Players { get; private set; } = new List<Player>();
    
    public delegate void OnCharacterCreated();
    public event OnCharacterCreated CharacterCreated;

    public delegate void OnPlayerRegistered(Player player);
    public event OnPlayerRegistered PlayerRegistered;

    public delegate void OnPlayerUnregistered(Player player);
    public event OnPlayerUnregistered PlayerUnregistered;

    protected override void Awake()
    {
        base.Awake();
        
        PlayerInputManager.instance.EnableJoining();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            int sceneIndex = SceneUtility.GetBuildIndexByScenePath(scene.path);
            if (sceneIndex == -1)
                PlayerInputManager.instance.DisableJoining();
        }
    }

    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += PlayerInputManager_OnPlayerJoined;
    }

    private void PlayerInputManager_OnPlayerJoined(PlayerInput newPlayerInput)
    {
        //var player = newPlayerInput.GetComponent<Player>();

        //_lastJoinedFaction = _lastJoinedFaction == Faction.Faction1 ? Faction.Faction2 : Faction.Faction1;
        //player.Faction = _lastJoinedFaction;
        //player.CreatePlayerController();
        //player.PlayerInputHandler.SwitchPlayersActionMapToGame();
    }

    public void DestroyCharacter()
    {
        foreach (var player in Players)
        {
            Destroy(player.Character.gameObject);
        }
    }

    public void RegisterPlayer(Player playerOld)
    {
        if (Players.Contains(playerOld))
        {
            Debug.LogError($"Player {playerOld} is already registered!");
            return;
        }

        Players.Add(playerOld);

        if (PlayerRegistered != null)
            PlayerRegistered(playerOld);
    }

    public void UnregisterPlayer(Player player)
    {
        if (!Players.Contains(player))
        {
            Debug.LogError($"Player {player} is not yet registered and we're trying to unregister it?!");
            return;
        }

        Players.Remove(player);

        if (PlayerUnregistered != null)
            PlayerUnregistered(player);
    }

    public void SwitchPlayersActionMapToGame()
    {
        foreach (var player in Players)
        {
            player.PlayerInputReader.ToggleMainGameInputs(true);
        }
    }
}
