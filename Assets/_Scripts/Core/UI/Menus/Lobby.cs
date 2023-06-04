// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class Lobby : MenuPanel
// {
//     public LobbyPlayerTag playerTagPrefab;
//     public Transform playerListParent;
//     public Transform faction1Parent;
//     public Transform factionNoneParent;
//     public Transform faction2Parent;
//
//     public Dictionary<Player, LobbyPlayerTag> PlayerTags { get; private set; } = new Dictionary<Player, LobbyPlayerTag>();
//     public Dictionary<Player, bool> ReadyStates { get; private set; } = new Dictionary<Player, bool>();
//
//     public void SpawnPlayerTag(Player player)
//     {
//         var playerTag = Instantiate(playerTagPrefab, playerListParent);
//
//         playerTag.SetData(this, player);
//
//         PlayerTags.Add(player, playerTag);
//     }
//
//     public void ClearPlayerTags()
//     {
//         foreach (var playerTag in PlayerTags.Values)
//         {
//             Destroy(playerTag.gameObject);
//         }
//
//         PlayerTags.Clear();
//     }
//
//     public void RemovePlayerTag(Player player)
//     {
//         LobbyPlayerTag playerTag;
//
//         if (PlayerTags.TryGetValue(player, out playerTag))
//             Destroy(playerTag.gameObject);
//     }
//
//     public void ToggleReady(Player player)
//     {
//         if (!ReadyStates.ContainsKey(player))
//             ReadyStates.Add(player, false);
//
//         ReadyStates[player] = !ReadyStates[player];
//     }
//
//     // Called from ui event
//     public void GoToMatch()
//     {
//         GameManager.Instance.GoToMatch();
//     }
//
//     public Transform GetParentForFaction(Faction faction)
//     {
//         switch (faction)
//         {
//             case Faction.Faction1:
//                 return faction1Parent;
//             case Faction.Faction2:
//                 return faction2Parent;
//             case Faction.None:
//             default:
//                 return factionNoneParent;
//         }
//     }
// }
