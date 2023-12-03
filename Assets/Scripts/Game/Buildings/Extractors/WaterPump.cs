using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game.Buildings.Extractors
{
    [Serializable]
    public class WaterPump : Extractor
    {
        private void Awake()
        {
            LoadBuildingConfig();
        }

        public static string ConfigPath = "/Resources/Configs/Buildings/Extractors/WaterPumpConfig.json";
        
        public new static ExtractorConfig Config { get; set; }
        public new static readonly BuildingType Type = BuildingType.EXTRACTOR;
        
        public override void LoadBuildingConfig()
        {
            string json = File.ReadAllText(Application.dataPath + ConfigPath);
            Config = JsonUtility.FromJson<ExtractorConfig>(json);
        }
        
        public override void LevelUp()
        {
            if (Level >= Config.maxLevel) return;
            
            
            Level++;
            Health = Config.levels[Level].maxHealth;
        }
        
        public override void Build()
        {
            Level = 1;
            Health = Config.levels[Level].maxHealth;
        }
        
        public override void Destroy()
        {
            Level = 0;
            Health = 0;
        }
        
        public override int Extract()
        {
            return Config.levels[Level].extractionRatePerTick;
        }
        
    }
}