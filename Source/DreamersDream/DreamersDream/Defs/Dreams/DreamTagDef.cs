using RimWorld;
using Verse;

namespace DreamersDream
{
    [DefOf]
    public class DreamTagDef : Def
    {
        public float chance = 0;

        public bool isSpecial = false;

        public bool isSleepwalk = false;

        public float moodOffset = 0;

        //public List<Requirements> requirements = new List<Requirements>();
    }
}