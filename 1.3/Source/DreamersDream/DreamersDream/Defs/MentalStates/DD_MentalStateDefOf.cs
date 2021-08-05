using RimWorld;

namespace DreamersDream
{
    [DefOf]
    public static class DD_MentalStateDefOf
    {
        static DD_MentalStateDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DD_MentalStateDefOf));
        }

        public static DD_MentalStateDef Sleepwalk;

        public static DD_MentalStateDef SleepwalkBerserk;

        public static DD_MentalStateDef SleepwalkSafe;

        public static DD_MentalStateDef SleepwalkTantrum;

        public static DD_MentalStateDef SleepwalkBingingFood;

        //public static DD_MentalStateDef SleepwalkFireStarter;

        public static DD_MentalStateDef SleepwalkOwnRoom;

        // public static MentalStateDef Binging_DrugExtreme;
    }
}