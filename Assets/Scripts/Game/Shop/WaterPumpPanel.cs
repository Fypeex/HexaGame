using Game.Buildings;
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
            Debug.Log($"Trying to buy water pump on {position}. " +
                      $"Already occupied: {position.GetBuilding() != null} " +
                      $"Money: {GameManager.Instance.Money}" +
                      $"Type: {position.Type}"
            );
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
            Debug.Log($"Bought water pump on {position}  {extractor.Type}");

            position.SetBuilding(extractor, BuildingType.EXTRACTOR);

            GameManager.Instance.AddBuilding(extractor, BuildingType.EXTRACTOR);
            GameManager.Instance.SetActiveTile(position);
        }
    }
}