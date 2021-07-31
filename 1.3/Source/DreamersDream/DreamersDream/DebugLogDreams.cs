namespace DreamersDream
{
    public static class DebugLogDreams
    {
        public static void DebugLogChances()
        {
            foreach (var qualityDef in PawnDreamTracker.GetDreamQualities)
            {
                //Log.Message("Chances for " + qualityDef.Key.label + " dream are " + qualityDef.Value);
            }

            /* Temporary holder for debug commands
             * Log.Message("It is " + PawnDreamTracker.listOfAllDreamDefs.IndexOf(dream) + " dream on the list.");
             * Log.Message("With chance of " + dream.chance + ".");
             * Log.Message("Loaded " + dreamQuality.defName + ".");
             * Log.Message("Loaded " + dream.defName + " with quality of " + dream.quality.label + ".");
             */
        }
    }
}