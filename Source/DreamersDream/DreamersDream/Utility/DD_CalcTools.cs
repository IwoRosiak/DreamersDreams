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
                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                {
                                    Log.Message("Chance increased because sick");
                                }
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.healthy:
                            if (!pawn.health.hediffSet.AnyHediffMakesSickThought && !pawn.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Serious))
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForIlness / 100;
                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                {
                                    Log.Message("Chance increased because healthy");
                                }
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.injured:
                            if (pawn.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Serious))
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForIlness / 100;
                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                {
                                    Log.Message("Chance increased because injured");
                                }
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.goodTemp:
                            if (pawn.AmbientTemperature > GenTemperature.ComfortableTemperatureRange(pawn).TrueMin && pawn.AmbientTemperature < GenTemperature.ComfortableTemperatureRange(pawn).TrueMax)
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForTemperature / 100;
                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                {
                                    Log.Message("Chance increased because good temp");
                                }
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.hot:
                            if (pawn.AmbientTemperature > GenTemperature.ComfortableTemperatureRange(pawn).TrueMax)
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForTemperature / 100;
                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                {
                                    Log.Message("Chance increased because hot");
                                }
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.cold:
                            if (pawn.AmbientTemperature < GenTemperature.ComfortableTemperatureRange(pawn).TrueMin)
                            {
                                multiplier += (float)DD_Settings.chanceMultiplierForTemperature / 100;
                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                {
                                    Log.Message("Chance increased because cold");
                                }
                            }
                            break;
                        case DD_ThoughtDef.Sensitivities.hungry:
                            if (pawn.needs.food.CurLevelPercentage < pawn.needs.food.PercentageThreshHungry)
                            {
                                switch (pawn.needs.food.CurCategory)
                                {
                                    case HungerCategory.Starving:
                                        multiplier += 0.4f * (float)DD_Settings.chanceMultiplierForHunger / 100;
                                        if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                        {
                                            Log.Message("Chance increased because starving");
                                        }
                                        break;
                                    case HungerCategory.UrgentlyHungry:
                                        multiplier += 0.25f * (float)DD_Settings.chanceMultiplierForHunger / 100;
                                        if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                        {
                                            Log.Message("Chance increased because very hungry");
                                        }
                                        break;
                                    case HungerCategory.Hungry:
                                        multiplier += 0.1f * (float)DD_Settings.chanceMultiplierForHunger / 100;
                                        if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                        {
                                            Log.Message("Chance increased because hungry");
                                        }
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
                                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                                {
                                                    Log.Message("Chance increased because trivial malnourished");
                                                }
                                                break;
                                            case "minor":
                                                malnourishedMultiplier = 0.4f;
                                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                                {
                                                    Log.Message("Chance increased because minor malnourished");
                                                }
                                                break;
                                            case "moderate":
                                                malnourishedMultiplier = 0.7f;
                                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                                {
                                                    Log.Message("Chance increased because moderate malnourished");
                                                }
                                                break;
                                            case "severe":
                                                malnourishedMultiplier = 1.0f;
                                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                                {
                                                    Log.Message("Chance increased because severe malnourished");
                                                }
                                                break;
                                            case "extreme":
                                                malnourishedMultiplier = 2.0f;
                                                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                                                {
                                                    Log.Message("Chance increased because extreme malnourished");
                                                }
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

        public static float CheckSleepwalkerTrait(Pawn pawn)
        {
            float traitMultiplier = 1;
            var traitDegree = pawn.story.traits.DegreeOfTrait(DD_TraitDefOf.Sleepwalker);

            switch (traitDegree)
            {
                case 1:
                    traitMultiplier += 50f * ((float)DD_Settings.traitMultiplierForSleepwalking / 100);
                    break;
                case 2:
                    traitMultiplier += 100f * ((float)DD_Settings.traitMultiplierForSleepwalking / 100);
                    break;
                case 3:
                    traitMultiplier += 200.0f * ((float)DD_Settings.traitMultiplierForSleepwalking / 100);
                    break;
                default:
                    break;
            }

            return traitMultiplier;

        }

        public static float TraitDreamChance(DD_ThoughtDef dream, Pawn pawn)
        {

            float traitMultiplier = 1;
            if (pawn.story.traits.HasTrait(DD_TraitDefOf.Sleepwalker))
            {
                if (DD_Settings.isDebugMode && dream.defName == "DebugDream" && pawn.NameShortColored == "TEST")
                {
                    Log.Message("Chance increased because of sleepwalker trait");
                }

                foreach (DD_MentalStateDef state in dream.triggers)
                {
                    if (state == DD_MentalStateDefOf.SleepwalkBerserk && (pawn.story.traits.HasTrait(TraitDefOf.Bloodlust) || pawn.story.traits.HasTrait(TraitDefOf.Psychopath)))
                    {
                        traitMultiplier = CheckSleepwalkerTrait(pawn) * 10.0f;
                    }
                    else if (state == DD_MentalStateDefOf.SleepwalkTantrum && pawn.story.traits.HasTrait(TraitDefOf.NaturalMood))
                    {
                        if (pawn.story.traits.DegreeOfTrait(TraitDefOf.NaturalMood) == -1)
                        {
                            traitMultiplier = CheckSleepwalkerTrait(pawn) * 5.0f;
                        }
                        else if (pawn.story.traits.DegreeOfTrait(TraitDefOf.NaturalMood) == -2)
                        {
                            traitMultiplier = CheckSleepwalkerTrait(pawn) * 10.0f;
                        }
                    }
                    else if (state == DD_MentalStateDefOf.SleepwalkBingingFood && pawn.story.traits.HasTrait(DD_TraitDefOf.Gourmand))
                    {
                        traitMultiplier = CheckSleepwalkerTrait(pawn) * 10.0f;
                    }
                    else if (state == DD_MentalStateDefOf.Sleepwalk || state == DD_MentalStateDefOf.SleepwalkOwnRoom || state == DD_MentalStateDefOf.SleepwalkSafe)
                    {
                        traitMultiplier = CheckSleepwalkerTrait(pawn);
                    }
                }

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
