using TUNING;
using UnityEngine;

namespace HeavyBatteriesMod
{
	public class HeavyTransformerConfig : IBuildingConfig
	{
		public const string ID = "HeavyTransformer";

		public override BuildingDef CreateBuildingDef()
		{
			string id = "HeavyTransformer";
			int width = 5;
			int height = 4;
			string anim = "transformer_kanim";
			int hitpoints = 45;
			float construction_time = 50f;
			float[] tier =
			{
				ConfigFile.config.heavyTransformerCostM,
				ConfigFile.config.heavyTransformerCostC,
				ConfigFile.config.heavyTransformerCostI
			};
			string[] materials =
			{
				"RefinedMetal",
				SimHashes.Copper.ToString(),
				SimHashes.SuperInsulator.ToString()
			};
			float melting_point = 800f;
			BuildLocationRule build_location_rule = BuildLocationRule.OnFloor;
			EffectorValues noise = NOISE_POLLUTION.NOISY.TIER6;
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tier, materials, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER1, noise, 0.2f);
			buildingDef.RequiresPowerInput = true;
			buildingDef.UseWhitePowerOutputConnectorColour = true;
			buildingDef.PowerInputOffset = new CellOffset(0, 1);
			buildingDef.PowerOutputOffset = new CellOffset(1, 0);
			buildingDef.ElectricalArrowOffset = new CellOffset(1, 0);
			buildingDef.ExhaustKilowattsWhenActive = 2f;
			buildingDef.SelfHeatKilowattsWhenActive = 3f;
			buildingDef.ViewMode = OverlayModes.Power.ID;
			buildingDef.AudioCategory = "Metal";
			buildingDef.Entombable = true;
			buildingDef.GeneratorWattageRating = 12000f;
			buildingDef.GeneratorBaseCapacity = 12000f;
			buildingDef.PermittedRotations = PermittedRotations.FlipH;

			return buildingDef;
		}

		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
			go.AddComponent<RequireInputs>();
			BuildingDef def = go.GetComponent<Building>().Def;
			Battery battery = go.AddOrGet<Battery>();
			battery.powerSortOrder = 1000;
			battery.capacity = def.GeneratorWattageRating;
			battery.chargeWattage = def.GeneratorWattageRating;
			PowerTransformer powerTransformer = go.AddComponent<PowerTransformer>();
			powerTransformer.powerDistributionOrder = 9;
		}

		public override void DoPostConfigureComplete(GameObject go)
		{
			UnityEngine.Object.DestroyImmediate(go.GetComponent<EnergyConsumer>());
			go.AddOrGetDef<PoweredActiveController.Def>();
		}

}	}