using RimWorld;
using Verse;

namespace DreamersDream
{
    public static class DD_CalcTools
    {
        public static float EnvironmentDreamChance(DD_ThoughtDef dream, Pawn pawn)
        {
            float multiplier = 1;
            if (dream.sensitivities != null)
            {
                foreach (DD_ThoughtDef.Sensitivities sensitivity in dream.sensitivities)
                {
                    switch (sensitivity)
                    {
                        case DD_ThoughtDef.Sensitivities.ill:
                            if (pawn.health.hediffSet.AnyHediffMakesSickThought)
                            {
                                multiplier += DD_Settings.chanceMultiplierForIlness / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.healthy:
                            if (!pawn.health.hediffSet.AnyHediffMakesSickThought && !pawn.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Serious))
                            {
                                multiplier += DD_Settings.chanceMultiplierForIlness / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.injured:
                            if (pawn.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Serious))
                            {
                                multiplier += DD_Settings.chanceMultiplierForIlness / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.goodTemp:
                            if (pawn.AmbientTemperature > GenTemperature.ComfortableTemperatureRange(pawn).TrueMin && pawn.AmbientTemperature < GenTemperature.ComfortableTemperatureRange(pawn).TrueMax)
                            {
                                multiplier += DD_Settings.chanceMultiplierForTemperature / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.hot:
                            if (pawn.AmbientTemperature > GenTemperature.ComfortableTemperatureRange(pawn).TrueMax)
                            {
                                multiplier += DD_Settings.chanceMultiplierForTemperature / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.cold:
                            if (pawn.AmbientTemperature < GenTemperature.ComfortableTemperatureRange(pawn).TrueMin)
                            {
                                multiplier += DD_Settings.chanceMultiplierForTemperature / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.hungry:
                            if (pawn.needs.food.CurLevelPercentage < pawn.needs.food.PercentageThreshHungry)
                            {
                                switch (pawn.needs.food.CurCategory)
                                {
                                    case HungerCategory.Starving:
                                        multiplier += 0.4f * DD_Settings.chanceMultiplierForHunger / 100;
                                        break;
                                    case HungerCategory.UrgentlyHungry:
                                        multiplier += 0.25f * DD_Settings.chanceMultiplierForHunger / 100;
                                        break;
                                    case HungerCategory.Hungry:
                                        multiplier += 0.1f * DD_Settings.chanceMultiplierForHunger / 100;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.malnourished:
                            var malnutrition = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Malnutrition);
                            if (malnutrition != null)
                            {
                                float malnourishedMultiplier = 0;


                                foreach (var stage in HediffDefOf.Malnutrition.stages)
                                {
                                    if (malnutrition.CurStage == stage)
                                    {
                                        switch (stage.label)
                                        {
                                            case "trivial":
                                                malnourishedMultiplier = 0.1f;
                                                break;
                                            case "minor":
                                                malnourishedMultiplier = 0.4f;
                                                break;
                                            case "moderate":
                                                malnourishedMultiplier = 0.7f;
                                                break;
                                            case "severe":
                                                malnourishedMultiplier = 1.0f;
                                                break;
                                            case "extreme":
                                                malnourishedMultiplier = 2.0f;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }

                                multiplier += malnourishedMultiplier * DD_Settings.chanceMultiplierForMalnourished / 100;
                            }
                            break;
                        default:
                            break;
                    }
                    /*if (sensitivity == "noPart" && pawn.health.hediffSet.)
                    {
                        multiplier += DD_Settings.chanceMultiplierForIlness / 100;
                    }*/
                }
            }
            return multiplier;
        }

        public static float CheckSettingsDream(float typeOfDream)
        {
            if (typeOfDream > 0)
            {
                return 1 + (DD_Settings.chanceForPositiveDreams / 100);
            }
            else if (typeOfDream < 0)
            {
                return 1 + (DD_Settings.chanceForNegativeDreams / 100);
            }
            else if (typeOfDream == 0)
            {
                return 1 + (DD_Settings.chanceForNoDream / 100);
            }
            return 0;
        }

        public static float CheckSettingsSleepwalk(DD_ThoughtDef dream)
        {
            if (!DD_Settings.isSleepwalkingActive)
            {
                return 0;
            }
            foreach (DD_MentalStateDef state in dream.triggers)
            {
                if (!DD_Settings.isSleepBerserkActive && state == DD_MentalStateDefOf.SleepwalkBerserk)
                {
                    return 0;
                }
                else if (!DD_Settings.isSleepNormalActive && (state == DD_MentalStateDefOf.Sleepwalk || state == DD_MentalStateDefOf.SleepwalkOwnRoom || state == DD_MentalStateDefOf.SleepwalkSafe))
                {
                    return 0;
                }
                else if (!DD_Settings.isSleepTantrumActive && state == DD_MentalStateDefOf.SleepwalkTantrum)
                {
                    return 0;
                }
                else if (!DD_Settings.isSleepFoodBingeActive && state == DD_MentalStateDefOf.SleepwalkBingingFood)
                {
                    return 0;
                }
            }
            return 1 + DD_Settings.chanceForSleepwalkingDreams / 100;
        }
    }
}
