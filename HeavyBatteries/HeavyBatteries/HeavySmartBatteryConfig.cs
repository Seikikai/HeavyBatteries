using TUNING;
using UnityEngine;

namespace HeavyBatteriesMod
{
	public class HeavySmartBatteryConfig : BaseBatteryConfig
	{
		public const string ID = "HeavySmartBattery";

		private static readonly LogicPorts.Port[] OUTPUT_PORTS = new LogicPorts.Port[]
		{
			LogicPorts.Port.OutputPort
			(
				BatterySmart.PORT_ID, new CellOffset(0, 0),
				STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT,
				STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_ACTIVE,
				STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_INACTIVE,
				true, false
			)
		};

		public override BuildingDef CreateBuildingDef()
		{
			string id = "HeavySmartBattery";
			int width = 4;
			int height = 4;
			int hitpoints = 75;
			string anim = "smartbattery_kanim";
			float construction_time = 160f;
			float[] tIER = { ConfigFile.config.heavySmartBatteryCost };
			string[] rEFINED_METALS = MATERIALS.REFINED_METALS;
			float melting_point = 800f;
			float exhaust_temperature_active = (float)ConfigFile.config.heavySmartBatteryHeatExhaust;
			float self_heat_kilowatts_active = (float)ConfigFile.config.heavySmartBatterySelfHeat;

			EffectorValues tIER2 = NOISE_POLLUTION.NOISY.TIER1;

			BuildingDef result = CreateBuildingDef(id, width, height, hitpoints, anim, construction_time, tIER, rEFINED_METALS, melting_point, exhaust_temperature_active, self_heat_kilowatts_active, TUNING.BUILDINGS.DECOR.PENALTY.TIER3, tIER2);

			SoundEventVolumeCache.instance.AddVolume("batterymed_kanim", "Battery_med_rattle", NOISE_POLLUTION.NOISY.TIER2);

			return result;
		}

		public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
		{
			GeneratedBuildings.RegisterLogicPorts(go, null, OUTPUT_PORTS);
		}

		public override void DoPostConfigureUnderConstruction(GameObject go)
		{
			GeneratedBuildings.RegisterLogicPorts(go, null, OUTPUT_PORTS);
		}

		public override void DoPostConfigureComplete(GameObject go)
		{
			BatterySmart batterySmart = go.AddOrGet<BatterySmart>();
			batterySmart.capacity = ConfigFile.config.heavySmartBatteryCapacity;
			batterySmart.joulesLostPerSecond = batterySmart.capacity * 0.005f / 600f;
			batterySmart.powerSortOrder = 1000;
			GeneratedBuildings.RegisterLogicPorts(go, null, OUTPUT_PORTS);
			base.DoPostConfigureComplete(go);
		}
	}
}