using RimWorld;
using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    [DefOf]
    public class DreamTagDef : Def
    {
        public float chance = 0;

        public bool isSpecial = false;

        public bool isSleepwalk = false;

        public bool isSideTag = false;

        public List<ChanceBoosters> ChanceBoosters = new List<ChanceBoosters>();
    }

    public enum ChanceBoosters
    {
        sleepwalker
    }
}