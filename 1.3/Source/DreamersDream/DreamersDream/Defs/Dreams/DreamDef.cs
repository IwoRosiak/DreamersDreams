using RimWorld;

namespace DreamersDream
{
    [DefOf]
    public class DreamDef : ThoughtDef
    {
        public bool isSleepwalk = false;

        public float chance = 1;

        public DreamQualityDef quality;

        //public bool isSpecial = false;
    }
}