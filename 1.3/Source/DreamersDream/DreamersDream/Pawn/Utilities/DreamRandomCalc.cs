using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    internal static class DreamRandomCalc
    {
        public static DreamQualityDef ChooseRandomDreamQuality(Dictionary<DreamQualityDef, float> Qualities)
        {
            return RollRandomDreamQuality(Qualities);
        }

        public static DreamDef ChooseRandomDream(Dictionary<DreamDef, float> Dreams)
        {
            return RollRandomDream(Dreams);
        }

        private static DreamDef RollRandomDream(Dictionary<DreamDef, float> Dreams)
        {
            float roll = PerformRandomRoll();

            foreach (var item in Dreams)
            {
                if (roll <= item.Value)
                {
                    return item.Key;
                }
            }
            return null;
        }

        private static DreamQualityDef RollRandomDreamQuality(Dictionary<DreamQualityDef, float> Qualities)
        {
            float roll = PerformRandomRoll();

            foreach (var item in Qualities)
            {
                if (roll <= item.Value)
                {
                    return item.Key;
                }
            }
            return null;
        }

        private static float PerformRandomRoll()
        {
            return Rand.Range(0, 100);
        }
    }
}