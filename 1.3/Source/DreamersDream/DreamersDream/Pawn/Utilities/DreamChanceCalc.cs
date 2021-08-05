using Verse;

namespace DreamersDream
{
    internal static class DreamChanceCalc
    {
        public static float CalculateChanceFor(this DreamDef dream, Pawn pawn = null)
        {
            return dream.chance;
        }

        public static float CalculateChanceFor(this DreamQualityDef dreamQuality, Pawn pawn = null)
        {
            return dreamQuality.chance;
        }

        private static float CheckSleepwalkerTrait(Pawn pawn)
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

                default:
                    break;
            }

            return traitMultiplier;
        }
    }
}