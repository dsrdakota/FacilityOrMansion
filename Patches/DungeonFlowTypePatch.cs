using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DunGen.Graph;
using HarmonyLib;
using UnityEngine;

namespace FacilityOrMansion.Patches
{
	internal class DungeonFlowTypePatch
	{
		private static List<DungeonFlow> ToDungeonFlowList(IndoorMapType[] maps)
		{
			List<DungeonFlow> ret = new List<DungeonFlow>();
			foreach (var map in maps)
			{
				ret.Add(map.dungeonFlow);
			}
			return ret;
		}
		private static IndoorMapType[] ToIndoorMapArray(RoundManager __instance, List<DungeonFlow> dungeonFlows)
		{
			IndoorMapType[] ret = new IndoorMapType[dungeonFlows.Count];
			foreach (var map in dungeonFlows)
			{
				foreach (var type in __instance.dungeonFlowTypes)
				{
					if(type.dungeonFlow == map) { 
						ret.AddItem<IndoorMapType>(type);
                    }
                }
			}
			return ret;
		}
		[HarmonyPatch(typeof(RoundManager), "Awake")]
		[HarmonyPriority(250)]
		[HarmonyPostfix]
		private static void onStartPrefix(RoundManager __instance)
		{
            if (FacilityOrMansionBase.modIsEnabled.Value) 
			{ 
				List<IndoorMapType> list = new List<IndoorMapType>();
				FacilityOrMansionBase.mls.LogInfo("Inserting missing dungeon flows into the RoundManager");
<<<<<<< Updated upstream
                list.AddRange(Resources.FindObjectsOfTypeAll<DungeonFlow>());
				List<DungeonFlow> list2 = ToDungeonFlowList(__instance.dungeonFlowTypes);
                for (int i = 0; i < list.Count; i++)
                {
                    DungeonFlow item = list[i];
                    list2.Add(item);
                }
				//__instance.dungeonFlowTypes = list2.ToArray();
				ToIndoorMapArray(__instance, list2);
=======
				List<IndoorMapType> list2 = __instance.dungeonFlowTypes.ToList<IndoorMapType>();

				//foreach (var item in UnityEngine.Object.FindObjectsOfType(typeof(IndoorMapType))) {
				foreach (var item in Resources.FindObjectsOfTypeAll(typeof(IndoorMapType))) {
					Type type = item.GetType();
					DungeonFlow dun = (DungeonFlow)type.GetProperty("dungeonFlow").GetValue(type);
					float size = (float)type.GetProperty("MapTileSize").GetValue(type);
					FacilityOrMansionBase.mls.LogInfo("mapsize: " + size.ToString());
					
					IndoorMapType map = new IndoorMapType();
					map.dungeonFlow = dun;
					map.MapTileSize = size;

					FacilityOrMansionBase.mls.LogInfo("map name: " + dun.name);
					list.Add(map);
				}

				//list.AddRange(Resources.FindObjectsOfTypeAll(typeof(IndoorMapType)));
				for (int i = 0; i < list.Count; i++)
				{
					IndoorMapType item = list[i];
					list2.Add(item);
				}
				__instance.dungeonFlowTypes = list2.ToArray();
>>>>>>> Stashed changes
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
