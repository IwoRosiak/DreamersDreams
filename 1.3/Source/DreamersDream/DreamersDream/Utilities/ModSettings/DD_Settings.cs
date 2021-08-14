using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    public class DD_Settings : ModSettings
    {
        public DD_Settings()
        {
        }

        public static bool isDreamingActive = true;

        public static bool isDefaultSettings = true;

        public static float sleepwalkerTraitModif = 1;

        public static bool canNonSleepwalkerSleepwalk = true;

        public static bool isDebugMode = false;

        public static Dictionary<string, float> TagsCustomChances;

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref TagsCustomChances, "TagsCustomChances", LookMode.Value, LookMode.Value);

            Scribe_Values.Look(ref isDreamingActive, "isDreamingActive", true);

            Scribe_Values.Look(ref isDefaultSettings, "isDefaultSettings", true);

            Scribe_Values.Look(ref sleepwalkerTraitModif, "sleepwalkerTraitModif", 1);

            base.ExposeData();
        }
    }
}