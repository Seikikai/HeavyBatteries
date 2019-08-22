using Harmony;
using System;
using System.Collections.Generic;

namespace HeavyBatteriesMod
{
	public class HeavyBatteriesMod_OnLoad
	{
		public static void OnLoad(string modPath)
		{ ConfigFile.loadFile(modPath); }
	}

	[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
	public class HeavyBatteriesMod
	{
		public static void Prefix()
		{
			Debug.Log("HeavyBatteries - Prefix: ");

			Strings.Add("STRINGS.BUILDINGS.PREFABS.HEAVYBATTERY.NAME", "Heavy Battery");
			Strings.Add("STRINGS.BUILDINGS.PREFABS.HEAVYBATTERY.DESC", "Heavy batteries hold lots of power and keep systems running longer before recharging.");
			Strings.Add("STRINGS.BUILDINGS.PREFABS.HEAVYBATTERY.EFFECT", "Stores most runoff " + STRINGS.UI.FormatAsLink("Power", "POWER") + " from generators, but loses charge over time.");

			ModUtil.AddBuildingToPlanScreen("Power", HeavyBatteryConfig.ID);

			Strings.Add("STRINGS.BUILDINGS.PREFABS.HEAVYSMARTBATTERY.NAME", "Heavy Smart Battery");
			Strings.Add("STRINGS.BUILDINGS.PREFABS.HEAVYSMARTBATTERY.DESC", "Heavy smart batteries can send an automation signal while they require charging.");
			Strings.Add("STRINGS.BUILDINGS.PREFABS.HEAVYSMARTBATTERY.EFFECT", string.Concat(new string[]
			{
				"Stores most runoff ", STRINGS.UI.FormatAsLink("Power", "POWER"),
				" from generators, but loses charge over time.\n\nSends an ", STRINGS.UI.FormatAsLink("Active", "LOGIC"),
				" or ", STRINGS.UI.FormatAsLink("Standby", "LOGIC"),
				" signal based on the configuration of the Logic Activation Parameters."
			}));

			ModUtil.AddBuildingToPlanScreen("Power", HeavySmartBatteryConfig.ID);

			if (ConfigFile.config.emergencyBatteryEnabled)
			{
				Strings.Add("STRINGS.BUILDINGS.PREFABS.EMERGENCYBATTERY.NAME", "Emergency Battery");
				Strings.Add("STRINGS.BUILDINGS.PREFABS.EMERGENCYBATTERY.DESC", "Emergency batteries can send an automation signal while they require charging.");
				Strings.Add("STRINGS.BUILDINGS.PREFABS.EMERGENCYBATTERY.EFFECT", string.Concat(new string[]
				{
					"Stores half of runoff ", STRINGS.UI.FormatAsLink("Power", "POWER"),
					" from generators, but never loses charge.\n\nSends an ", STRINGS.UI.FormatAsLink("Active", "LOGIC"),
					" or ", STRINGS.UI.FormatAsLink("Standby", "LOGIC"),
					" signal based on the configuration of the Logic Activation Parameters."
				}));

				ModUtil.AddBuildingToPlanScreen("Power", EmergencyBatteryConfig.ID);
			}

			Strings.Add("STRINGS.BUILDINGS.PREFABS.HEAVYTRANSFORMER.NAME", "Heavy Transformer");
			Strings.Add("STRINGS.BUILDINGS.PREFABS.HEAVYTRANSFORMER.DESC", "DO NOT TOUCH! SERIOUSLY!!!");
			Strings.Add("STRINGS.BUILDINGS.PREFABS.HEAVYTRANSFORMER.EFFECT", string.Concat(new string[]
			{
				"Protects circuits from overloading by increasing or decreasing ", STRINGS.UI.FormatAsLink("Power", "POWER"),
				" flow.\n\nStores a great deal of ", STRINGS.UI.FormatAsLink("Power", "POWER"),
				"."
			}));

			ModUtil.AddBuildingToPlanScreen("Power", HeavyTransformerConfig.ID);
	}	}

	[HarmonyPatch(typeof(Db), "Initialize")]
	public class HeavyBatteriesTech
	{
		public static void Prefix(Db __instance)
		{
			Debug.Log("Heavy Batteries - Loaded: ");

			List<string> list = new List<string>(Database.Techs.TECH_GROUPING["DupeTrafficControl"]);
			list.Add(HeavyBatteryConfig.ID);
			list.Add(HeavySmartBatteryConfig.ID);
			list.Add(HeavyTransformerConfig.ID);

			if (ConfigFile.config.emergencyBatteryEnabled)
			{ list.Add(EmergencyBatteryConfig.ID); }

			Database.Techs.TECH_GROUPING["DupeTrafficControl"] = list.ToArray();
		}
	}

	[HarmonyPatch(typeof(Building), "OnSpawn")]
	public class HeavyBatteriesOnSpawn
	{
		public static void Postfix(Building __instance)
		{
			string[] batteries = { "HeavyBattery", "HeavySmartBattery", "HeavyTransformer", "EmergencyBattery" };

			if (__instance.name.Contains(batteries[0]) || __instance.name.Contains(batteries[1]) || __instance.name.Contains(batteries[3]))
			{
				__instance.gameObject.GetComponent<KBatchedAnimController>().animHeight = 2f;
				__instance.gameObject.GetComponent<KBatchedAnimController>().animWidth = 2f;
			}
			else if (__instance.name.Contains(batteries[2]))
			{
				__instance.gameObject.GetComponent<KBatchedAnimController>().animHeight = 2f;
				__instance.gameObject.GetComponent<KBatchedAnimController>().animWidth = 1.5f;
	}	}	}


	[HarmonyPatch(typeof(Battery), "AddEnergy", new Type[] { typeof(float) })]
	public class HeavyBatteriesEmergencyBatteryEnergy
	{
		public static void Prefix(Battery __instance, ref float joules)
		{
			if (__instance.gameObject.GetComponent<KPrefabID>().PrefabTag == EmergencyBatteryConfig.ID)
			{ joules = joules / 2; }
		}
	}

}