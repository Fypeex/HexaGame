using System;
using System.Collections.Generic;
using Game.Buildings;
using UnityEngine;

namespace Game
{
    public class BuildingManager : MonoBehaviour
    {
        private static BuildingManager _instance;

        public static BuildingManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Create a new GameObject and attach a GameManager to it
                    _instance = new GameObject("GameManager").AddComponent<BuildingManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        void Awake()
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
        }

        public List<WaterPump> WaterPumps { get; private set; }

        public int WaterExtractionPerTick()
        {
            int amount = 0;
            foreach (var waterPump in WaterPumps)
            {
                amount += waterPump.Extract();
            }

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
                    WaterPumps.Add((WaterPump) building);
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