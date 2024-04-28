using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using FacilityOrMansion.Patches;

namespace FacilityOrMansion
{
	// Token: 0x02000002 RID: 2
	[BepInPlugin("dsrdakota.FacilityOrMansion", "Facility or Mansion", "1.0.2")]
	public class FacilityOrMansionBase : BaseUnityPlugin
	{
		public static FacilityOrMansionBase Instance { get; private set; }
        public static ConfigEntry<bool> modIsEnabled { get; set; }
        public static ConfigEntry<bool> mansionOnly { get; set; }
        public static ConfigEntry<bool> facilityOnly { get; set; }
        public static ManualLogSource mls { get; private set; }
		private void Awake()
		{
			if (FacilityOrMansionBase.Instance == null)
			{
                FacilityOrMansionBase.Instance = this;
            }
            modIsEnabled = Config.Bind("Facility or Mansion", "Mod Enabled", true, "Enable or disable the mod.");
            mansionOnly = Config.Bind("Choose Only One", "Mansion Only", true, "Interiors set to Mansion.");
            facilityOnly = Config.Bind("Choose Only One", "Facility Only", false, "Interiors set to Facility.");
            FacilityOrMansionBase.mls = BepInEx.Logging.Logger.CreateLogSource("DeathWrench.FacilityOrMansion");
			FacilityOrMansionBase.mls.LogInfo("Facility or Mansion Only mod has awakened!");
			this.harmony.PatchAll(typeof(DungeonFlowTypePatch));
		}
		private readonly Harmony harmony = new Harmony("dsrdakota.FacilityOrMansion");
	}
}
