using UnityEngine;
using Verse;

namespace DreamersDream
{
    public class DD_Settings : ModSettings
    {
        /// <summary>
        /// The three settings our mod has.
        /// </summary>
        /// 
        public static bool isDreamingActive = true;
        public static bool isSleepwalkingActive = true;
        public static bool isSleepBerserkActive = true;
        public static bool isSleepNormalActive = true;
        public static bool isSleepFoodBingeActive = true;
        public static bool isSleepTantrumActive = true;

        public static int chanceForNoDream = 0;
        public static int chanceForPositiveDreams = 0;
        public static int chanceForNegativeDreams = 0;
        //public float chanceForUserCreatedDreams = 1.0f;
        public static int chanceForSleepwalkingDreams = 0;

        public static int chanceMultiplierForIlness = 50;
        public static int chanceMultiplierForTemperature = 25;
        public static int chanceMultiplierForHunger = 50;

        /// <summary>
        /// The part that writes our settings to file. Note that saving is by ref.
        /// </summary>
        public override void ExposeData()
        {
            Scribe_Values.Look(ref isDreamingActive, "isDreamingActive", true);
            Scribe_Values.Look(ref isSleepwalkingActive, "isSleepwalkingActive", true);
            Scribe_Values.Look(ref isSleepBerserkActive, "isSleepBerserkActive", true);
            Scribe_Values.Look(ref isSleepNormalActive, "isSleepNormalActive", true);
            Scribe_Values.Look(ref isSleepFoodBingeActive, "isSleepFoodBingeActive", true);
            Scribe_Values.Look(ref isSleepTantrumActive, "isSleepTantrumActive", true);

            Scribe_Values.Look(ref chanceForNoDream, "chanceForNoDream", 0);
            Scribe_Values.Look(ref chanceForPositiveDreams, "chanceForPositiveDreams", 0);
            Scribe_Values.Look(ref chanceForNegativeDreams, "chanceForNegativeDreams", 0);
            //Scribe_Values.Look(ref chanceForUserCreatedDreams, "chanceForUserCreatedDreams", 1.0f);
            Scribe_Values.Look(ref chanceForSleepwalkingDreams, "chanceForSleepwalkingDreams", 0);
            //Scribe_Collections.Look(ref exampleListOfPawns, "exampleListOfPawns", LookMode.Reference);

            Scribe_Values.Look(ref chanceMultiplierForIlness, "chanceMultiplierForIlness", 50);
            Scribe_Values.Look(ref chanceMultiplierForTemperature, "chanceMultiplierForTemperature", 25);
            Scribe_Values.Look(ref chanceMultiplierForHunger, "chanceMultiplierForHunger", 50);
            base.ExposeData();
        }
    }

    public class DD_Mod : Mod
    {
        DD_Settings settings;

        private static Vector2 ScrollPos = Vector2.zero;


        public DD_Mod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<DD_Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {


            Listing_Standard listingStandard = new Listing_Standard();
            Rect rect = new Rect(0f, 0f, inRect.width, 600f);
            rect.xMax *= 0.9f;
            listingStandard.BeginScrollView(inRect, ref DD_Mod.ScrollPos, ref rect);
            //listingStandard.CheckboxLabeled("exampleBoolExplanation", ref settings.exampleBool, "exampleBoolToolTip");
            listingStandard.Label("Remember, decreasing value for one category increases the odds to get other type of dreams.");
            listingStandard.Label("");

            listingStandard.Label("Chance for no dreams: " + DD_Settings.chanceForNoDream.ToString() + "%");
            DD_Settings.chanceForNoDream = (int)listingStandard.Slider(DD_Settings.chanceForNoDream, -100, 100);


            listingStandard.Label("Chance for good dreams: " + DD_Settings.chanceForPositiveDreams.ToString() + "%");
            DD_Settings.chanceForPositiveDreams = (int)listingStandard.Slider(DD_Settings.chanceForPositiveDreams, -100, 100);


            listingStandard.Label("Chance for bad dreams: " + DD_Settings.chanceForNegativeDreams.ToString() + "%");
            DD_Settings.chanceForNegativeDreams = (int)listingStandard.Slider(DD_Settings.chanceForNegativeDreams, -100, 100);
            //settings.chanceForUserCreatedDreams = listingStandard.Slider(settings.chanceForUserCreatedDreams, 0f, 100f);


            listingStandard.Label("Chance for sleepwalking: " + DD_Settings.chanceForSleepwalkingDreams.ToString() + "%");
            DD_Settings.chanceForSleepwalkingDreams = (int)listingStandard.Slider(DD_Settings.chanceForSleepwalkingDreams, -100, 100);

            listingStandard.CheckboxLabeled("Do you want your colonists to dream? (ON by default)", ref DD_Settings.isDreamingActive, "If OFF it will stop colonists from getting new dreams. It won't erase the ones colonists already have. It doesn't stop colonists from getting dreams which trigger sleepwalking.");
            listingStandard.CheckboxLabeled("Do you want your colonists to sleepwalk? (ON by default)", ref DD_Settings.isSleepwalkingActive, "If OFF it will stop colonists from sleepwalking, It won't stop currently sleepwalking colonists from wrecking your colony.");

            listingStandard.Label("");
            listingStandard.Label("Those settings are for turning off particular sleepwalking states. These do not affect dreams.");
            listingStandard.CheckboxLabeled("Sleepwalking - normal: ", ref DD_Settings.isSleepNormalActive, "Turns off normal sleepwalking state.");
            listingStandard.CheckboxLabeled("Sleepwalking - berserk: ", ref DD_Settings.isSleepBerserkActive, "Turns off sleepwalking berserk state.");
            listingStandard.CheckboxLabeled("Sleepwalking - tantrum: ", ref DD_Settings.isSleepTantrumActive, "Turns off sleepwalking tantrum state.");
            listingStandard.CheckboxLabeled("Sleepwalking - foodbinge: ", ref DD_Settings.isSleepFoodBingeActive, "Turns off sleepwalking food binge state.");
            //listingStandard.CheckboxLabeled("Sleepwalking - berserk: ", ref DD_Settings.isSleepwalkingActive, "Turns off sleepwalking berserk state.");

            listingStandard.Label("");
            listingStandard.Label("If a dream has a sensitivity to particular factor (all listed below) the setting for that factor will increase the chance. E.g. 100% will double that chance. This chance is added (not multiplied!) to overall chance. If a dream has sensitivity to more than one thing then those settings will stack.");
            listingStandard.Label("How much ilness increases chance for ilness sensitive dreams: " + DD_Settings.chanceMultiplierForIlness.ToString() + "%");
            DD_Settings.chanceMultiplierForIlness = (int)listingStandard.Slider(DD_Settings.chanceMultiplierForIlness, 0, 250);

            listingStandard.Label("How much temperature increases chance for temperature sensitive dreams: " + DD_Settings.chanceMultiplierForTemperature.ToString() + "%");
            DD_Settings.chanceMultiplierForTemperature = (int)listingStandard.Slider(DD_Settings.chanceMultiplierForTemperature, 0, 250);

            listingStandard.Label("How much temperature increases chance for temperature sensitive dreams: " + DD_Settings.chanceMultiplierForHunger.ToString() + "%");
            DD_Settings.chanceMultiplierForHunger = (int)listingStandard.Slider(DD_Settings.chanceMultiplierForHunger, 0, 250);

            listingStandard.EndScrollView(ref rect);
        }

        /// <summary>
        /// Override SettingsCategory to show up in the list of settings.
        /// Using .Translate() is optional, but does allow for localisation.
        /// </summary>
        /// <returns>The (translated) mod name.</returns>
        public override string SettingsCategory()
        {
            return "Dreamer's Dreams";
        }
    }
}