using RimWorld;

namespace DreamersDream
{
    [DefOf]
    public static class DD_MentalStateDefOf
    {
        // Token: 0x06006506 RID: 25862 RVA: 0x00232CDD File Offset: 0x00230EDD
        static DD_MentalStateDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DD_MentalStateDefOf));
        }

        // Token: 0x04003A63 RID: 14947
        public static DD_MentalStateDef Sleepwalk;

        public static DD_MentalStateDef SleepwalkBerserk;

        public static DD_MentalStateDef SleepwalkSafe;

        public static DD_MentalStateDef SleepwalkTantrum;

        //public static DD_MentalStateDef SleepwalkFireStarter;

        public static DD_MentalStateDef SleepwalkOwnRoom;

        // Token: 0x04003A64 RID: 14948
        // public static MentalStateDef Binging_DrugExtreme;

    }
}
