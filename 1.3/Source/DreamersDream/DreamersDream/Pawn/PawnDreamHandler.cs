using RimWorld.Planet;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class PawnDreamHandler
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
                PawnDreamRandomCalc.ChooseRandomDream()?.TriggerDreamEffects();
            }
        }

        private static bool CanGetDreamNow()
        {
            if (IsPawnCapableOfDreaming() && !IsAwake() && IsPawnRestedEnough() && !PawnDreamTracker.HasDreamAlready(pawn))
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

        private static void TriggerDreamEffects(this DreamDef dream)
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
            return pawn.needs?.rest != null && pawn.def.defName == "Human" && !pawn.Dead && (pawn.Spawned || pawn.IsCaravanMember());
        }

        private static Pawn pawn;
    }
}