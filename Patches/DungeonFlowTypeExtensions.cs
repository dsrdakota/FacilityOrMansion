using System;
using System.Collections.Generic;

namespace FacilityOrMansion.Patches
{
	public static class DungeonFlowTypeExtensions
	{
		public static IntWithRarity[] setDungeonFlowTypeRarity(this RoundManager manager)
		{
			FacilityOrMansionBase.mls.LogInfo("Entered setDungeonFlowTypeRarity");
			List<IntWithRarity> list = new List<IntWithRarity>();
			for (int i = 0; i < manager.dungeonFlowTypes.Length; i++)
			{
				IntWithRarity intWithRarity = new IntWithRarity();
				intWithRarity.id = i;
<<<<<<< Updated upstream
                

                FacilityOrMansionBase.mls.LogInfo("DungeonFlow Name = " + manager.dungeonFlowTypes[i].dungeonFlow.name);
=======
				FacilityOrMansionBase.mls.LogInfo("DungeonFlow Name = " + manager.dungeonFlowTypes[i].dungeonFlow.name);
>>>>>>> Stashed changes
				if (manager.dungeonFlowTypes[i].dungeonFlow.name == "Level2Flow")
                {
                    if (FacilityOrMansionBase.mansionOnly.Value && FacilityOrMansionBase.facilityOnly.Value)
                    {
                        intWithRarity.rarity = 150;
                    }
                    if (FacilityOrMansionBase.mansionOnly.Value && !FacilityOrMansionBase.facilityOnly.Value)
                    {
                        intWithRarity.rarity = 300;
                    }
                    if (FacilityOrMansionBase.facilityOnly.Value && !FacilityOrMansionBase.mansionOnly.Value)
                    {
                        intWithRarity.rarity = 0;
                    }
                }
				else
                {
                    if (FacilityOrMansionBase.mansionOnly.Value && FacilityOrMansionBase.facilityOnly.Value)
                    {
                        intWithRarity.rarity = 150;
                    }
                    if (FacilityOrMansionBase.mansionOnly.Value && !FacilityOrMansionBase.facilityOnly.Value)
                    {
                        intWithRarity.rarity = 0;
					}
					if (FacilityOrMansionBase.facilityOnly.Value && !FacilityOrMansionBase.mansionOnly.Value)
					{
						intWithRarity.rarity = 300;
					}
                }
				FacilityOrMansionBase.mls.LogInfo(string.Format("New Rarity = {0}", intWithRarity.rarity));
				list.Add(intWithRarity);
			}
			return list.ToArray();
		}
	}
}
