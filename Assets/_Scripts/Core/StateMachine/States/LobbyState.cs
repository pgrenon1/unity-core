// using System;
// using System.Collections.Generic;
// using UnityEngine.InputSystem;
//
// public class LobbyState : MenuState
// {
//     private Lobby _lobby;
//     public Lobby Lobby
//     {
//         get
//         {
//             if (_lobby == null)
//                 _lobby = MenuPanel as Lobby;
//
//             return _lobby;
//         }
//     }
//
//     public LobbyState(MainMenu gameMenu, PushdownStateMachine stateMachine)
//         : base(gameMenu, stateMachine)
//     {
//         MenuPanel = gameMenu.GetComponentInChildren<Lobby>();
//     }
//
//     public override void Enter()
//     {
//         base.Enter();
//
//         Lobby.Show(EndEnter);
//     }
//
//     public override void EndEnter()
//     {
//         base.EndEnter();
//
//         PlayerManager.Instance.PlayerRegistered += PlayerManager_PlayerRegistered;
//         PlayerManager.Instance.PlayerUnregistered += PlayerManager_PlayerUnregistered;
//
//         Lobby.ClearPlayerTags();
//         foreach (var player in PlayerManager.Instance.Players)
//         {
//             Lobby.SpawnPlayerTag(player);
//         }
//
//         EnablePlayerJoining();
//     }
//
//     public override void Execute()
//     {
//         base.Execute();
//
//         if (PlayerManager.Instance.Players.Count > 0 && AllReady())
//         {
//             GameManager.Instance.GoToMatch();
//         }
//     }
//
//     private bool AllReady()
//     {
//         bool allReady = true;
//
//         foreach (var playerTag in Lobby.PlayerTags)
//         {
//             var readyState = playerTag.Value.IsReady;
//             if (!readyState)
//                 allReady = false;
//         }
//
//         return allReady;
//     }
//
//     public override void Exit()
//     {
//         base.Exit();
//
//         DisbablePlayerJoining();
//
//         PlayerManager.Instance.PlayerRegistered -= PlayerManager_PlayerRegistered;
//         PlayerManager.Instance.PlayerUnregistered -= PlayerManager_PlayerUnregistered;
//
//         Lobby.Hide(EndExit);
//     }
//
//     private void EnablePlayerJoining()
//     {
//         PlayerInputManager.instance.EnableJoining();
//     }
//
//     private void DisbablePlayerJoining()
//     {
//         PlayerInputManager.instance.DisableJoining();
//     }
//
//     private void PlayerManager_PlayerRegistered(Player player)
//     {
//         Lobby.SpawnPlayerTag(player);
//     }
//     
//     private void PlayerManager_PlayerUnregistered(Player player)
//     {
//         Lobby.RemovePlayerTag(player);
//     }
// }