using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class PawnDreamProvider
    {
        public static DreamDef ProvideDream()
        {
            var dreamQuality = GetRandomDreamCategory();

            return null;
        }

        private static DreamQualityDef GetRandomDreamCategory()
        {
            return null;
        }

        private static DreamDef GetRandomDream()
        {
            var dreamChanceProgress = 0.0f;

            float dreamChanceRoll = GetRandomNumberForThisDreamQuality();

            foreach (DreamDef dream in PawnDreamTracker.listOfAllDreamDefs)
            {
                var chanceForDream = dream.chance; //DD_Utility.CheckDreamChance(dream, pawn);

                if (dreamChanceRoll < dreamChanceProgress + chanceForDream)
                {
                    return dream;
                }
                else
                {
                    dreamChanceProgress += chanceForDream;
                }
            }
            Log.Error("ChooseDream() was called but it did not select a dream.");
            return null;
        }

        private static float GetRandomNumberForThisDreamQuality()
        {
            float totalDreamChance = 0;
            foreach (DreamDef dream in PawnDreamTracker.listOfAllDreamDefs)
            {
                totalDreamChance += dream.chance;         //DD_Utility.CheckDreamChance(dream, pawn);
            }
            return Rand.Range(0, totalDreamChance);
        }
    }
}