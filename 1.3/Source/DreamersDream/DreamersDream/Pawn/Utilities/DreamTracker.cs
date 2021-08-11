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

        public static List<DreamQualityDef> DreamQualityDefs = new List<DreamQualityDef>();

        public static Dictionary<DreamQualityDef, float> DreamQualityDefsWithSettings = new Dictionary<DreamQualityDef, float>();

        internal static List<DreamDef> DreamDefs = new List<DreamDef>();
    }
}