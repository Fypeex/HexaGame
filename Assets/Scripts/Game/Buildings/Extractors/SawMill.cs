using System.IO;
using UnityEngine;

namespace Game.Buildings.Extractors
{
    public class SawMill : Extractor
    {
        public static string ConfigPath = "/Resources/Configs/Buildings/Extractors/SawMillConfig.json";
        public new static readonly BuildingType BuildingType = BuildingType.EXTRACTOR;
        public new static readonly ExtractorType ExtractorType = ExtractorType.SAW_MILL;
        
        public new static ExtractorConfig Config { get; set; }

        private void Awake()
        {
            LoadBuildingConfig();
        }

        public override void LoadBuildingConfig()
        {
            var json = File.ReadAllText(Application.dataPath + ConfigPath);
            Config = JsonUtility.FromJson<ExtractorConfig>(json);
        }

        public override void LevelUp()
        {
            if (Level >= Config.maxLevel) return;


            Level++;
            Health = Config.levels[Level - 1].maxHealth;
        }

        public override void Build()
        {
            Level = 1;
            Health = Config.levels[Level - 1].maxHealth;
        }

        public override void Destroy()
        {
            Level = 0;
            Health = 0;
        }

        public override int Extract()
        {
            return Config.levels[Level - 1].extractionRatePerTick;
        }

        public override ExtractorType GetExtractorType()
        {
            return ExtractorType;
        }
    }
}