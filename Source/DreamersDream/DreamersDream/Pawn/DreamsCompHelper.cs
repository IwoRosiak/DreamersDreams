using RimWorld.Planet;
using Verse;

namespace DreamersDream
{
    internal static class DreamsCompHelper
    {
        public static bool CanGetDreamNow(this Pawn pawn)
        {
            if (pawn.IsPawnCapableOfDreaming() && !pawn.IsAwake() && pawn.IsPawnRestedEnough() && !pawn.HasDreamAlready())
            {
                return true;
            }
            return false;
        }

        private static bool IsPawnCapableOfDreaming(this Pawn pawn)
        {
            return pawn.needs?.rest != null && !pawn.Dead && (pawn.Spawned || pawn.IsCaravanMember());
        }

        private static bool IsAwake(this Pawn pawn)
        {
            if (pawn.needs.rest.GUIChangeArrow == 1)
            {
                return false;
            }
            else if (pawn.needs.rest.GUIChangeArrow == -1)
            {
                return true;
            }
            return false;
        }

        private static bool IsPawnRestedEnough(this Pawn pawn)
        {
            return pawn.needs.rest.CurCategory == RimWorld.RestCategory.Rested;
        }

        private static bool HasDreamAlready(this Pawn pawn)
        {
            foreach (DreamDef dream in DreamTracker.GetAllDreams)
            {
                if (pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(dream) != null)
                {
                    return true;
                }
            }
            return false;
        }

        //Sleepwalking

        public static bool ShouldSleepwalkNow(this Pawn pawn)
        {
            if (pawn.IsSleepwalker())
            {
                float sleepwalkChance = pawn.GetPawnSleepwalkingChance();

                if (sleepwalkChance != 0)
                {
                    float roll = Rand.RangeInclusive(0, 60);

                    if (roll <= sleepwalkChance)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static float GetPawnSleepwalkingChance(this Pawn pawn)
        {
            float sleepwalkChance = 0;
            if (pawn?.story.traits.HasTrait(DD_TraitDefOf.Sleepwalker) == true)
            {
                sleepwalkChance = DD_Settings.sleepwalkerTraitModif;
                switch (pawn.story.traits.DegreeOfTrait(DD_TraitDefOf.Sleepwalker))
                {
                    case 1:
                        sleepwalkChance *= DD_Settings.occasionalSleepwalkerTraitModif;
                        break;

                    case 2:
                        sleepwalkChance *= DD_Settings.normalSleepwalkerTraitModif;
                        break;

                    case 3:
                        sleepwalkChance *= DD_Settings.usualSleepwalkerTraitModif;
                        break;
                }
            }
            return sleepwalkChance;
        }
    }
}