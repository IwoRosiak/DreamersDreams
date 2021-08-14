using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    internal static class DreamSelector
    {
        internal static DreamTagDef ChooseRandomDreamTag(Dictionary<DreamTagDef, float> Tags)
        {
            return RollRandomDreamTags(Tags);
        }

        internal static DreamDef ChooseRandomDream(Dictionary<DreamDef, float> Dreams)
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

        private static DreamTagDef RollRandomDreamTags(Dictionary<DreamTagDef, float> Qualities)
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