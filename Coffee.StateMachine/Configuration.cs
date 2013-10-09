namespace Coffee.StateMachine
{
    public static class Configuration
    {
        public const int MaxHistorySize = 10;

        public const int BrewingTime = (6 * /*60 */ 1000) + (10 * 1000); // 6 minutes + 10 sek

        public const int WeightWithFilterWithoutCan = 2500;
        public const int WeightWithFilterWithoutCanMin = WeightWithFilterWithoutCan - 100;
        public const int WeightWithFilterWithoutCanMax = WeightWithFilterWithoutCan + 100;

        public const int WeightWithFilterAndFullCan = 4000;
        public const int WeightWithFilterAndFullCanMax = WeightWithFilterAndFullCan + 100;
        public const int WeightWithFilterAndFullCanMin = WeightWithFilterAndFullCan - 200;

        public const int WeightWithFilterAndEmptyCan = 2900;
        public const int WeightWithFilterAndEmptyCanMin = WeightWithFilterAndEmptyCan - 100;
        public const int WeightWithFilterAndEmptyCanMax = WeightWithFilterAndEmptyCan + 100;

        public const int OneCupWeight = 110;
    }
}
