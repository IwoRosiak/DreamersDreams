using Verse;

namespace DreamersDream
{
    internal static class DreamChanceCalc
    {
        public static float CalculateChanceFor(this DreamDef dream, Pawn pawn)
        {
            float dreamChance = dream.chance;

            if (dream.isSleepwalk)
            {
                if (pawn.story.traits.HasTrait(DD_TraitDefOf.Sleepwalker))
                {
                    dreamChance *= GetSleepwalkerMultiplier(pawn);
                }
                else if (DD_Settings.canNonSleepwalkerSleepwalk)
                {
                    dreamChance = 0.01f;
                }
                else
                {
                    dreamChance = 0;
                }
            }

            return dreamChance;
        }

        public static float CalculateChanceFor(this DreamQualityDef dreamQuality, Pawn pawn = null)
        {
            if (DD_Settings.QualityChanceModifs.ContainsKey(dreamQuality.defName) && !DD_Settings.isDefaultSettings)
            {
                return DD_Settings.QualityChanceModifs[dreamQuality.defName];
            }
            else
            {
                return dreamQuality.chance;
            }
        }

        private static float GetSleepwalkerMultiplier(Pawn pawn)
        {
            float traitMultiplier = DD_Settings.sleepwalkerTraitModif;
            switch (pawn.story.traits.DegreeOfTrait(DD_TraitDefOf.Sleepwalker))
            {
                case 1:
                    traitMultiplier *= 20f;
                    break;

                case 2:
                    traitMultiplier *= 50f;
                    break;

                case 3:
                    traitMultiplier *= 100f;
                    break;
            }

            return traitMultiplier;
        }
    }
}