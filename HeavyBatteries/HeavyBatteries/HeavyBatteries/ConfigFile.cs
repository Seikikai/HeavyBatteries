using System.IO;
using Newtonsoft.Json;

namespace HeavyBatteriesMod
{
	public class ConfigFile
	{
		public static void loadFile(string modPath)
		{ config = loadFile<ConfigFile>(Path.Combine(modPath, "Config.json")); }

		protected static T loadFile<T>(string path)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				ObjectCreationHandling = ObjectCreationHandling.Replace
			});

			T result;

			using (StreamReader streamReader = new StreamReader(path))
			{
				using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
				{
					result = jsonSerializer.Deserialize<T>(jsonTextReader);
					jsonTextReader.Close();
				}

				streamReader.Close();
			}

			return result;
		}

		public static ConfigFile config;

		public int heavyBatteryCost { get; set; } = 800;
		public double heavyBatterySelfHeat { get; set; } = 0.5;
		public double heavyBatteryHeatExhaust { get; set; } = 2.0;
        		public int heavyBatteryCapacity { get; set; } = 320000;


		public int heavySmartBatteryCost { get; set; } = 650;
		public double heavySmartBatterySelfHeat { get; set; } = 0.0;
		public double heavySmartBatteryHeatExhaust { get; set; } = 0.8;
		public int heavySmartBatteryCapacity { get; set; } = 160000;

		public bool emergencyBatteryEnabled { get; set; } = true;


		public int emergencyBatteryCostM { get; set; } = 700;
		public int emergencyBatteryCostI { get; set; } = 200;
		public int emergencyBatteryCostC { get; set; } = 150;


		public int emergencyBatteryCapacity { get; set; } = 25000;
		public int heavyTransformerCostM { get; set; } = 600;
		public int heavyTransformerCostC { get; set; } = 100;
		public int heavyTransformerCostI { get; set; } = 100;
	}
}