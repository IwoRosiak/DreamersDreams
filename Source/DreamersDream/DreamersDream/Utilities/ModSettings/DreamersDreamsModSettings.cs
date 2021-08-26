using System;
using UnityEngine;
using Verse;

namespace DreamersDream
{
    public class DD_Mod : Mod
    {
        private DD_Settings settings;

        public DD_Mod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<DD_Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Widgets.DrawTextureFitted(new Rect(inRect.x, inRect.y, inRect.width, inRect.height), Textures.SettingsBackGround, 1);

            Rect masterRect = new Rect(inRect.x + (0.1f * inRect.width), inRect.y + 40, 0.8f * inRect.width, 936);

            Listing_Standard listingTop = new Listing_Standard();
            Rect TopSettings = new Rect(masterRect.x, masterRect.y, masterRect.width, 45);

            listingTop.Begin(TopSettings); //column 1
            listingTop.ColumnWidth = TopSettings.width / 3.2f;

            if (listingTop.ButtonText("Mod active: " + DD_Settings.isDreamingActive.ToString()))
            {
                DD_Settings.isDreamingActive = !DD_Settings.isDreamingActive;
            }

            Rect MidSettings = new Rect(TopSettings.x, TopSettings.y + listingTop.CurHeight, masterRect.width, 180f);

            listingTop.NewColumn(); // column 2
            if (DD_Settings.isDreamingActive)
            {
                if (listingTop.ButtonText("Default settings: " + DD_Settings.isDefaultSettings.ToString()))
                {
                    DD_Settings.isDefaultSettings = !DD_Settings.isDefaultSettings;
                }
            }

            listingTop.NewColumn(); //column 3
            if (listingTop.ButtonText("Reset settings", "Set now"))
            {
                ResetValues();
            }

            listingTop.End();

            Listing_Standard listingMid = new Listing_Standard();
            if (DD_Settings.isDreamingActive && !DD_Settings.isDefaultSettings)
            {
                listingMid.Begin(MidSettings);

                listingMid.GapLine();

                if (DD_Settings.sleepwalkerTraitModif == 0)
                {
                    listingMid.Label("Sleepwalking is turned off.");
                }
                else
                {
                    listingMid.Label("Occasional sleepwalker will sleepwalk around " + DD_Settings.occasionalSleepwalkerTraitModif * DD_Settings.sleepwalkerTraitModif + " times a year.");
                    listingMid.Label("Sleepwalker will sleepwalk around " + DD_Settings.normalSleepwalkerTraitModif * DD_Settings.sleepwalkerTraitModif + " times a year.");
                    listingMid.Label("Usual sleepwalker will sleepwalk around " + DD_Settings.usualSleepwalkerTraitModif * DD_Settings.sleepwalkerTraitModif + " times a year.");
                }

                DD_Settings.sleepwalkerTraitModif = (float)Math.Round(listingMid.Slider(DD_Settings.sleepwalkerTraitModif, 0, 2), 2);

                listingMid.GapLine();

                Rect TableSettings = new Rect(masterRect.x, MidSettings.y + listingMid.CurHeight, 320f, inRect.height);
                TableSettings.height = inRect.height - TableSettings.y;

                listingMid.End();

                Rect TagTable = new Rect(TableSettings.x, TableSettings.y, TableSettings.width - 10f, columnHeight * CountDisplayableTags());
                Widgets.BeginScrollView(TableSettings, ref scrollPos, TagTable);

                DrawTagsRows(TagTable);

                Widgets.EndScrollView();
            }
        }

        private void DrawTagsRows(Rect TableRect)
        {
            float count = 0;

            float ScrollYPos = TableRect.y;

            Rect columnTags = new Rect(TableRect.x, ScrollYPos, 100f, columnHeight);
            ModSettingsUtility.DrawTableFirstRow(ref columnTags, "Tags");

            Rect columnChance = new Rect(columnTags.x + columnTags.width, ScrollYPos, 146f, columnHeight);
            ModSettingsUtility.DrawTableFirstRow(ref columnChance, "Chance");

            Rect columnNotify = new Rect(columnChance.x + columnChance.width, ScrollYPos, 50f, columnHeight);
            ModSettingsUtility.DrawTableFirstRow(ref columnNotify, "Notify");

            foreach (var dreamTag in DreamTracker.GetAllDreamTags)
            {
                if (dreamTag.isSideTag)
                {
                    continue;
                }

                ResolveAlternatingBG(count, columnTags, Textures.TableEntryBGCat1, Textures.TableEntryBGCat2);
                DrawColumnCategory(columnTags, dreamTag);

                ResolveAlternatingBG(count, columnChance, Textures.TableEntryBGChance1, Textures.TableEntryBGChance2);
                DrawColumnChance(columnChance, dreamTag);

                ResolveAlternatingBG(count, columnNotify, Textures.TableEntryBGNotify1, Textures.TableEntryBGNotify1);
                DrawColumnNotify(columnNotify, dreamTag);

                count++;

                columnTags.y += columnHeight;
                columnChance.y += columnHeight;
                columnNotify.y += columnHeight;
            }
        }

        private void DrawColumnCategory(Rect column, DreamTagDef tag)
        {
            Widgets.Label(ModSettingsUtility.GetMiddleOfRectForString(column, tag.label), tag.defName);
        }

        private void DrawColumnChance(Rect column, DreamTagDef tag)
        {
            float chance = ModSettingsUtility.ResolveCustomChanceForTag(tag);

            ModSettingsUtility.DrawChanceButtons(column, ref chance);

            string label = Math.Round(PawnDreamTagsOddsTracker.ChanceInPercentages(chance, ModSettingsUtility.AddUpChancesForQualities()), 2) + "%";
            Widgets.Label(ModSettingsUtility.GetMiddleOfRectForString(column, label), label);

            ModSettingsUtility.UpdateCustomChanceForTag(tag, chance);
        }

        private void DrawColumnNotify(Rect column, DreamTagDef tag)
        {
            bool notify = tag.isSpecial;
            ModSettingsUtility.CheckIfHasCustomNotifyAndAddIfNot(tag, ref notify);

            if (Widgets.ButtonText(column, notify.ToString().CapitalizeFirst()))
            {
                notify = !notify;
            }

            DD_Settings.TagsCustomNotify[tag.defName] = notify;
        }

        private void ResetValues()
        {
            DD_Settings.TagsCustomChances.Clear();

            DD_Settings.TagsCustomNotify.Clear();

            DD_Settings.sleepwalkerTraitModif = 1;
        }

        private void ResolveAlternatingBG(float alternatingBGCount, Rect column, Texture texture1, Texture texture2)
        {
            switch (alternatingBGCount % 2)
            {
                case 0:
                    Widgets.DrawTextureFitted(column, texture1, 1);
                    break;

                case 1:
                    Widgets.DrawTextureFitted(column, texture2, 1);
                    break;
            }
        }

        private float CountDisplayableTags()
        {
            float numberOfRows = 1;

            foreach (var tag in DreamTracker.GetAllDreamTags)
            {
                if (!tag.isSideTag)
                {
                    numberOfRows++;
                }
            }

            return numberOfRows;
        }

        public override string SettingsCategory()
        {
            return "Dreamer's Dreams";
        }

        private static Vector2 scrollPos = Vector2.zero;

        private static float columnHeight = 25f;
    }
}