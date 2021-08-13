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

        public static bool canNonSleepwalkerSleepwalk = false;

        public static bool isDebugMode = false;

        public static Dictionary<string, float> TagsChanceModifs;

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref TagsChanceModifs, "TagsChanceModifs", LookMode.Value, LookMode.Value);

            Scribe_Values.Look(ref isDreamingActive, "isDreamingActive", true);

            Scribe_Values.Look(ref isDefaultSettings, "isDefaultSettings", true);

            Scribe_Values.Look(ref sleepwalkerTraitModif, "sleepwalkerTraitModif", 1);
            //Scribe_Values.Look(ref chanceForPositiveDreams, "chanceForPositiveDreams", 0);
            //Scribe_Values.Look(ref chanceForNegativeDreams, "chanceForNegativeDreams", 0);

            base.ExposeData();
        }
    }
}