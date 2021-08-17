using Verse;

namespace DreamersDream
{
    internal static class DreamsPawnHelper
    {
        public static bool isSleepwalker(this Pawn pawn)
        {
            var comp = pawn.TryGetComp<DreamsComp>();

            return comp?.pawn.story.traits.HasTrait(DD_TraitDefOf.Sleepwalker) == true;
        }
    }
}