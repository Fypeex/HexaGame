using Game.Buildings;
using UnityEngine;

namespace Game
{
    public class BuildingInfo : MonoBehaviour
    {
        public void Show(Building building)
        {
            if (building == null)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}