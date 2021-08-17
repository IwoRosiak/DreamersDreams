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

        public List<ChanceBooster> ChanceBoosters = new List<ChanceBooster>();

        public List<Requirement> requirements = new List<Requirement>();
    }

    public enum ChanceBooster
    {
        sleepwalker
    }

    public enum Requirement
    {
        sleepwalker
    }
}