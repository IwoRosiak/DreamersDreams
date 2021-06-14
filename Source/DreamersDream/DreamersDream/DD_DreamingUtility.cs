using Verse;

namespace DreamersDream
{
    public static class DD_DreamingUtility
    {
        static DD_DreamingUtility()
        {
        }

        public static bool IsAwake(Pawn pawn)
        {
            if (pawn.needs.rest.GUIChangeArrow == 1)
            {
                return true;
            }
            else if (pawn.needs.rest.GUIChangeArrow == -1)
            {
                return false;
            }
            return true;
        }

        public static float CheckDreamChance(DD_ThoughtDef dream)
        {
            /*if (CheckForHigh() && dream.defName == "VeryGoodDream")
            {
                return dream.chance + 1000;

            }*/
            return dream.chance;
        }

        public static bool CheckForHigh(Pawn pawn)
        {
            for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[i];

                if (hediff.def.defName == "DreamHigh")
                {
                    //Messages.Message("Dream High Detected!", MessageTypeDefOf.NeutralEvent);
                    return true;
                }
            }
            return false;
        }
    }
}
