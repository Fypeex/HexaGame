using System;
using Game.Resources;
using UnityEngine;

namespace Game.Buildings
{
    [Serializable]
    public class LevelConfig
    {
        public int maxHealth;
        public ResourceConfig cost;
        public int buildTime;
    }

    [Serializable]
    public class BuildingConfig
    {
        public int maxLevel;
        public LevelConfig[] levels;
    }

    public abstract class Building : MonoBehaviour
    {
        public static readonly BuildingType BuildingType = BuildingType.EMPTY;


        public static BuildingConfig Config { get; protected set; }

        public int Level { get; protected set; }
        public int Health { get; protected set; }
        public abstract void LoadBuildingConfig();
        public abstract void LevelUp();
        public abstract void Build();
        public abstract void Destroy();

        public string BuildingTypeToString()
        {
            switch (BuildingType)
            {
                case BuildingType.DEFENSE:
                    return "Defense";
                case BuildingType.FACTORY:
                    return "Factory";
                case BuildingType.EXTRACTOR:
                    return "Extractor";
                default:
                    return "Empty";
            }
        }

        public BuildingType GetBuildingType()
        {
            return BuildingType;
        }
    }


    public enum BuildingType
    {
        FACTORY,
        DEFENSE,
        EXTRACTOR,
        EMPTY
    }
}