using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class PawnDreamTracker
    {
        public static List<DreamQualityDef> GetDreamQualities
        {
            get
            {
                return DreamQualityDefs;
            }
        }

        public static List<DreamDef> GetAvailibleDreamsForPawn
        {
            get
            {
                return DreamDefs;
            }
        }

        public static bool HasDreamAlready(Pawn pawn)
        {
            foreach (DreamDef dream in DreamDefs)
            {
                if (pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(dream) != null)
                {
                    return true;
                }
            }
            return false;
        }

        private static List<DreamQualityDef> DreamQualityDefs = new List<DreamQualityDef>();

        internal static List<DreamDef> DreamDefs = new List<DreamDef>();
    }
}