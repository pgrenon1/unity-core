// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class FactionManager : OdinSerializedSingletonBehaviour<FactionManager>
// {
//     [ColorUsage(true, true)]
//     public List<Color> factionColors = new List<Color>();
//
//     public List<Faction> AllFactions { get; private set; } = new List<Faction>();
//
//     protected override void Awake()
//     {
//         base.Awake();
//
//         foreach (Faction factionValue in Enum.GetValues(typeof(Faction)))
//         {
//             AllFactions.AddUnique(factionValue);
//         }
//     }
//
//     public List<Faction> GetEnemyFactions(Faction faction)
//     {
//         List<Faction> enemyFactions = new List<Faction>();
//         foreach (Faction factionValue in Enum.GetValues(typeof(Faction)))
//         {
//             if (factionValue != faction)
//                 enemyFactions.Add(factionValue);
//         }
//
//         return enemyFactions;
//     }
//
//     public List<Faction> GetAlliedFactions(Faction faction)
//     {
//         List<Faction> enemyFactions = new List<Faction>();
//         foreach (Faction factionValue in Enum.GetValues(typeof(Faction)))
//         {
//             if (factionValue == faction)
//                 enemyFactions.Add(factionValue);
//         }
//
//         return enemyFactions;
//     }
// }
