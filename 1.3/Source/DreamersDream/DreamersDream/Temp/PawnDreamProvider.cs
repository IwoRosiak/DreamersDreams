using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class PawnDreamProvider
    {
        public static DreamDef ProvideDream()
        {
            var dreamQuality = GetRandomDreamCategory();

            return null;
        }

        private static DreamQualityDef GetRandomDreamCategory()
        {
            return null;
        }

        private static DreamDef GetRandomDream()
        {
            var dreamChanceProgress = 0.0f;

            float dreamChanceRoll = GetRandomNumberForThisDreamQuality();

            foreach (DreamDef dream in DreamTracker.DreamDefs)
            {
                var chanceForDream = dream.chance; //DD_Utility.CheckDreamChance(dream, pawn);

                if (dreamChanceRoll < dreamChanceProgress + chanceForDream)
                {
                    return dream;
                }
                else
                {
                    dreamChanceProgress += chanceForDream;
                }
            }
            Log.Error("ChooseDream() was called but it did not select a dream.");
            return null;
        }

        private static float GetRandomNumberForThisDreamQuality()
        {
            float totalDreamChance = 0;
            foreach (DreamDef dream in DreamTracker.DreamDefs)
            {
                totalDreamChance += dream.chance;         //DD_Utility.CheckDreamChance(dream, pawn);
            }
            return Rand.Range(0, totalDreamChance);
        }
    }
}

/*
private void DrawQualityTable(Rect inRect)
        {
            Rect columnQuality = new Rect(inRect.x + 45, inRect.y, 100f, 25f);

            //Widgets.DrawBox(new Rect(inRect.x - 20, inRect.y - 20, 120f, 45f), 10, Textures.BoxFrameCourner);
            Widgets.DrawTextureFitted(columnQuality, Textures.BoxFrameCourner, 1);

            columnQuality.x += 10f;
            Widgets.Label(columnQuality, "Category ");
            columnQuality.x -= 10f;

            Rect columnChance = new Rect(columnQuality.x + 100, inRect.y, 146f, 25f);

            Widgets.DrawTextureFitted(columnChance, Textures.BoxFrameCourner, 1);
            Widgets.Label(columnChance, "Chance ");

            columnChance.y += 25;
            columnQuality.y += 25;

            foreach (var dreamQuality in DreamTracker.DreamQualityDefs)
            {
                float chance = dreamQuality.chance;

                if (DD_Settings.QualityChanceModifs.ContainsKey(dreamQuality.defName))
                {
                    chance = DD_Settings.QualityChanceModifs[dreamQuality.defName];
                }
                else
                {
                    DD_Settings.QualityChanceModifs.Add(dreamQuality.defName, chance);
                    chance = DD_Settings.QualityChanceModifs[dreamQuality.defName];
                }
                columnQuality.x += 10f;
                Widgets.Label(columnQuality, dreamQuality.defName);
                columnQuality.x -= 10f;
                Widgets.DrawBox(columnQuality);
                columnQuality.y += 25f;

                Widgets.DrawTextureFitted(columnChance, Textures.BoxFrameCourner, 1);
                columnChance.width = 24f;

                if (Widgets.Button ButtonText(columnChance, "--", true, true, new Color(30, 30, 26), true))   //Widgets.ButtonImage(columnChance, Textures.IncrementButton, true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
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
                columnChance.x += 24f;
                if (Widgets.ButtonText(columnChance, "-", true, true, new Color(30, 30, 26), true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
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
                columnChance.width = 50f;
                columnChance.x += 24f;
                Widgets.Label(columnChance, Math.Round(PawnDreamQualityOddsTracker.ChanceInPercentages(DD_Settings.QualityChanceModifs[dreamQuality.defName], AddUpChancesForQualities()), 2) + "%");
                columnChance.x += 50f;
                columnChance.width = 24f;
                if (Widgets.ButtonText(columnChance, "+", true, true, new Color(30, 30, 26), true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
                {
                    chance++;
                }
                columnChance.x += 24f;
                if (Widgets.ButtonText(columnChance, "++", true, true, new Color(30, 30, 26), true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
                {
                    chance += 5;
                }
                columnChance.width = 146f;
                columnChance.x -= 122f;

                //Widgets.DrawBox(columnChance);

                columnChance.y += 25f;

                DD_Settings.QualityChanceModifs[dreamQuality.defName] = chance;
            }
        }

foreach (var dreamQuality in DreamTracker.DreamQualityDefs)
            {
                float chance = dreamQuality.chance;

                if (DD_Settings.QualityChanceModifs.ContainsKey(dreamQuality.defName))
                {
                    chance = DD_Settings.QualityChanceModifs[dreamQuality.defName];
                }
                else
                {
                    DD_Settings.QualityChanceModifs.Add(dreamQuality.defName, chance);
                    chance = DD_Settings.QualityChanceModifs[dreamQuality.defName];
                }
                columnQuality.x += 10f;
                Widgets.Label(columnQuality, dreamQuality.defName);
                columnQuality.x -= 10f;
                Widgets.DrawBox(columnQuality);
                columnQuality.y += 25f;

                Widgets.DrawTextureFitted(columnChance, Textures.BoxFrameCourner, 1);
                columnChance.width = 24f;

                if (Widgets.ButtonText(columnChance, "--", true, true, new Color(30, 30, 26), true))   //Widgets.ButtonImage(columnChance, Textures.IncrementButton, true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
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
                columnChance.x += 24f;
                if (Widgets.ButtonText(columnChance, "-", true, true, new Color(30, 30, 26), true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
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
                columnChance.width = 50f;
                columnChance.x += 24f;
                Widgets.Label(columnChance, Math.Round(PawnDreamQualityOddsTracker.ChanceInPercentages(DD_Settings.QualityChanceModifs[dreamQuality.defName], AddUpChancesForQualities()), 2) + "%");
                columnChance.x += 50f;
                columnChance.width = 24f;
                if (Widgets.ButtonText(columnChance, "+", true, true, new Color(30, 30, 26), true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
                {
                    chance++;
                }
                columnChance.x += 24f;
                if (Widgets.ButtonText(columnChance, "++", true, true, new Color(30, 30, 26), true)) //(columnChance, button.MatSingle.GetMaskTexture(), true);
                {
                    chance += 5;
                }
                columnChance.width = 146f;
                columnChance.x -= 122f;

                //Widgets.DrawBox(columnChance);

                columnChance.y += 25f;

                DD_Settings.QualityChanceModifs[dreamQuality.defName] = chance;
            }

*/