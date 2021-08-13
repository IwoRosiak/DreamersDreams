using System;
using System.Collections.Generic;
using UnityEngine;
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

        public static bool isDebugMode = false;

        public static Dictionary<string, float> QualityChanceModifs;

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref QualityChanceModifs, "DreamQualityDefs", LookMode.Value, LookMode.Value);

            Scribe_Values.Look(ref isDreamingActive, "isDreamingActive", true);

            Scribe_Values.Look(ref isDefaultSettings, "isDefaultSettings", true);

            Scribe_Values.Look(ref sleepwalkerTraitModif, "sleepwalkerTraitModif", 1);
            //Scribe_Values.Look(ref chanceForPositiveDreams, "chanceForPositiveDreams", 0);
            //Scribe_Values.Look(ref chanceForNegativeDreams, "chanceForNegativeDreams", 0);

            base.ExposeData();
        }
    }

    public class DD_Mod : Mod
    {
        private DD_Settings settings;

        public DD_Mod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<DD_Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            if (DD_Settings.QualityChanceModifs.NullOrEmpty())
            {
                DD_Settings.QualityChanceModifs = new Dictionary<string, float>();
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
            foreach (var dreamQuality in DreamTracker.DreamQualityDefs)
            {
                DD_Settings.QualityChanceModifs[dreamQuality.defName] = dreamQuality.chance;
            }

            DD_Settings.sleepwalkerTraitModif = 1;
        }

        private static Vector2 scrollPosi = Vector2.zero;

        private float scroll = 0;

        private void DrawQualityTable(Rect inRect)
        {
            ResolveScroll(new Rect(inRect.x, inRect.y, 10f, inRect.height));

            GUI.BeginClip(inRect, scrollPosi, scrollPosi, false);

            Rect columnQuality = new Rect(inRect.x - 68f, inRect.y - scroll - 204f, 100f, 25f);

            DrawTableFirstRow(ref columnQuality, "Category");

            Rect columnChance = new Rect(columnQuality.x + 100, inRect.y - scroll - 204f, 146f, 25f);

            DrawTableFirstRow(ref columnChance, "Chance");

            DrawColumnCategory(ref columnQuality);

            DrawColumnChance(ref columnChance);

            GUI.EndClip();
        }

        private void ResolveScroll(Rect inRect)
        {
            float numberOfRows = DreamTracker.DreamQualityDefs.Count + 1;

            float tableRowCapacity = inRect.height / 25f;

            if (numberOfRows > tableRowCapacity)
            {
                scroll = GUI.VerticalScrollbar(inRect, scroll, inRect.height, 0, numberOfRows * 25f);
            }
        }

        private void DrawColumnCategory(ref Rect column)
        {
            float count = 0;
            foreach (var dreamQuality in DreamTracker.DreamQualityDefs)
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

                Widgets.Label(GetMiddleOfRectForString(column, dreamQuality.defName), dreamQuality.defName);

                column.y += 25f;
            }
        }

        private void DrawColumnChance(ref Rect column)
        {
            float count = 0;

            foreach (var dreamQuality in DreamTracker.DreamQualityDefs)
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
                CheckIfMasterListContainsAddIfNot(dreamQuality, ref chance);

                DrawChanceButtons(column, ref chance);

                string label = Math.Round(PawnDreamQualityOddsTracker.ChanceInPercentages(chance, AddUpChancesForQualities()), 2) + "%";

                Widgets.Label(GetMiddleOfRectForString(column, label), label);

                column.y += 25f;

                DD_Settings.QualityChanceModifs[dreamQuality.defName] = chance;
            }
        }

        private Rect GetMiddleOfRectForString(Rect rect, string text)
        {
            return new Rect(rect.x + (rect.width / 2) - CenteredStringPos(text), rect.y, rect.width, rect.height);
        }

        private float CenteredStringPos(string text)
        {
            return (Text.CalcSize(text).x / 2);
        }

        private void CheckIfMasterListContainsAddIfNot(DreamQualityDef thingToCheck, ref float chance)
        {
            if (DD_Settings.QualityChanceModifs.ContainsKey(thingToCheck.defName))
            {
                chance = DD_Settings.QualityChanceModifs[thingToCheck.defName];
            }
            else
            {
                DD_Settings.QualityChanceModifs.Add(thingToCheck.defName, chance);
                chance = DD_Settings.QualityChanceModifs[thingToCheck.defName];
            }
        }

        private void DrawChanceButtons(Rect column, ref float chance)
        {
            column.width = 24f;

            if (Widgets.ButtonText(column, "--", true, true, new Color(30, 30, 26), true))   //Widgets.ButtonImage(columnChance, Textures.IncrementButton, true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
            {
                if (chance > 0)
                {
                    chance -= 5;
                }
                if (chance < 0)
                {
                    chance = 0;
                }
            }
            column.x += 24f;
            if (Widgets.ButtonText(column, "-", true, true, new Color(30, 30, 26), true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
            {
                if (chance > 0)
                {
                    chance--;
                }
                if (chance < 0)
                {
                    chance = 0;
                }
            }
            column.x += 74f;
            if (Widgets.ButtonText(column, "+", true, true, new Color(30, 30, 26), true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
            {
                chance++;
            }
            column.x += 24f;
            if (Widgets.ButtonText(column, "++", true, true, new Color(30, 30, 26), true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
            {
                chance += 5;
            }
        }

        private void DrawTableFirstRow(ref Rect column, string label)
        {
            if (label == "Category")
            {
                Widgets.DrawTextureFitted(column, Textures.TableEntryBGCat1, 1);
            }
            else if (label == "Chance")
            {
                Widgets.DrawTextureFitted(column, Textures.TableEntryBGChance1, 1);
            }

            Widgets.Label(GetMiddleOfRectForString(column, label), label);

            column.y += 25;
        }

        private float AddUpChancesForQualities()
        {
            float sumOfCollectionChances = 0;
            foreach (var item in DD_Settings.QualityChanceModifs)
            {
                sumOfCollectionChances += item.Value;
            }
            return sumOfCollectionChances;
        }

        public override string SettingsCategory()
        {
            return "Dreamer's Dreams";
        }
    }
}