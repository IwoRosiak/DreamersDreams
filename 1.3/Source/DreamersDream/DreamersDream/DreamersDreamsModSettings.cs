using System;
using UnityEngine;
using Verse;

namespace DreamersDream
{
    public class DD_Settings : ModSettings
    {
        public static bool isDreamingActive = true;

        public static int sleepwalkerTraitModif = 1;

        public static bool isDebugMode = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref isDreamingActive, "isDreamingActive", true);

            Scribe_Values.Look(ref sleepwalkerTraitModif, "sleepwalkerTraitModif", 1);
            //Scribe_Values.Look(ref chanceForPositiveDreams, "chanceForPositiveDreams", 0);
            //Scribe_Values.Look(ref chanceForNegativeDreams, "chanceForNegativeDreams", 0);

            base.ExposeData();
        }
    }

    public class DD_Mod : Mod
    {
        private DD_Settings settings;

        private static Vector2 ScrollPos = Vector2.zero;

        public DD_Mod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<DD_Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Tree listingTree = new Listing_Tree();

            Listing_Standard listingStandard = new Listing_Standard();
            Rect rect = new Rect(0f, 0f, inRect.width / 2, 850f);
            rect.xMax *= 0.9f;
            Vector2 vector = new Vector2(rect.x, rect.y + 25f);
            listingStandard.Begin(inRect);
            float yOffset = 0;

            Rect columnQuality = new Rect(rect.x + 20f, vector.y, 100f, 25f);

            Widgets.Label(columnQuality, "Category ");
            Widgets.DrawBox(columnQuality);

            Rect columnChance = new Rect(rect.x + 120f, vector.y, 100f, 25f);

            Widgets.Label(columnChance, "Chance ");
            Widgets.DrawBox(columnChance);

            columnQuality.y += 25f;
            columnChance.y += 25f;
            foreach (DreamQualityDef dreamQuality in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(DreamQualityDef)))
            {
                Widgets.Label(columnQuality, dreamQuality.defName);
                Widgets.DrawBox(columnQuality);
                columnQuality.y += 25f;

                Widgets.Label(columnChance, Math.Round(PawnDreamQualityOddsTracker.ChanceInPercentages(dreamQuality.chance, PawnDreamQualityOddsTracker.AddUpChancesForQualities()), 2) + "%");
                Widgets.DrawBox(columnChance);
                Widgets.ButtonInvisible(columnChance);
                columnChance.y += 25f;

                //yOffset += 25f;
            }

            //Widgets.Label(qualityRect, "Category " + dreamQuality.defName + " chance: " + Math.Round(PawnDreamRandomCalc.ChanceInPercentages(dreamQuality.chance, PawnDreamRandomCalc.AddUpChancesForQualities()), 2) + "%");

            //Widgets.Checkbox(new Vector2(inRect.x, listingTree.CurHeight + 25f), ref DD_Settings.isDebugMode);
            listingStandard.End();

            /*listingStandard. Label("Chance modifier for no dreams: " + DD_Settings.chanceForNoDream.ToString() + "%");
            DD_Settings.chanceForNoDream = (int)listingStandard.Slider(DD_Settings.chanceForNoDream, -100, 100);

            listingStandard.Label("Chance modifier for good dreams: " + DD_Settings.chanceForPositiveDreams.ToString() + "%");
            DD_Settings.chanceForPositiveDreams = (int)listingStandard.Slider(DD_Settings.chanceForPositiveDreams, -100, 100);

            listingStandard.Label("Chance modifier for bad dreams: " + DD_Settings.chanceForNegativeDreams.ToString() + "%");
            DD_Settings.chanceForNegativeDreams = (int)listingStandard.Slider(DD_Settings.chanceForNegativeDreams, -100, 100);

            listingStandard.AddLabeledCheckbox("Debug mode: ", ref DD_Settings.isDebugMode);

            listingStandard.CheckboxLabeled("Debug mode: ", ref DD_Settings.isDebugMode, "Turns on Debug Mode. It will automatically turn off when you reload the game.");
            listingStandard.End();*/
            //listingStandard.EndScrollView(ref rect);
        }

        public override string SettingsCategory()
        {
            return "Dreamer's Dreams";
        }
    }
}