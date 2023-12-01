using System.Collections.Generic;
using Game.Buildings;
using Game.Resources;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private enum DevelopmentState
        {
            DEVELOPMENT,
            PRODUCTION
        }

        private DevelopmentState Development { get; set; } = DevelopmentState.DEVELOPMENT;

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Create a new GameObject and attach a GameManager to it
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        private void Start()
        {
            _hexTiles = new List<HexTile>();
            _gameState = GameState.PLAYING;
            _buildMenu = FindObjectOfType<BuildMenu>();
            _buildingInfo = FindObjectOfType<BuildingInfo>();

            _buildMenu.Hide();
           // _buildingInfo.Hide();

            Debug.Log("GameManager started");
            InvokeRepeating(nameof(Tick), 2f, 1f);
            Debug.Log("GameManager tick started");
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

        /*
         * Managable entities:
         * - Extractors
         * - Enemies
         * - Resources
         * - Factories
         * - Defenses
         * - HexTiles
         * - GameState
         *
         * - Active HexTile
         * - UI State
         * - Money
         *
         */

        //private List<Enemy> _enemies;
        private List<HexTile> _hexTiles;
        private GameState _gameState;
        private BuildMenu _buildMenu;
        private BuildingInfo _buildingInfo;

        private int _money;

        public int Money
        {
            get => DevelopmentState.DEVELOPMENT == Development ? 1000000 : _money;

            set
            {
                if (value < 0) return;
                _money = value;
            }
        }


        public HexTile ActiveTile { get; private set; }


        public bool IsRunning => _gameState == GameState.PLAYING;

        public void AddTile(HexTile tile)
        {
            _hexTiles.Add(tile);
        }

        public void AddBuilding(Building building, BuildingType type)
        {
            BuildingManager.Instance.AddBuilding(building, type);
        }

        public void SetActiveTile(HexTile tile)
        {
            if (ActiveTile != null)
                ActiveTile.SetInactive();

            tile.SetActive();

            ActiveTile = tile;
        }

        public void ClearActiveTile()
        {
            if (ActiveTile != null)
                ActiveTile.SetInactive();

            ActiveTile = null;
        }

        public void ShowBuildMenu(HexTile tile)
        {
            _buildMenu.Show(tile);
        }

        public void HideBuildMenu()
        {
            _buildMenu.Hide();
        }

        private void Tick()
        {
            ResourceManager.Instance.Tick();
            BuildingManager.Instance.Tick();
        }
    }
}