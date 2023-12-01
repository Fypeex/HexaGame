using System;
using UnityEngine;

namespace Game.Buildings
{
    public class Building : MonoBehaviour
    {

        public readonly int MaxLevel;
        private int _health;
        private int _maxHealth;
        private readonly BuildingType _type;
        public int Level { get; set;  }

        public String TypeAsString() {
            switch (_type)
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

        public Building(int maxLevel, int maxHealth, BuildingType type)
        {
            MaxLevel = maxLevel;
            _health = maxHealth;
            Level = 1;
            _maxHealth = maxHealth;
            _type = type;
            Level = 1;
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