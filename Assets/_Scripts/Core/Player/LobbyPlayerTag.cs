// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UI;
//
// public class LobbyPlayerTag : MonoBehaviour
// {
//     public Image playerImage;
//     public TextMeshProUGUI playerNameText;
//     public Image backgroundNotReady;
//     public Image backgroundReady;
//
//     private Player _player;
//     private Lobby _lobby;
//
//     public bool IsReady { get; private set; } 
//
//     public void SetData(Lobby lobby, Player player)
//     {
//         _player = player;
//         _lobby = lobby;
//
//         _player.PlayerInputReader.NavigateEvent += ChangeFaction;
//         _player.PlayerInputReader.SubmitEvent += ToggleReady;
//         // _player.MenuInputHandler.navigateAction.GetInputAction(_player.PlayerInput).performed += ChangeFaction;
//         // _player.MenuInputHandler.submitAction.GetInputAction(_player.PlayerInput).performed += ToggleReady;
//     }
//
//     private void ToggleReady()
//     {
//         if (_player.Faction == Faction.None)
//             return;
//
//         IsReady = !IsReady;
//
//         backgroundReady.enabled = IsReady;
//         backgroundNotReady.enabled = !IsReady;
//     }
//
//     private void ChangeFaction(Vector2 direction)
//     {
//         if (IsReady)
//             return;
//
//         // var direction = _player.MenuInputHandler.navigateAction.GetInputAction(_player.PlayerInput).ReadValue<Vector2>();
//         var xOffset = direction.x;
//
//         if (!Mathf.Approximately(xOffset, 0f))
//         {
//             if (xOffset > 0f)
//             {
//                 MoveRight();
//             }
//             else if (xOffset < 0f)
//             {
//                 MoveLeft();
//             }
//
//             Transform parent = _lobby.GetParentForFaction(_player.Faction);
//             transform.SetParent(parent);
//             transform.localPosition = Vector3.zero;
//             LayoutRebuilder.MarkLayoutForRebuild(parent as RectTransform);
//         }
//     }
//
//     private void MoveLeft()
//     {
//         switch (_player.Faction)
//         {
//             case Faction.None:
//                 _player.Faction = Faction.Faction1;
//                 break;
//             case Faction.Faction2:
//                 _player.Faction = Faction.None;
//                 IsReady = false;
//                 break;
//             case Faction.Faction1:
//             default:
//                 break;
//         }
//     }
//
//     private void MoveRight()
//     {
//         switch (_player.Faction)
//         {
//             case Faction.None:
//                 _player.Faction = Faction.Faction2;
//                 break;
//             case Faction.Faction1:
//                 _player.Faction = Faction.None;
//                 IsReady = false;
//                 break;
//             case Faction.Faction2:
//             default:
//                 break;
//         }
//     }
// }
