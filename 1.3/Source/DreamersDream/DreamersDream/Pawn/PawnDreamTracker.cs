using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class PawnDreamTracker
    {
        public static Dictionary<DreamQualityDef, float> GetDreamQualityDict
        {
            get
            {
                return DreamQualityChancesDict;
            }
        }

        public static IEnumerable<DreamDef> AllAvailibleDreamsForPawn()
        {
            foreach (var dream in listOfAllDreamDefs)
            {
                yield return dream;
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

        private static Dictionary<DreamQualityDef, float> DreamQualityChancesDict = new Dictionary<DreamQualityDef, float>();

        internal static List<DreamDef> listOfAllDreamDefs = new List<DreamDef>();
    }
}