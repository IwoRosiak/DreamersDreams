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

        public static int chanceForNoDream = 100;
        public static int chanceForPositiveDreams = 100;
        public static int chanceForNegativeDreams = 100;
        //public float chanceForUserCreatedDreams = 1.0f;
        public static int chanceForSleepwalkingDreams = 100;

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

            Scribe_Values.Look(ref chanceForNoDream, "chanceForNoDream", 100);
            Scribe_Values.Look(ref chanceForPositiveDreams, "chanceForPositiveDreams", 100);
            Scribe_Values.Look(ref chanceForNegativeDreams, "chanceForNegativeDreams", 100);
            //Scribe_Values.Look(ref chanceForUserCreatedDreams, "chanceForUserCreatedDreams", 1.0f);
            Scribe_Values.Look(ref chanceForSleepwalkingDreams, "chanceForSleepwalkingDreams", 100);
            //Scribe_Collections.Look(ref exampleListOfPawns, "exampleListOfPawns", LookMode.Reference);
            base.ExposeData();
        }
    }

    public class DD_Mod : Mod
    {
        /// <summary>
        /// A reference to our settings.
        /// </summary>
        DD_Settings settings;

        /// <summary>
        /// A mandatory constructor which resolves the reference to our settings.
        /// </summary>
        /// <param name="content"></param>
        public DD_Mod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<DD_Settings>();
        }

        /// <summary>
        /// The (optional) GUI part to set your settings.
        /// </summary>
        /// <param name="inRect">A Unity Rect with the size of the settings window.</param>
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            //listingStandard.CheckboxLabeled("exampleBoolExplanation", ref settings.exampleBool, "exampleBoolToolTip");

            listingStandard.Label("How does it work? Increasing the slider will cause particular dreams to have higher chance than other dreams. 100% chance for sleepwalking doesn't mean that the actual chance for sleepwalking is 100%. In that case it would be around 1% (coded chance for sleepwalking dreams) each rest multiplied by 1 (100% divided by 100). Setting all the dreams to 1% is the same as setting all of them to 200%.");
            listingStandard.Label("");

            listingStandard.Label("Chance for no dreams: " + DD_Settings.chanceForNoDream.ToString() + "%");
            DD_Settings.chanceForNoDream = (int)listingStandard.Slider(DD_Settings.chanceForNoDream, 1, 200);


            listingStandard.Label("Chance for good dreams: " + DD_Settings.chanceForPositiveDreams.ToString() + "%");
            DD_Settings.chanceForPositiveDreams = (int)listingStandard.Slider(DD_Settings.chanceForPositiveDreams, 1, 200);


            listingStandard.Label("Chance for bad dreams: " + DD_Settings.chanceForNegativeDreams.ToString() + "%");
            DD_Settings.chanceForNegativeDreams = (int)listingStandard.Slider(DD_Settings.chanceForNegativeDreams, 1, 200);
            //settings.chanceForUserCreatedDreams = listingStandard.Slider(settings.chanceForUserCreatedDreams, 0f, 100f);


            listingStandard.Label("Chance for sleepwalking: " + DD_Settings.chanceForSleepwalkingDreams.ToString() + "%");
            DD_Settings.chanceForSleepwalkingDreams = (int)listingStandard.Slider(DD_Settings.chanceForSleepwalkingDreams, 1, 200);

            listingStandard.CheckboxLabeled("Do you want your colonists to dream? (ON by default)", ref DD_Settings.isDreamingActive, "If OFF it will stop colonists from getting new dreams. It won't erase the ones colonists already have. It doesn't stop colonists from getting dreams which trigger sleepwalking.");
            listingStandard.CheckboxLabeled("Do you want your colonists to sleepwalk? (ON by default)", ref DD_Settings.isSleepwalkingActive, "If OFF it will stop colonists from sleepwalking, It won't stop currently sleepwalking colonists from wrecking your colony.");

            listingStandard.Label("");
            listingStandard.Label("Those settings are for turning off particular sleepwalking states. These do not affect dreams.");
            listingStandard.CheckboxLabeled("Sleepwalking - normal: ", ref DD_Settings.isSleepNormalActive, "Turns off normal sleepwalking state.");
            listingStandard.CheckboxLabeled("Sleepwalking - berserk: ", ref DD_Settings.isSleepBerserkActive, "Turns off sleepwalking berserk state.");
            listingStandard.CheckboxLabeled("Sleepwalking - tantrum: ", ref DD_Settings.isSleepTantrumActive, "Turns off sleepwalking tantrum state.");
            listingStandard.CheckboxLabeled("Sleepwalking - foodbinge: ", ref DD_Settings.isSleepFoodBingeActive, "Turns off sleepwalking food binge state.");
            //listingStandard.CheckboxLabeled("Sleepwalking - berserk: ", ref DD_Settings.isSleepwalkingActive, "Turns off sleepwalking berserk state.");

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
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