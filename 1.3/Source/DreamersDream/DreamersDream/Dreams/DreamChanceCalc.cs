using Verse;

namespace DreamersDream
{
    internal static class DreamChanceCalc
    {
        public static float CalculateChanceFor(this DreamDef dream, Pawn pawn)
        {
            float dreamChance = 0;

            if (DD_Settings.TagsCustomChances.ContainsKey(dream.tags[0].defName) && !DD_Settings.isDefaultSettings)
            {
                dreamChance = DD_Settings.TagsCustomChances[dream.tags[0].defName];
            }
            else
            {
                dreamChance = dream.tags[0].chance;
            }

            return dreamChance;
        }

        public static float CalculateChanceFor(this DreamTagDef dreamTag, Pawn pawn = null)
        {
            float chance = 0;
            if (DD_Settings.TagsCustomChances.ContainsKey(dreamTag.defName) && !DD_Settings.isDefaultSettings)
            {
                chance = DD_Settings.TagsCustomChances[dreamTag.defName];
            }
            else
            {
                chance = dreamTag.chance;
            }

            foreach (var booster in dreamTag.ChanceBoosters)
            {
                switch (booster)
                {
                    case ChanceBoosters.sleepwalker:
                        chance *= GetSleepwalkerMultiplier(pawn);
                        break;

                    default:
                        break;
                }
            }

            return chance;
        }

        private static float GetSleepwalkerMultiplier(Pawn pawn)
        {
            if (pawn == null)
            {
                return 1;
            }

            float traitMultiplier = 1;
            if (pawn?.story.traits.HasTrait(DD_TraitDefOf.Sleepwalker) == true)
            {
                traitMultiplier = DD_Settings.sleepwalkerTraitModif;
                switch (pawn.story.traits.DegreeOfTrait(DD_TraitDefOf.Sleepwalker))
                {
                    case 1:
                        traitMultiplier *= 100f;
                        break;

                    case 2:
                        traitMultiplier *= 250f;
                        break;

                    case 3:
                        traitMultiplier *= 500f;
                        break;
                }
            }
            else
            {
                traitMultiplier = 0;
            }

            return traitMultiplier;
        }
    }
}