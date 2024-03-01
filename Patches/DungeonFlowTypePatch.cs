using System;
using System.Collections.Generic;
using System.Linq;
using DunGen.Graph;
using HarmonyLib;
using UnityEngine;

namespace FacilityOrMansion.Patches
{
	internal class DungeonFlowTypePatch
	{
		[HarmonyPatch(typeof(RoundManager), "Awake")]
		[HarmonyPriority(250)]
		[HarmonyPostfix]
		private static void onStartPrefix(RoundManager __instance)
		{
            if (FacilityOrMansionBase.modIsEnabled.Value) 
			{ 
				List<DungeonFlow> list = new List<DungeonFlow>();
				FacilityOrMansionBase.mls.LogInfo("Inserting missing dungeon flows into the RoundManager");
				List<DungeonFlow> list2 = __instance.dungeonFlowTypes.ToList<DungeonFlow>();
				list.AddRange(Resources.FindObjectsOfTypeAll<DungeonFlow>());
				for (int i = 0; i < list.Count; i++)
				{
					DungeonFlow item = list[i];
					list2.Add(item);
				}
				__instance.dungeonFlowTypes = list2.ToArray();
            }
        }
		[HarmonyPatch(typeof(RoundManager), "GenerateNewFloor")]
		[HarmonyPrefix]
		private static void FlowTypeUpdate(RoundManager __instance)
		{
            if (FacilityOrMansionBase.modIsEnabled.Value) 
			{ 
				SelectableLevel currentLevel = __instance.currentLevel;
				FacilityOrMansionBase.mls.LogInfo("Changing dungeon flow values");
				currentLevel.dungeonFlowTypes = __instance.setDungeonFlowTypeRarity();
            }
        }
	}
}
