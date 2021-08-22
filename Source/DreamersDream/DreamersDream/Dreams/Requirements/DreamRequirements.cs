using Verse;

namespace DreamersDream
{
    internal static class DreamRequirements
    {
        public static bool IsMeetingRequirements(this DreamDef dream, Pawn pawn)
        {
            foreach (var tag in dream.tags)
            {
                if (!IsMeetingRequirements(tag, pawn))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsMeetingRequirements(this DreamTagDef tag, Pawn pawn)
        {
            foreach (var requirement in tag.requirements)
            {
                if (!requirement.CheckRequirementForPawn(pawn))
                {
                    return false;
                }
            }

            return true;
        }
    }
}