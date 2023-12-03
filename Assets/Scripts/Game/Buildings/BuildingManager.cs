using System;
using System.Collections.Generic;
using Game.Buildings.Extractors;
using UnityEngine;

namespace Game.Buildings
{
    public class BuildingManager : MonoBehaviour
    {
        private static BuildingManager _instance;

        public static BuildingManager Instance
        {
            get
            {
                if (_instance != null) return _instance;
                // Create a new GameObject and attach a GameManager to it
                _instance = new GameObject("GameManager").AddComponent<BuildingManager>();
                DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }

        public List<WaterPump> WaterPumps { get; private set; }
        public List<SawMill> SawMills { get; private set; }
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            WaterPumps = new List<WaterPump>();
            SawMills = new List<SawMill>();
        }

        public int WaterExtractionPerTick()
        {
            var amount = 0;
            foreach (var waterPump in WaterPumps) amount += waterPump.Extract();

            return amount;
        }

        public int WoodExtractionPerTick()
        {
            var amount = 0;
            foreach (var sawMill in SawMills) amount += sawMill.Extract();

            return amount;
        }
        
        public void Tick()
        {
        }

        public void AddBuilding(Building building, BuildingType type)
        {
            switch (type)
            {
                case BuildingType.EXTRACTOR:
                    switch (((Extractor) building).GetExtractorType())
                    {
                        case ExtractorType.WATER_PUMP:
                            WaterPumps.Add((WaterPump) building);
                            break;
                        case ExtractorType.SAW_MILL:
                            SawMills.Add((SawMill) building);
                            break;
                    }
                    break;
                case BuildingType.FACTORY:
                    break;
                case BuildingType.DEFENSE:
                    break;
                case BuildingType.EMPTY:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}