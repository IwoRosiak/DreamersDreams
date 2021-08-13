using RimWorld;
using System.Collections.Generic;

namespace DreamersDream
{
    [DefOf]
    public class DreamDef : ThoughtDef
    {
        public bool isSleepwalk = false;

        public float chance = 1;

        public List<DreamTagDef> tags = new List<DreamTagDef>();

        //public DreamTagDef tag;

        //public bool isSpecial = false;
    }
}