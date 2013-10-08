namespace Coffee.StateMachine
{
    public static class Configuration
    {
        public const int MaxHistorySize = 10;

        public const int BrewingTime = 6 * 60 * 1000; // 6 minutes

        public const int CanWeight = 200;
        public const int CoffeWeight = 500;
        public const int WetFilterWeight = 300;


        public const int WeightWithFilterWithoutCan = 1800;
        public const int WeightWithoutFilterAndCan = 1500;

        public const int WeightWithFilterAndFullCan = 4000;
        public const int WeightWithFilterAndFullCanMax = WeightWithFilterAndFullCan + 200;
        public const int WeightWithFilterAndFullCanMin = WeightWithFilterAndFullCan - 200;


        public const int WeightWithFilterAndEmptyCan = 2000;
        public const int WeightWithFilterAndEmptyCanMin = WeightWithFilterAndEmptyCan - 60;
        public const int WeightWithFilterAndEmptyCanMax = WeightWithFilterAndEmptyCan + 60;
    }
}
