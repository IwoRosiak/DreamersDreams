using System;
using System.Collections.Generic;
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
            if (DD_Settings.TagsCustomChances.NullOrEmpty())
            {
                DD_Settings.TagsCustomChances = new Dictionary<string, float>();
            }

            Widgets.DrawTextureFitted(new Rect(inRect.x, inRect.y, inRect.width, inRect.height), Textures.SettingsBackGround, 1);

            Rect masterRect = new Rect(inRect.x + (0.1f * inRect.width), inRect.y + 40, 0.8f * inRect.width, 936);

            Listing_Standard listingStandard = new Listing_Standard();
            Rect TopSettings = new Rect(masterRect.x, masterRect.y, masterRect.width, 45);

            listingStandard.Begin(TopSettings); //column 1
            listingStandard.ColumnWidth = TopSettings.width / 3.2f;

            if (listingStandard.ButtonText("Mod active: " + DD_Settings.isDreamingActive.ToString()))
            {
                DD_Settings.isDreamingActive = !DD_Settings.isDreamingActive;
            }

            Rect MidSettings = new Rect(TopSettings.x, TopSettings.y + listingStandard.CurHeight, masterRect.width, 60);

            listingStandard.NewColumn(); // column 2
            if (DD_Settings.isDreamingActive)
            {
                if (listingStandard.ButtonText("Default settings: " + DD_Settings.isDefaultSettings.ToString()))
                {
                    DD_Settings.isDefaultSettings = !DD_Settings.isDefaultSettings;
                }
            }

            listingStandard.NewColumn(); //column 3
            if (listingStandard.ButtonText("Reset settings", "Set now"))
            {
                ResetValues();
            }

            listingStandard.End();

            Listing_Standard listingMid = new Listing_Standard();
            if (DD_Settings.isDreamingActive && !DD_Settings.isDefaultSettings)
            {
                listingMid.Begin(MidSettings);

                listingMid.GapLine();

                if (DD_Settings.sleepwalkerTraitModif == 0)
                {
                    listingMid.Label("How much sleepwalker traits affect chance for sleepwalking: " + "off");
                }
                else
                {
                    listingMid.Label("How much sleepwalker traits affect chance for sleepwalking: " + DD_Settings.sleepwalkerTraitModif * 100 + "%");
                }

                DD_Settings.sleepwalkerTraitModif = (float)Math.Round(listingMid.Slider(DD_Settings.sleepwalkerTraitModif, 0, 2), 2);
                listingMid.GapLine();

                Rect TableSettings = new Rect(masterRect.x, MidSettings.y + listingMid.CurHeight, 270f, inRect.height);
                TableSettings.height = inRect.height - TableSettings.y;

                listingMid.End();

                DrawTagsTable(TableSettings);
            }
        }

        private void DrawTagsTable(Rect inRect)
        {
            ResolveScroll(new Rect(inRect.x, inRect.y, 10f, inRect.height));

            GUI.BeginClip(inRect, scrollPos, scrollPos, false);

            Rect columnTags = new Rect(inRect.x - 68f, inRect.y - scroll - 204f, 100f, 25f);

            ModSettingsUtility.DrawTableFirstRow(ref columnTags, "Tags");

            Rect columnChance = new Rect(columnTags.x + 100, inRect.y - scroll - 204f, 146f, 25f);

            ModSettingsUtility.DrawTableFirstRow(ref columnChance, "Chance");

            DrawColumnCategory(ref columnTags);

            DrawColumnChance(ref columnChance);

            GUI.EndClip();
        }

        private void DrawRow(ref Rect row)
        {
        }

        private void DrawColumnCategory(ref Rect column)
        {
            float alternatingBGCount = 0;
            foreach (var dreamTag in DreamTracker.GetAllDreamTags)
            {
                if (dreamTag.isSideTag)
                {
                    continue;
                }
                ResolveAlternatingBG(ref alternatingBGCount, column, Textures.TableEntryBGCat1, Textures.TableEntryBGCat2);

                Widgets.Label(ModSettingsUtility.GetMiddleOfRectForString(column, dreamTag.label), dreamTag.defName);

                column.y += 25f;
            }
        }

        private void DrawColumnChance(ref Rect column)
        {
            float alternatingBGCount = 0;

            foreach (var dreamTag in DreamTracker.GetAllDreamTags)
            {
                if (dreamTag.isSideTag)
                {
                    continue;
                }
                ResolveAlternatingBG(ref alternatingBGCount, column, Textures.TableEntryBGChance1, Textures.TableEntryBGChance2);

                float chance = dreamTag.chance;
                ModSettingsUtility.CheckIfMasterListContainsAddIfNot(dreamTag, ref chance);

                ModSettingsUtility.DrawChanceButtons(column, ref chance);

                string label = Math.Round(PawnDreamTagsOddsTracker.ChanceInPercentages(chance, ModSettingsUtility.AddUpChancesForQualities()), 2) + "%";

                Widgets.Label(ModSettingsUtility.GetMiddleOfRectForString(column, label), label);

                column.y += 25f;

                DD_Settings.TagsCustomChances[dreamTag.defName] = chance;
            }
        }

        private void ResetValues()
        {
            foreach (var dreamTag in DreamTracker.GetAllDreamTags)
            {
                DD_Settings.TagsCustomChances[dreamTag.defName] = dreamTag.chance;
            }

            DD_Settings.sleepwalkerTraitModif = 1;
        }

        private void ResolveAlternatingBG(ref float alternatingBGCount, Rect column, Texture texture1, Texture texture2)
        {
            alternatingBGCount++;
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

        private void ResolveScroll(Rect inRect)
        {
            float numberOfRows = DreamTracker.GetAllDreamTags.Count + 1;

            float tableRowCapacity = inRect.height / 25f;

            if (numberOfRows > tableRowCapacity)
            {
                scroll = GUI.VerticalScrollbar(inRect, scroll, inRect.height, 0, numberOfRows * 25f);
            }
        }

        public override string SettingsCategory()
        {
            return "Dreamer's Dreams";
        }

        private static Vector2 scrollPos = Vector2.zero;

        private float scroll = 0;
    }
}