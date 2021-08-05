using System.Collections.Generic;

namespace DreamersDream
{
    public static class DreamTracker
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

        private static List<DreamQualityDef> DreamQualityDefs = new List<DreamQualityDef>();

        internal static List<DreamDef> DreamDefs = new List<DreamDef>();
    }
}