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

        public static bool canNonSleepwalkerSleepwalk = true;

        public static bool isDebugMode = false;

        public static Dictionary<string, float> TagsCustomChances = new Dictionary<string, float>();

        public static Dictionary<string, bool> TagsCustomNotify = new Dictionary<string, bool>();

        //sleepwalk
        public static float sleepwalkerTraitModif = 1;

        public static float occasionalSleepwalkerTraitModif = 3f;
        public static float normalSleepwalkerTraitModif = 12.0f;
        public static float usualSleepwalkerTraitModif = 30.0f;

        public static float calmSleepwalkBaseChance = 1.0f;

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref TagsCustomChances, "TagsCustomChances", LookMode.Value, LookMode.Value);
            Scribe_Collections.Look(ref TagsCustomNotify, "TagsCustomNotify", LookMode.Value, LookMode.Value);

            Scribe_Values.Look(ref isDreamingActive, "isDreamingActive", true);

            Scribe_Values.Look(ref isDefaultSettings, "isDefaultSettings", true);

            //sleepwalking

            Scribe_Values.Look(ref sleepwalkerTraitModif, "sleepwalkerTraitModif", 1);

            //Scribe_Values.Look(ref occasionalSleepwalkerTraitModif, "occasionalSleepwalkerTraitModif", 1.5f);
            //Scribe_Values.Look(ref normalSleepwalkerTraitModif, "normalSleepwalkerTraitModif", 8.0f);
            //Scribe_Values.Look(ref usualSleepwalkerTraitModif, "usualSleepwalkerTraitModif", 30.0f);

            base.ExposeData();
        }

        public static void PurgeDicts()
        {
            //chances
            if (TagsCustomChances.EnumerableNullOrEmpty())
            {
                TagsCustomChances = new Dictionary<string, float>();
            }

            Dictionary<string, float> tempDictChances = new Dictionary<string, float>();

            foreach (var tag in DreamTracker.GetAllDreamTags)
            {
                if (TagsCustomChances.ContainsKey(tag.defName) && TagsCustomChances[tag.defName] != tag.chance)
                {
                    tempDictChances.Add(tag.defName, TagsCustomChances[tag.defName]);
                }
            }

            TagsCustomChances.Clear();
            TagsCustomChances = tempDictChances;

            //notifications
            if (TagsCustomNotify.EnumerableNullOrEmpty())
            {
                TagsCustomNotify = new Dictionary<string, bool>();
            }

            Dictionary<string, bool> tempDictNotify = new Dictionary<string, bool>();

            foreach (var tag in DreamTracker.GetAllDreamTags)
            {
                if (TagsCustomNotify.ContainsKey(tag.defName))
                {
                    tempDictNotify.Add(tag.defName, TagsCustomNotify[tag.defName]);
                }
            }

            TagsCustomNotify.Clear();
            TagsCustomNotify = tempDictNotify;
        }
    }
}