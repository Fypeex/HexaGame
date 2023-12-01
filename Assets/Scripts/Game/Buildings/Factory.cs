namespace Game.Buildings
{
    public class Factory : Building
    {
        private static int MAX_LEVEL = 3;
        private static int MAX_HEALTH = 100;

        private static int HEALTH_GROWTH = 200;

        public Factory() : base(MAX_LEVEL, MAX_HEALTH, BuildingType.FACTORY) {}
    }
}