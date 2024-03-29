﻿using UnityEngine;
using Verse;

namespace DreamersDream
{
    internal static class ModSettingsUtility
    {
        public static Rect GetMiddleOfRectForString(Rect rect, string text)
        {
            return new Rect(rect.x + (rect.width / 2) - CenteredStringPos(text), rect.y, rect.width, rect.height);
        }

        public static float CenteredStringPos(string text)
        {
            return (Text.CalcSize(text).x / 2);
        }

        public static float ResolveCustomChanceForTag(DreamTagDef tag)
        {
            if (DD_Settings.TagsCustomChances.ContainsKey(tag.defName) && DD_Settings.TagsCustomChances[tag.defName] != tag.chance)
            {
                return DD_Settings.TagsCustomChances[tag.defName];
            }
            else if (DD_Settings.TagsCustomChances.ContainsKey(tag.defName) && DD_Settings.TagsCustomChances[tag.defName] == tag.chance)
            {
                DD_Settings.TagsCustomChances.Remove(tag.defName);
                return tag.chance;
            }
            else
            {
                return tag.chance;
            }
        }

        public static void UpdateCustomChanceForTag(DreamTagDef tag, float chance)
        {
            if (!DD_Settings.TagsCustomChances.ContainsKey(tag.defName) && chance != tag.chance)
            {
                DD_Settings.TagsCustomChances.Add(tag.defName, chance);
            }
            else if (DD_Settings.TagsCustomChances.ContainsKey(tag.defName) && chance != tag.chance)
            {
                DD_Settings.TagsCustomChances[tag.defName] = chance;
            }
            else if (DD_Settings.TagsCustomChances.ContainsKey(tag.defName) && chance == tag.chance)
            {
                DD_Settings.TagsCustomChances.Remove(tag.defName);
            }
        }

        public static void CheckIfHasCustomNotifyAndAddIfNot(DreamTagDef thingToCheck, ref bool notify)
        {
            if (DD_Settings.TagsCustomNotify.ContainsKey(thingToCheck.defName))
            {
                notify = DD_Settings.TagsCustomNotify[thingToCheck.defName];
            }
            else
            {
                DD_Settings.TagsCustomNotify.Add(thingToCheck.defName, notify);
                notify = DD_Settings.TagsCustomNotify[thingToCheck.defName];
            }
        }

        public static void DrawChanceButtons(Rect column, ref float chance)
        {
            column.width = 24f;

            if (Widgets.ButtonText(column, "--", true, true, new Color(30, 30, 26), true))   //Widgets.ButtonImage(columnChance, Textures.IncrementButton, true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
            {
                if (chance > 0)
                {
                    chance -= 10;
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
                chance += 10;
            }
        }

        public static void DrawTableFirstRow(ref Rect column, string label)
        {
            if (label == "Tags")
            {
                Widgets.DrawTextureFitted(column, Textures.TableEntryBGCat1, 1);
            }
            else if (label == "Chance")
            {
                Widgets.DrawTextureFitted(column, Textures.TableEntryBGChance1, 1);
            }
            else if (label == "Notify")
            {
                Widgets.DrawTextureFitted(column, Textures.TableEntryBGNotify1, 1);
            }

            Widgets.Label(GetMiddleOfRectForString(column, label), label);

            column.y += 25;
        }

        public static float AddUpChancesForQualities()
        {
            float sumOfCollectionChances = 0;
            foreach (var dreamTag in DreamTracker.GetAllDreamTags)
            {
                float chanceForTag = dreamTag?.CalculateChanceFor() ?? 0;

                sumOfCollectionChances += chanceForTag;
            }
            return sumOfCollectionChances;
        }
    }
}