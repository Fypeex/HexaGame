﻿using Game.Buildings;
using Game.Buildings.Extractors;
using UnityEngine;

namespace Game.Shop
{
    public class WaterPumpPanel : MonoBehaviour
    {
        [field: SerializeField] public GameObject WaterPumpPrefab;

        public void Buy()
        {
            var position = GameManager.Instance.ActiveTile;

            if (position == null) return;

            if (position.GetBuilding() != null) return;

            if (position.Type != Resourcetype.WATER) return;

            if (GameManager.Instance.Money < 100) return;

            GameManager.Instance.Money -= 100;

            var extractor = Instantiate(WaterPumpPrefab, position.transform, false).GetComponent<WaterPump>();

            extractor.Build();

            position.SetBuilding(extractor, BuildingType.EXTRACTOR);

            GameManager.Instance.AddBuilding(extractor, BuildingType.EXTRACTOR);
            GameManager.Instance.SetActiveTile(position);
        }

        public void Upgrade()
        {
            var position = GameManager.Instance.ActiveTile;

            if (position == null) return;

            if (position.GetBuilding() == null) return;

            if (position.Type != Resourcetype.WATER) return;
            
            if (GameManager.Instance.Money < 100) return;
            
            GameManager.Instance.Money -= 100;

            var extractor = position.GetBuilding();
            
            extractor.LevelUp();
        }
    }
}