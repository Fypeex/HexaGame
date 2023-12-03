using System.Collections.Generic;

namespace Game.Buildings.Extractors
{
    public class ExtractorConfig : BuildingConfig
    {
        public new List<ExtractorLevelConfig> levels;
    }
    public class ExtractorLevelConfig : LevelConfig
    {
        public int extractionRatePerTick;
    }
    
    public abstract class Extractor : Building
    {
        public abstract int Extract();
    }
}