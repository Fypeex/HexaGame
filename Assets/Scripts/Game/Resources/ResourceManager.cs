using System.Numerics;
using Game.Buildings;
using TMPro;
using UnityEngine;

namespace Game.Resources 
{
    public class ResourceManager : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI WaterText { get; set; }
        
        private static ResourceManager _instance;
        public static ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Create a new GameObject and attach a GameManager to it
                    _instance = new GameObject("GameManager").AddComponent<ResourceManager>();
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

        public BigInteger Water { get; private set; } = 0;
        public BigInteger Iron { get; private set; } = 0;
        public BigInteger Gold { get; private set; } = 0;
        public BigInteger Oil { get; private set; } = 0;
        public BigInteger Coal { get; private set; } = 0;
        public BigInteger Wood { get; private set; } = 0;
        public BigInteger Stone { get; private set; } = 0;

        private void ExtractResources()
        {
            Water += BuildingManager.Instance.WaterExtractionPerTick();
        }


        private void Update()
        {
            WaterText.text = ToDisplayableString(Water);
        }

        public void Tick()
        {
            ExtractResources();
        }
        
        private string ToDisplayableString(BigInteger amount)
        {
            if(amount < 1000)
                return amount.ToString();
            if(amount < 1000000)
                return $"{amount / 1000}.{(amount / 100) % 10}K";
            if(amount < 1000000000)
                return $"{amount / 1000000}.{(amount / 100000) % 10}M";
            if(amount < 1000000000000)
                return $"{amount / 1000000000}.{(amount / 100000000) % 10}B";
            if(amount < 1000000000000000)
                return $"{amount / 1000000000000}.{(amount / 100000000000) % 10}T";
            if(amount < 1000000000000000000)
                return $"{amount / 1000000000000000}.{(amount / 100000000000000) % 10}Qa";
            if(amount < BigInteger.Parse("1000000000000000000000"))
                return $"{amount / BigInteger.Parse("1000000000000000000")}.{(amount / BigInteger.Parse("100000000000000000")) % 10}Qi";
            if(amount < BigInteger.Parse("1000000000000000000000000"))
                return $"{amount / BigInteger.Parse("1000000000000000000000")}.{(amount / BigInteger.Parse("100000000000000000000")) % 10}Sx";

            return "Too much!";
        }
    }
}