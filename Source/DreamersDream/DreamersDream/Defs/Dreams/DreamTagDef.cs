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

        public List<ChanceBoosters> chanceBoosters = new List<ChanceBoosters>();

        public List<Requirements> requirements = new List<Requirements>();
    }

    public enum ChanceBoosters
    {
        sleepwalker
    }

    public enum Requirements
    {
        sleepwalker
    }
}