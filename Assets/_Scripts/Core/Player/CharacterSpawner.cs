using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSpawner : BaseBehaviour
{
    public bool spawnOnAwake = true;
    public bool spawnOnPlayerJoined = true;
    public List<Transform> spawnPoints;

    private void Awake()
    {
        if (spawnOnAwake)
            SpawnCharacters();
        
        if (spawnOnPlayerJoined)
            PlayerManager.Instance.PlayerRegistered += OnPlayerRegistered;
    }

    private void OnPlayerRegistered(Player player)
    {
        SpawnCharacters();
    }

    private void SpawnCharacters()
    {
        var spawnerIndex = 0;
        for (var i = 0; i < PlayerManager.Instance.Players.Count; i++)
        {
            var player = PlayerManager.Instance.Players[i];
            if (player.Character != null)
                continue;
            
            Transform spawnPoint = spawnPoints[spawnerIndex];
            player.SpawnCharacter(spawnPoint.position, spawnPoint.rotation);
            player.PlayerInputReader.ToggleMainGameInputs(true);

            spawnerIndex++;
            if (spawnerIndex > spawnPoints.Count)
                spawnerIndex = 0;
        }
    }
}
