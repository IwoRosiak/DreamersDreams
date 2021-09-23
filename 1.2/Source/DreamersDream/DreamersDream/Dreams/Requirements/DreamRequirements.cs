using RimWorld;
using Verse;

namespace DreamersDream
{
    internal static class DreamRequirements
    {
        public static bool IsMeetingRequirements(this DreamDef dream, Pawn pawn)
        {
            if (!ThoughtUtility.CanGetThought_NewTemp(pawn, dream, true))
            {
                return false;
            }

            if (!dream.CheckRequirementForPawn(pawn))
            {
                return false;
            }

            return true;
        }

        public static bool IsMeetingRequirements(this DreamTagDef tag, Pawn pawn)
        {
            return true;
        }
    }
}