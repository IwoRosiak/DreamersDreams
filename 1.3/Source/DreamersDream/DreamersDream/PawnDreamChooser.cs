using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class PawnDreamChooser
    {
        public static DreamDef ProvideDream()
        {
            return null;
        }

        private static DreamDef GetRandomDream()
        {
            float totalDreamChance = 0;

            var cycle = 0;

            foreach (DreamDef dream in PawnDreamTracker.listOfAllDreamDefs)
            {
                cycle++;
                totalDreamChance += dream.chance;         //DD_Utility.CheckDreamChance(dream, pawn);
            }
            var dreamChanceRoll = Rand.Range(0, totalDreamChance);
            Log.Message(dreamChanceRoll.ToString());
            var dreamChanceProgress = 0.0f;

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

        private static DreamDef GetRandomCategory()
        {
            return null;
        }
    }
}