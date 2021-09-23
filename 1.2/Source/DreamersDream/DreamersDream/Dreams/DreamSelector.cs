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
                    if (DD_Settings.isDebugMode)
                    {
                        Log.Message("Rolled dream: " + item.Key.defName + " with upper chance threshold of " + item.Value + "% with roll of " + roll + ".");
                    }

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
                    if (DD_Settings.isDebugMode)
                    {
                        Log.Message(" ");
                        Log.Message("Rolled tag: " + item.Key.label + " with standard chance of " + item.Value + "% and def chance of " + item.Key.CalculateChanceFor() + " with roll of " + roll + ".");
                    }

                    return item.Key;
                }
            }
            return null;
        }

        private static float PerformRandomRoll()
        {
            return Rand.Range(0f, 100f);
        }
    }
}