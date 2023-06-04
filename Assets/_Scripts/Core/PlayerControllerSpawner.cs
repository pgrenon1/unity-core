using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSpawner : BaseBehaviour
{
    public bool spawnOnAwake = true;
    public bool spawnOnPlayerJoined = true;
    public List<Transform> spawnPoints;

    private void Awake()
    {
        if (spawnOnAwake)
            SpawnPlayerControllers();
        
        if (spawnOnPlayerJoined)
            PlayerManager.Instance.PlayerRegistered += OnPlayerRegistered;
    }

    private void OnPlayerRegistered(Player player)
    {
        SpawnPlayerControllers();
    }

    private void SpawnPlayerControllers()
    {
        var spawnerIndex = 0;
        for (var i = 0; i < PlayerManager.Instance.Players.Count; i++)
        {
            var player = PlayerManager.Instance.Players[i];
            if (player.PlayerController != null)
                continue;
            
            Transform spawnPoint = spawnPoints[spawnerIndex];
            player.SpawnPlayerController(spawnPoint.position, spawnPoint.rotation);

            spawnerIndex++;
            if (spawnerIndex > spawnPoints.Count)
                spawnerIndex = 0;
        }
    }
}
