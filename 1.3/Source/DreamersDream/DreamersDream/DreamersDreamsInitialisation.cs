using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    internal static class DreamersDreamsInitialisation
    {
        static DreamersDreamsInitialisation()
        {
            LoadDreams();
            LoadDreamQualities();
        }

        private static void LoadDreams()
        {
            var totalDreams = 0;

            foreach (DreamDef dream in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(DreamDef)))
            {
                if (dream.quality != null)
                {
                    totalDreams++;
                    PawnDreamTracker.listOfAllDreamDefs.Add(dream);
                }
                else
                {
                    Log.Warning("Dream " + dream.defName + " does not have category, so it will not be loaded.");
                }
            }
            Log.Message("Dreamer's Dreams: succesfully loaded " + totalDreams + " dreams.");
        }

        private static void LoadDreamQualities()
        {
            var totalQualities = 0;

            foreach (DreamQualityDef dreamQuality in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(DreamQualityDef)))
            {
                totalQualities++;
                PawnDreamTracker.GetDreamQualityDict.Add(dreamQuality, dreamQuality.chance);
            }
            Log.Message("Dreamer's Dreams: succesfully loaded " + totalQualities + " dream qualities.");
        }
    }
}