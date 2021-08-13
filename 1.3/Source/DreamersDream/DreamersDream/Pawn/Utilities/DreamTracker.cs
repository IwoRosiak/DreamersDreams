using System.Collections.Generic;

namespace DreamersDream
{
    public static class DreamTracker
    {
        public static List<DreamTagDef> GetDreamTags
        {
            get
            {
                return DreamTagsDefs;
            }
        }

        public static List<DreamDef> GetAvailibleDreamsForPawn
        {
            get
            {
                return DreamDefs;
            }
        }

        public static List<DreamTagDef> DreamTagsDefs = new List<DreamTagDef>();

        public static Dictionary<DreamTagDef, float> DreamTagsDefsWithSettings = new Dictionary<DreamTagDef, float>();

        internal static List<DreamDef> DreamDefs = new List<DreamDef>();
    }
}