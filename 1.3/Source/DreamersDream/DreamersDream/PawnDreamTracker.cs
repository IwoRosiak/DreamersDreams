using RimWorld.Planet;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class PawnDreamTracker
    {
        public static void Tick(Pawn curPawn)
        {
            pawn = curPawn;
            TryApplyDream();
        }

        private static void TryApplyDream()
        {
            if (CanGetDreamNow())
            {
                var chosenDream = ChooseDream();
                TriggerDreamEffects(chosenDream);
            }
        }

        private static bool CanGetDreamNow()
        {
            //Log.Message(IsPawnCapableOfDreaming().ToString());
            if (IsPawnCapableOfDreaming() && !IsAwake() && IsPawnRestedEnough() && !HasDreamAlready())
            {
                return true;
            }
            return false;
        }

        private static bool IsPawnRestedEnough()
        {
            return pawn.needs.rest.CurCategory == RimWorld.RestCategory.Rested;
        }

        private static bool IsAwake()
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

        public static bool HasDreamAlready()
        {
            foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
            {
                if (pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(dream) != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static void TriggerDreamEffects(DD_ThoughtDef dream)
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(dream, null);
            if (dream.triggers != null)
            {
                pawn.mindState.mentalStateHandler.TryStartMentalState(dream.triggers[Rand.RangeInclusive(0, dream.triggers.Count - 1)], null, true, false, null, false);
            }

            if (dream.inspiration != null)
            {
                pawn.mindState.inspirationHandler.TryStartInspiration(dream.inspiration);
            }
        }

        private static bool IsPawnCapableOfDreaming()
        {
            return pawn.needs?.rest != null && pawn.def.defName == "Human" && !pawn.Dead && (pawn.Spawned || CaravanUtility.IsCaravanMember(pawn));
        }

        private static DD_ThoughtDef ChooseDream()
        {
            float totalDreamChance = 0;

            foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
            {
                totalDreamChance += DD_Utility.CheckDreamChance(dream, pawn);
            }
            var dreamChanceRoll = Rand.Range(0, totalDreamChance);

            var dreamChanceProgress = 0.0f;

            foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
            {
                var chanceForDream = DD_Utility.CheckDreamChance(dream, pawn);

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

        private static Pawn pawn;
    }
}