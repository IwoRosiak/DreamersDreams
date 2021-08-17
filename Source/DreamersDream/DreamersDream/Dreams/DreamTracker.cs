using System.Collections.Generic;

namespace DreamersDream
{
    public static class DreamTracker
    {
        public static List<DreamTagDef> GetAllDreamTags
        {
            get
            {
                return DreamTagsDefs;
            }
        }

        public static List<DreamDef> GetAllDreams
        {
            get
            {
                return DreamDefs;
            }
        }

        private static List<DreamTagDef> DreamTagsDefs = new List<DreamTagDef>();

        private static List<DreamDef> DreamDefs = new List<DreamDef>();
    }
}