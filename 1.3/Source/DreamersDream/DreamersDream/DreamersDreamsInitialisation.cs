﻿using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    internal static class DreamersDreamsInitialisation
    {
        static DreamersDreamsInitialisation()
        {
            LoadDreams();
            LoadDreamQualities();
            //Log.Message(PawnDreamRandomCalc.ChooseRandomDream().defName);
        }

        private static void LoadDreams()
        {
            var totalDreams = 0;

            foreach (DreamDef dream in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(DreamDef)))
            {
                if (!dream.tags.NullOrEmpty())
                {
                    totalDreams++;
                    DreamTracker.DreamDefs.Add(dream);
                }
                else
                {
                    Log.Warning("Dream " + dream.defName + " does not have category, so it will not be loaded.");
                }
            }
            Log.Message("Dreamer's Dreams: successfully loaded " + totalDreams + " dreams.");
        }

        private static void LoadDreamQualities()
        {
            var totalQualities = 0;

            foreach (DreamTagDef dreamQuality in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(DreamTagDef)))
            {
                totalQualities++;
                DreamTracker.GetDreamTags.Add(dreamQuality);
                //DreamTracker.DreamQualityDefsWithSettings.Add(dreamQuality, dreamQuality.chance);
            }
            Log.Message("Dreamer's Dreams: successfully loaded " + totalQualities + " dream qualities.");
        }
    }
}