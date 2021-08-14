using UnityEngine;
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

        public static void CheckIfMasterListContainsAddIfNot(DreamTagDef thingToCheck, ref float chance)
        {
            if (DD_Settings.TagsCustomChances.ContainsKey(thingToCheck.defName))
            {
                chance = DD_Settings.TagsCustomChances[thingToCheck.defName];
            }
            else
            {
                DD_Settings.TagsCustomChances.Add(thingToCheck.defName, chance);
                chance = DD_Settings.TagsCustomChances[thingToCheck.defName];
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

            Widgets.Label(GetMiddleOfRectForString(column, label), label);

            column.y += 25;
        }

        public static float AddUpChancesForQualities()
        {
            float sumOfCollectionChances = 0;
            foreach (var item in DD_Settings.TagsCustomChances)
            {
                sumOfCollectionChances += item.Value;
            }
            return sumOfCollectionChances;
        }
    }
}