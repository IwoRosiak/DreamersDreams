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
                    //if (dream.defName == "DebugDream" && false)
                    //{
                    //    Log.Message(sensitivity.ToString());
                    //}
                    /*if (pawn.NameShortColored == "TEST")
                    {
                        Log.Message(sensitivity + " success");
                    }*/
                    switch (sensitivity)
                    {
                        case DD_ThoughtDef.Sensitivities.ill:
                            if (pawn.health.hediffSet.AnyHediffMakesSickThought)
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForIlness / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.healthy:
                            if (!pawn.health.hediffSet.AnyHediffMakesSickThought && !pawn.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Serious))
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForIlness / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.injured:
                            if (pawn.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Serious))
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForIlness / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.goodTemp:
                            if (pawn.AmbientTemperature > GenTemperature.ComfortableTemperatureRange(pawn).TrueMin && pawn.AmbientTemperature < GenTemperature.ComfortableTemperatureRange(pawn).TrueMax)
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForTemperature / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.hot:
                            if (pawn.AmbientTemperature > GenTemperature.ComfortableTemperatureRange(pawn).TrueMax)
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForTemperature / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.cold:
                            if (pawn.AmbientTemperature < GenTemperature.ComfortableTemperatureRange(pawn).TrueMin)
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForTemperature / 100;
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.hungry:
                            if (pawn.needs.food.CurLevelPercentage < pawn.needs.food.PercentageThreshHungry)
                            {
                                switch (pawn.needs.food.CurCategory)
                                {
                                    case HungerCategory.Starving:
                                        multiplier += 0.4f * (float)DD_Settings.chanceMultiplierForHunger / 100;
                                        break;
                                    case HungerCategory.UrgentlyHungry:
                                        multiplier += 0.25f * (float)DD_Settings.chanceMultiplierForHunger / 100;
                                        break;
                                    case HungerCategory.Hungry:
                                        multiplier += 0.1f * (float)DD_Settings.chanceMultiplierForHunger / 100;
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

                                multiplier += malnourishedMultiplier * (float)DD_Settings.chanceMultiplierForMalnourished / 100;
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

        public static float TraitDreamChance(DD_ThoughtDef dream, Pawn pawn)
        {
            float traitMultiplier = 1;
            if (pawn.story.traits.HasTrait(DD_TraitDefOf.Sleepwalker))
            {
                var traitDegree = pawn.story.traits.DegreeOfTrait(DD_TraitDefOf.Sleepwalker);

                switch (traitDegree)
                {
                    case 1:
                        traitMultiplier += 1f * DD_Settings.traitMultiplierForSleepwalking;
                        break;
                    case 2:
                        traitMultiplier += 2.5f * DD_Settings.traitMultiplierForSleepwalking;
                        break;
                    case 3:
                        traitMultiplier += 5.0f * DD_Settings.traitMultiplierForSleepwalking;
                        break;
                    default:
                        break;
                }

                /*foreach (DD_MentalStateDef state in dream.triggers)
                {
                    if (state == DD_MentalStateDefOf.SleepwalkBerserk && pawn.story.traits.HasTrait(TraitDefOf.Bloodlust) || pawn.story.traits.HasTrait(TraitDefOf.Psychopath))
                    {
                        traitMultiplier += 2.0f;
                    }
                    else if (state == DD_MentalStateDefOf.SleepwalkTantrum && pawn.story.traits.HasTrait(TraitDefOf.NaturalMood))
                    {
                        if (pawn.story.traits.DegreeOfTrait(TraitDefOf.NaturalMood) == -1)
                        {
                            traitMultiplier = 1.5f;
                        }
                        else if (pawn.story.traits.DegreeOfTrait(TraitDefOf.NaturalMood) == -2)
                        {
                            traitMultiplier = 2.0f;
                        }
                    }
                    else if (state == DD_MentalStateDefOf.SleepwalkBingingFood && pawn.story.traits.HasTrait(DD_TraitDefOf.Gourmand))
                    {
                        traitMultiplier = 2.0f;
                    }
                    else if (state == DD_MentalStateDefOf.Sleepwalk || state == DD_MentalStateDefOf.SleepwalkOwnRoom || state == DD_MentalStateDefOf.SleepwalkSafe)
                    {
                        traitMultiplier = 3.0f;
                    }
                }*/
            }
            return traitMultiplier;
        }

        public static float CheckSettingsDream(float typeOfDream)
        {
            if (typeOfDream > 0)
            {
                return 1 + ((float)DD_Settings.chanceForPositiveDreams / 100);
            }
            else if (typeOfDream < 0)
            {
                return 1 + ((float)DD_Settings.chanceForNegativeDreams / 100);
            }
            else if (typeOfDream == 0)
            {
                return 1 + ((float)DD_Settings.chanceForNoDream / 100);
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
