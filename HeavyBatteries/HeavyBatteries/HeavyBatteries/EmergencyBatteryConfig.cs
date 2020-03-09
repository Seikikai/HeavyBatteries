using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace HeavyBatteriesMod
{
    public class EmergencyBatteryConfig : BaseBatteryConfig
    {
        public const string ID = "EmergencyBattery";



        public override BuildingDef CreateBuildingDef()
        {
            string id = "EmergencyBattery";
            int width = 3;
            int height = 5;
            int hitpoints = 250;
            string anim = "batterylg_kanim";
            float construction_time = 320f;
            float[] tIER ={
                ConfigFile.config.emergencyBatteryCostM,
                ConfigFile.config.emergencyBatteryCostI,
                ConfigFile.config.emergencyBatteryCostC
            };
            string[] mATERIALS =
            {
                "RefinedMetal",
                SimHashes.SuperInsulator.ToString(),
                SimHashes.Ceramic.ToString()
            };
            float melting_point = 850f;
            float exhaust_temperature_active = 0f;
            float self_heat_kilowatts_active = 0f;

            EffectorValues tIER2 = NOISE_POLLUTION.NOISY.TIER2;

            BuildingDef buildingDef = CreateBuildingDef(id, width, height, hitpoints, anim, construction_time, tIER, mATERIALS, melting_point, exhaust_temperature_active, self_heat_kilowatts_active, TUNING.BUILDINGS.DECOR.PENALTY.TIER2, tIER2);
            buildingDef.PermittedRotations = PermittedRotations.Unrotatable;

            SoundEventVolumeCache.instance.AddVolume("batterymed_kanim", "Battery_med_rattle", NOISE_POLLUTION.NOISY.TIER2);
            buildingDef.LogicOutputPorts = new List<LogicPorts.Port>()
    {
      LogicPorts.Port.OutputPort(BatterySmart.PORT_ID, new CellOffset(0, 0), (string) STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT, (string) STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_ACTIVE, (string) STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_INACTIVE, true, false)
    };
            return buildingDef;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        { GeneratedBuildings.RegisterSingleLogicInputPort(go); }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        { GeneratedBuildings.RegisterSingleLogicInputPort(go); }

        public override void DoPostConfigureComplete(GameObject go)
        {
            BatterySmart emergencyBattery = go.AddOrGet<BatterySmart>();
            emergencyBattery.capacity = ConfigFile.config.emergencyBatteryCapacity;
            emergencyBattery.joulesLostPerSecond = 0f;
            emergencyBattery.powerSortOrder = 1500;

            GeneratedBuildings.RegisterSingleLogicInputPort(go);

            base.DoPostConfigureComplete(go);
        }

    }
}