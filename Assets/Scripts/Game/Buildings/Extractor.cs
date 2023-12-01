namespace Game.Buildings
{
    public class Extractor : Building
    {
        public Resourcetype Type { get; private set; }
        private int _extractRate;


        private static int MAX_LEVEL = 3;
        private static int MAX_HEALTH = 100;
        private static int HEALTH_GROWTH = 200;

        public Extractor(
            Resourcetype type,
            int extractRate
        ) : base(MAX_LEVEL, MAX_HEALTH, BuildingType.FACTORY)
        {
            Type = type;
            _extractRate = extractRate;
        }

        public int Extract()
        {
            return _extractRate;
        }
    }
}