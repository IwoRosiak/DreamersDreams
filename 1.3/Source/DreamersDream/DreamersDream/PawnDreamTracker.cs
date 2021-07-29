using RimWorld.Planet;
using System.Collections.Generic;
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
                ChooseDream()?.TriggerDreamEffects();
            }
        }

        private static bool CanGetDreamNow()
        {
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
            foreach (DreamDef dream in PawnDreamTracker.listOfAllDreamDefs)
            {
                if (pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(dream) != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static void TriggerDreamEffects(this DreamDef dream)
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

        private static DreamDef ChooseDream()
        {
            float totalDreamChance = 0;

            var cycle = 0;

            foreach (DreamDef dream in listOfAllDreamDefs)
            {
                cycle++;
                totalDreamChance += dream.chance;         //DD_Utility.CheckDreamChance(dream, pawn);
            }
            var dreamChanceRoll = Rand.Range(0, totalDreamChance);
            Log.Message(dreamChanceRoll.ToString());
            var dreamChanceProgress = 0.0f;

            foreach (DreamDef dream in listOfAllDreamDefs)
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

        public static IEnumerable<DreamDef> AllAvailibleDreamsForPawn()
        {
            foreach (var dream in listOfAllDreamDefs)
            {
                yield return dream;
            }
        }

        private static Pawn pawn;

        private static List<DreamDef> listOfAllDreamDefs = new List<DreamDef>();
    }
}