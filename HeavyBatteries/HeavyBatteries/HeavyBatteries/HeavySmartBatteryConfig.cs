using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace HeavyBatteriesMod
{
    public class HeavySmartBatteryConfig : BaseBatteryConfig
    {
        public const string ID = "HeavySmartBattery";

      

        public override BuildingDef CreateBuildingDef()
        {
            string id = "HeavySmartBattery";
            int width = 4;
            int height = 4;
            int hitpoints = 75;
            string anim = "smartbattery_kanim";
            float construction_time = 160f;
            float[] TIER = { ConfigFile.config.heavySmartBatteryCost };
            string[] REFINED_METALS = MATERIALS.REFINED_METALS;
            float melting_point = 800f;
            float exhaust_temperature_active = (float)ConfigFile.config.heavySmartBatteryHeatExhaust;
            float self_heat_kilowatts_active = (float)ConfigFile.config.heavySmartBatterySelfHeat;

            EffectorValues TIER2 = NOISE_POLLUTION.NOISY.TIER1;

            BuildingDef buildingDef = CreateBuildingDef(id, width, height, hitpoints, anim, construction_time, TIER, REFINED_METALS, melting_point, exhaust_temperature_active, self_heat_kilowatts_active, TUNING.BUILDINGS.DECOR.PENALTY.TIER3, TIER2);

            SoundEventVolumeCache.instance.AddVolume("batterymed_kanim", "Battery_med_rattle", NOISE_POLLUTION.NOISY.TIER2);
            buildingDef.LogicOutputPorts = new List<LogicPorts.Port>()
    {
      LogicPorts.Port.OutputPort(BatterySmart.PORT_ID, new CellOffset(0, 0), (string) STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT, (string) STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_ACTIVE, (string) STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_INACTIVE, true, false)
    };
            return buildingDef;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            GeneratedBuildings.RegisterSingleLogicInputPort(go);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            GeneratedBuildings.RegisterSingleLogicInputPort(go);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            BatterySmart batterySmart = go.AddOrGet<BatterySmart>();
            batterySmart.capacity = ConfigFile.config.heavySmartBatteryCapacity;
            batterySmart.joulesLostPerSecond = batterySmart.capacity * 0.005f / 600f;
            batterySmart.powerSortOrder = 1000;
            GeneratedBuildings.RegisterSingleLogicInputPort(go);
            base.DoPostConfigureComplete(go);
        }
    }
}