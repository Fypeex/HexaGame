using Game.Buildings;
using Game.Buildings.Extractors;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Shop
{
    public class WaterPumpPanel : MonoBehaviour
    {
        [field: SerializeField] public GameObject WaterPumpPrefab;

        public void Buy()
        {
            HexTile position = GameManager.Instance.ActiveTile;
            
            if (position == null)
            {
                return;
            }

            if (position.GetBuilding() != null)
            {
                return;
            }

            if (position.Type != Resourcetype.WATER)
            {
                return;
            }

            if (GameManager.Instance.Money < 100)
            {
                return;
            }

            GameManager.Instance.Money -= 100;

            WaterPump extractor = Instantiate(WaterPumpPrefab, position.transform, false).GetComponent<WaterPump>();
            
            extractor.Build();
            
            position.SetBuilding(extractor, BuildingType.EXTRACTOR);

            GameManager.Instance.AddBuilding(extractor, BuildingType.EXTRACTOR);
            GameManager.Instance.SetActiveTile(position);
        }
    }
}