using TUNING;
using UnityEngine;

namespace HeavyBatteriesMod
{
	public class HeavyBatteryConfig : BaseBatteryConfig
	{
		public const string ID = "HeavyBattery";

		public override BuildingDef CreateBuildingDef()
		{
			string id = "HeavyBattery";
			int width = 4;
			int height = 4;
			int hitpoints = 75;
			string anim = "batterymed_kanim";
			float construction_time = 140f;
			float[] tIER = { ConfigFile.config.heavyBatteryCost };
			string[] aLL_METALS = MATERIALS.ALL_METALS;
			float melting_point = 800f;
			float exhaust_temperature_active = (float)ConfigFile.config.heavyBatteryHeatExhaust;
			float self_heat_kilowatts_active = (float)ConfigFile.config.heavyBatterySelfHeat;

			EffectorValues tIER2 = NOISE_POLLUTION.NOISY.TIER1;

			BuildingDef result = CreateBuildingDef(id, width, height, hitpoints, anim, construction_time, tIER, aLL_METALS, melting_point, exhaust_temperature_active, self_heat_kilowatts_active, BUILDINGS.DECOR.PENALTY.TIER3, tIER2);

			SoundEventVolumeCache.instance.AddVolume("batterymed_kanim", "Battery_med_rattle", NOISE_POLLUTION.NOISY.TIER2);

			return result;
		}

		public override void DoPostConfigureComplete(GameObject go)
		{
			Battery battery = go.AddOrGet<Battery>();
			battery.capacity = ConfigFile.config.heavyBatteryCapacity;
			battery.joulesLostPerSecond = battery.capacity * 0.005f / 600f;
			base.DoPostConfigureComplete(go);
		}

	}
}