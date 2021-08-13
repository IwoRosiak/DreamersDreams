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
            if (DD_Settings.TagsChanceModifs.NullOrEmpty())
            {
                DD_Settings.TagsChanceModifs = new Dictionary<string, float>();
            }

            Rect masterRect = new Rect(inRect.x + (0.1f * inRect.width), inRect.y + 40, 0.8f * inRect.width, 936);

            Widgets.DrawTextureFitted(new Rect(inRect.x, inRect.y, inRect.width, inRect.height), Textures.SettingsBackGround, 1);

            Listing_Standard listingStandard = new Listing_Standard();

            Rect TopSettings = new Rect(masterRect.x, masterRect.y, masterRect.width, 45);

            listingStandard.Begin(TopSettings);

            listingStandard.ColumnWidth = TopSettings.width / 3.2f;

            if (listingStandard.ButtonText("Mod active: " + DD_Settings.isDreamingActive.ToString()))
            {
                DD_Settings.isDreamingActive = !DD_Settings.isDreamingActive;
            }

            Rect MidSettings = new Rect(TopSettings.x, TopSettings.y + listingStandard.CurHeight, masterRect.width, 60);//936 - listingStandard.CurHeight);
            listingStandard.NewColumn();

            if (DD_Settings.isDreamingActive)
            {
                if (listingStandard.ButtonText("Default settings: " + DD_Settings.isDefaultSettings.ToString()))
                {
                    DD_Settings.isDefaultSettings = !DD_Settings.isDefaultSettings;
                }
            }

            listingStandard.NewColumn();

            if (listingStandard.ButtonText("Reset settings", "Set now"))
            {
                ResetValues();
            }

            listingStandard.End();

            Listing_Standard listingMid = new Listing_Standard();
            Listing_Standard listingTop = new Listing_Standard();
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

                // = Widgets.HorizontalSlider(new Rect(TopSettings.x, ref cur), 50f, DD_Settings.sleepwalkerTraitModif, 0, 5, false, null, "0%", "500%", 0.01f);
                //Widgets.Label(TopSettings.x, ref ScrollPos.y, 150f, "Sleepwalker trait modifier " + DD_Settings.sleepwalkerTraitModif * 100 + "%");
                Rect TableSettings = new Rect(masterRect.x, masterRect.y + listingMid.CurHeight * 2.1f, 270f, inRect.height);
                TableSettings.height = inRect.height - TableSettings.y;

                listingMid.End();

                //listingTop.GapLine();

                DrawQualityTable(TableSettings);
            }
        }

        private void ResetValues()
        {
            foreach (var dreamQuality in DreamTracker.DreamTagsDefs)
            {
                DD_Settings.TagsChanceModifs[dreamQuality.defName] = dreamQuality.chance;
            }

            DD_Settings.sleepwalkerTraitModif = 1;
        }

        private static Vector2 scrollPos = Vector2.zero;

        private float scroll = 0;

        private void DrawQualityTable(Rect inRect)
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

        private void ResolveScroll(Rect inRect)
        {
            float numberOfRows = DreamTracker.DreamTagsDefs.Count + 1;

            float tableRowCapacity = inRect.height / 25f;

            if (numberOfRows > tableRowCapacity)
            {
                scroll = GUI.VerticalScrollbar(inRect, scroll, inRect.height, 0, numberOfRows * 25f);
            }
        }

        private void DrawColumnCategory(ref Rect column)
        {
            float count = 0;
            foreach (var dreamTag in DreamTracker.DreamTagsDefs)
            {
                count++;
                switch (count % 2)
                {
                    case 0:
                        Widgets.DrawTextureFitted(column, Textures.TableEntryBGCat1, 1);
                        break;

                    case 1:
                        Widgets.DrawTextureFitted(column, Textures.TableEntryBGCat2, 1);
                        break;

                    default:
                        break;
                }

                Widgets.Label(ModSettingsUtility.GetMiddleOfRectForString(column, dreamTag.defName), dreamTag.defName);

                column.y += 25f;
            }
        }

        private void DrawColumnChance(ref Rect column)
        {
            float count = 0;

            foreach (var dreamQuality in DreamTracker.DreamTagsDefs)
            {
                count++;
                switch (count % 2)
                {
                    case 0:
                        Widgets.DrawTextureFitted(column, Textures.TableEntryBGChance1, 1);
                        break;

                    case 1:
                        Widgets.DrawTextureFitted(column, Textures.TableEntryBGChance2, 1);
                        break;

                    default:
                        break;
                }

                float chance = dreamQuality.chance;
                ModSettingsUtility.CheckIfMasterListContainsAddIfNot(dreamQuality, ref chance);

                ModSettingsUtility.DrawChanceButtons(column, ref chance);

                string label = Math.Round(PawnDreamTagsOddsTracker.ChanceInPercentages(chance, ModSettingsUtility.AddUpChancesForQualities()), 2) + "%";

                Widgets.Label(ModSettingsUtility.GetMiddleOfRectForString(column, label), label);

                column.y += 25f;

                DD_Settings.TagsChanceModifs[dreamQuality.defName] = chance;
            }
        }

        public override string SettingsCategory()
        {
            return "Dreamer's Dreams";
        }
    }
}