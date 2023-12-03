using System;
using System.Collections.Generic;
using Game;
using Game.Buildings;
using Game.Buildings.Extractors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BuildMenu : MonoBehaviour
{
    public void Start()
    {
        _content = new List<GameObject>();
    }

    [field: SerializeField] public TextMeshProUGUI InfoText;
    [field: SerializeField] public GameObject Shop;
    [field: SerializeField] public GameObject WaterPumpPanel;

    private GameObject _shop;
    private List<GameObject> _content;


    public void Show(HexTile tile)
    {
        var tileInfo = GetTileInfo(tile);

        if (tile.Type == Resourcetype.WATER)
        {
            GameObject panel = Instantiate(Shop, transform, false);
            panel.transform.localScale = Vector3.one;
            panel.transform.localPosition = Vector3.zero;
            _shop = panel;


            if (tileInfo.IsOccupied)
            {
                //Extractor is built
            }
            else
            {
                //Extractor is not built
                GameObject waterPumpPanel = Instantiate(WaterPumpPanel,
                    panel.GetComponentInChildren<ScrollRect>().content.transform, false);
                if (_content == null)
                {
                    _content = new List<GameObject> {waterPumpPanel};
                }
                else
                {
                    _content.Add(waterPumpPanel);
                }
            }
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        if (_shop != null)
        {
            Destroy(_shop);
        }

        if (_content != null)
        {
            foreach (var item in _content)
            {
                Destroy(item);
            }
        }
    }


    private static TileInfo GetTileInfo(HexTile tile)
    {
        return new TileInfo(tile);
    }

    private class TileInfo
    {
        public readonly Resourcetype Type;
        public readonly bool IsOccupied;
        public bool IsBuildable;
        public bool IsExtractable;
        public Building Building;
        public Factory Factory;
        public Extractor Extractor;
        public Defense Defense;
        public BuildingType BuildingType;

        public TileInfo(HexTile tile)
        {
            Type = tile.Type;
            BuildingType = tile.GetBuildingType();
            Building = tile.GetBuilding();
            switch (Type)
            {
                case Resourcetype.OIL:
                case Resourcetype.WATER:
                    IsBuildable = false;
                    IsExtractable = true;
                    IsOccupied = tile.GetBuilding() != null;
                    break;

                case Resourcetype.GRASS:
                    IsBuildable = true;
                    IsExtractable = false;
                    IsOccupied = tile.GetBuilding() != null;
                    break;

                case Resourcetype.STONE:
                case Resourcetype.IRON:
                case Resourcetype.COAL:
                case Resourcetype.GOLD:
                    IsBuildable = true;
                    IsExtractable = true;
                    IsOccupied = tile.GetBuilding() != null;
                    break;
            }

            if (!IsOccupied)
            {
                Factory = null;
                Extractor = null;
                Defense = null;
            }
            else
            {
                switch (tile.GetBuildingType())
                {
                    case BuildingType.FACTORY:
                        Extractor = null;
                        Defense = null;
                        Factory = tile.GetBuilding() as Factory;
                        break;
                    case BuildingType.EXTRACTOR:
                        Factory = null;
                        Defense = null;
                        Extractor = tile.GetBuilding() as Extractor;
                        break;
                    case BuildingType.DEFENSE:
                        Factory = null;
                        Extractor = null;
                        Defense = tile.GetBuilding() as Defense;
                        break;
                    default:
                        Factory = null;
                        Extractor = null;
                        Defense = null;
                        break;
                }
            }
        }
    }
}