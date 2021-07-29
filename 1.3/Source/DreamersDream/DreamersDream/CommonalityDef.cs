using RimWorld;
using Verse;

namespace DreamersDream
{
    [DefOf]
    public class DreamQualityDef : Def
    {
        /*public enum CommonalityCategory
        {
            legendary,
            epic,
            amazing,
            exciting,
            pleasent,
            boring,
            disturbing,
            frightening,
            terrorising,
            helllike
        }*/

        public float chance = 1;

        public bool isSpecial = false;
    }
}