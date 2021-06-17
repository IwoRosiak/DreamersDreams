using RimWorld;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class DD_Utility
    {
        static DD_Utility()
        {
        }

        public static bool IsAwake(Pawn pawn)
        {
            if (pawn.needs.rest.GUIChangeArrow == 1)
            {
                return true;
            }
            else if (pawn.needs.rest.GUIChangeArrow == -1)
            {
                return false;
            }
            return true;
        }

        public static float CheckDreamChance(DD_ThoughtDef dream, Pawn pawn)
        {


            float chanceMutliplier = 1;

            if (dream.triggers != null)
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
                }
                chanceMutliplier = DD_Settings.chanceForSleepwalkingDreams / 100;
            }
            /*else if (!DD_Settings.isDreamingActive)
            {
                return 0;
            }*/
            else
            {
                float typeOfDream = dream.stages[0].baseMoodEffect;
                if (typeOfDream > 0)
                {
                    chanceMutliplier = DD_Settings.chanceForPositiveDreams / 100;
                }
                else if (typeOfDream < 0)
                {
                    chanceMutliplier = DD_Settings.chanceForNegativeDreams / 100;
                }
                else if (typeOfDream == 0)
                {
                    chanceMutliplier = DD_Settings.chanceForNoDream / 100;
                }
            }
            var environmentMultiplier = EnvironmentDreamChance(dream, pawn);

            /*if (CheckForHigh() && dream.defName == "VeryGoodDream")
            {
                return dream.chance + 1000;

            }*/
            return dream.chance + (dream.chance * chanceMutliplier) * (dream.chance * environmentMultiplier);
        }

        public static float EnvironmentDreamChance(DD_ThoughtDef dream, Pawn pawn)
        {
            float multiplier = 1;
            if (dream.sensitivities != null)
            {
                foreach (string sensitivity in dream.sensitivities)
                {
                    if (sensitivity == "ilness" && pawn.health.hediffSet.AnyHediffMakesSickThought)
                    {
                        multiplier += DD_Settings.chanceMultiplierForIlness / 100;
                    }

                    if (sensitivity == "injured" && pawn.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Serious))
                    {
                        multiplier += DD_Settings.chanceMultiplierForIlness / 100;
                    }

                    if (sensitivity == "healthy" && !pawn.health.hediffSet.AnyHediffMakesSickThought && !pawn.health.hediffSet.HasTemperatureInjury(TemperatureInjuryStage.Serious))
                    {
                        multiplier += DD_Settings.chanceMultiplierForIlness / 100;
                    }

                    /*if (sensitivity == "noPart" && pawn.health.hediffSet.)
                    {
                        multiplier += DD_Settings.chanceMultiplierForIlness / 100;
                    }*/


                    if (sensitivity == "goodTemperature" && pawn.AmbientTemperature > GenTemperature.ComfortableTemperatureRange(pawn).TrueMin && pawn.AmbientTemperature < GenTemperature.ComfortableTemperatureRange(pawn).TrueMax)
                    {
                        multiplier += DD_Settings.chanceMultiplierForTemperature / 100;
                    }

                    if (sensitivity == "temperatureHot" && pawn.AmbientTemperature > GenTemperature.ComfortableTemperatureRange(pawn).TrueMax)
                    {
                        multiplier += DD_Settings.chanceMultiplierForTemperature / 100;
                    }

                    if (sensitivity == "temperatureCold" && pawn.AmbientTemperature < GenTemperature.ComfortableTemperatureRange(pawn).TrueMin)
                    {
                        multiplier += DD_Settings.chanceMultiplierForTemperature / 100;
                    }

                    if (sensitivity == "hunger" && pawn.needs.food.CurLevelPercentage < pawn.needs.food.PercentageThreshHungry)
                    {
                        float hungerMultiplier = 0;
                        switch (pawn.needs.food.CurCategory)
                        {
                            case HungerCategory.Hungry:
                                hungerMultiplier = 0.1f;
                                break;
                            case HungerCategory.UrgentlyHungry:
                                hungerMultiplier = 0.25f;
                                break;
                            case HungerCategory.Starving:
                                hungerMultiplier = 0.40f;
                                break;
                            default:
                                break;
                        }
                        multiplier += hungerMultiplier * DD_Settings.chanceMultiplierForHunger / 100;
                    }

                    var malnutrition = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Malnutrition);
                    if (sensitivity == "malnourished" && malnutrition != null)
                    {
                        float malnourishedMultiplier = 0;


                        foreach (var stage in HediffDefOf.Malnutrition.stages)
                        {
                            if (malnutrition.CurStage == stage)
                            {
                                switch (stage.label.ToString())
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
                }
            }
            return multiplier;
        }

        public static bool CheckForHigh(Pawn pawn)
        {
            for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[i];

                if (hediff.def.defName == "DreamHigh")
                {
                    //Messages.Message("Dream High Detected!", MessageTypeDefOf.NeutralEvent);
                    return true;
                }
            }
            return false;
        }





        /* public virtual void MentalStateTick()
         {
             if (this.pawn.IsHashIntervalTick(150))
             {
                 this.age += 150;
                 if (this.age >= this.def.maxTicksBeforeRecovery || (this.age >= this.def.minTicksBeforeRecovery && this.CanEndBeforeMaxDurationNow && Rand.MTBEventOccurs(this.def.recoveryMtbDays, 60000f, 150f)) || (this.forceRecoverAfterTicks != -1 && this.age >= this.forceRecoverAfterTicks))
                 {
                     this.RecoverFromState();
                     return;
                 }
                 if (this.def.recoverFromSleep && !this.pawn.Awake())
                 {
                     this.RecoverFromState();
                     return;
                 }
             }
         } CHECK BASED ON AGE*/
    }
}
