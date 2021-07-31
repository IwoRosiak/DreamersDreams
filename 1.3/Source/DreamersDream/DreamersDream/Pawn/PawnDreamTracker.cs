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
                return DreamQualityChances;
            }
        }

        public static List<DreamDef> GetAvailibleDreamsForPawn
        {
            get
            {
                return listOfAllDreamDefs;
            }
        }

        public static bool HasDreamAlready(Pawn pawn)
        {
            foreach (DreamDef dream in listOfAllDreamDefs)
            {
                if (pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(dream) != null)
                {
                    return true;
                }
            }
            return false;
        }

        private static List<DreamQualityDef> DreamQualityChances = new List<DreamQualityDef>();

        internal static List<DreamDef> listOfAllDreamDefs = new List<DreamDef>();
    }
}