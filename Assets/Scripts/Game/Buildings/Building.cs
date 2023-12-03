using System;
using System.Collections.Generic;
using Game.Resources;
using UnityEngine;

namespace Game.Buildings
{
    public class LevelConfig
    {
        public int maxHealth;
        public ResourceConfig cost;
        public int buildTime;
    }

    public class BuildingConfig
    {
        public int maxLevel;
        public List<LevelConfig> levels;
    }

    public abstract class Building : MonoBehaviour
    {
        public abstract void LoadBuildingConfig();
        public abstract void LevelUp();
        public abstract void Build();
        public abstract void Destroy();
        
        
        public static BuildingConfig Config { get; protected set; }
        public static readonly BuildingType Type;
        
        public int Level { get; protected set; }
        public int Health { get; protected set; }

        public String TypeToString()
        {
            switch (Type)
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
            return Type;
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