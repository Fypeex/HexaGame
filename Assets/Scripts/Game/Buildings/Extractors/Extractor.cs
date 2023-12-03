using System;

namespace Game.Buildings.Extractors
{
    [Serializable]
    public class ExtractorConfig : BuildingConfig
    {
        public new ExtractorLevelConfig[] levels;
    }

    [Serializable]
    public class ExtractorLevelConfig : LevelConfig
    {
        public int extractionRatePerTick;
    }

    public abstract class Extractor : Building
    {
        public abstract int Extract();
        public static readonly ExtractorType ExtractorType = ExtractorType.EMPTY; 
        public abstract ExtractorType GetExtractorType();
    }
    
    public enum ExtractorType
    {
        WATER_PUMP,
        SAW_MILL,
        EMPTY
    }
}