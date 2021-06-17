using HarmonyLib;
using RimWorld.Planet;
using System.Reflection;
using Verse;
using static DreamersDream.DD_Utility;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        //Verse.LoadedModManager.GetMod<DD_Mod>().GetSettings<ExampleSettings>().exampleBool

        static HarmonyPatches()
        {
            var harmony = new Harmony("com.company.QarsoonMeel.DreamersDreams");

            MethodInfo targetmethod = AccessTools.Method(typeof(Verse.Pawn), "Tick");

            HarmonyMethod postfixmethod = new HarmonyMethod(typeof(DreamersDream.HarmonyPatches).GetMethod("CheckSleep_Prefix"));

            harmony.Patch(targetmethod, postfixmethod, null);

        }
        public static void CheckSleep_Prefix(Pawn __instance)
        {
            /*if (__instance.NameShortColored == "TEST" && __instance.AmbientTemperature > GenTemperature.SafeTemperatureRange(__instance).TrueMax)
            {

                Messages.Message(__instance.jobs.curJob.ToString(), RimWorld.MessageTypeDefOf.NeutralEvent);

            }*/

            var currentTime = Find.TickManager.TicksGame;

            //is the instance a colonist and is it dead
            if (__instance.RaceProps.Humanlike && !__instance.Dead && (__instance.Spawned || CaravanUtility.IsCaravanMember(__instance)))
            {
                //needs to use different classes but it checks if the pawn is resting
                if (IsAwake(__instance))                       //      !(__instance.health.capacities.CanBeAwake && (!__instance.Spawned || __instance.CurJob == null || __instance.jobs.curDriver == null || !__instance.jobs.curDriver.asleep)))
                {
                    //variable that holds whether the pawn can get a dream or not
                    var eligibleForDream = false;

                    //checks if pawn is on the dictionary to check last time it was awake
                    if (SleepingPawnTracker.SleepingList.ContainsKey(__instance))
                    {
                        //saves the time the pawn has fallen asleep
                        var pawnFallenAsleepTime = SleepingPawnTracker.SleepingList[__instance];
                        //Messages.Message("First sleep: " + pawnFallenAsleepTime + "Current time :" + currentTime, MessageTypeDefOf.NeutralEvent); //debug message about sleeping times

                        //timeToDream is the time pawn needs to sleep to be eligible for a dream
                        var timeToDream = Rand.Range(7500, 10000);

                        //checks if pawn has slepted enough to get a dream. If yes then it checks if the pawn has a dream already if yes 
                        if (currentTime >= pawnFallenAsleepTime + timeToDream)
                        {
                            //loop for all the avaible dreams, then it checks if pawn has any of those dreams
                            foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
                            {
                                if (__instance.needs.mood.thoughts.memories.GetFirstMemoryOfDef(dream) == null)
                                {
                                    eligibleForDream = true;
                                }
                                else
                                {
                                    eligibleForDream = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            eligibleForDream = false;
                        }
                    }

                    if (eligibleForDream && __instance.needs.rest.CurCategory == RimWorld.RestCategory.Rested)
                    {
                        //variable that holds sum of all chances for all the dreams to generate a random number
                        float totalDreamChance = 0;

                        //loop that fills out totalDreamChance to generate a random number later
                        foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
                        {
                            totalDreamChance += CheckDreamChance(dream, __instance);
                        }
                        var dreamChanceRoll = Rand.Range(0, totalDreamChance);

                        //variable that allows for determining rolls
                        var dreamChanceProgress = 0.0f;

                        //loop for applying dreams (thoughts)
                        foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
                        {
                            var chanceForDream = CheckDreamChance(dream, __instance);

                            if (dreamChanceRoll < dreamChanceProgress + chanceForDream)
                            {
                                __instance.needs.mood.thoughts.memories.TryGainMemory(dream, null);
                                if (dream.triggers != null)
                                {
                                    __instance.mindState.mentalStateHandler.TryStartMentalState(dream.triggers[Rand.RangeInclusive(0, dream.triggers.Count - 1)], null, true, false, null, false);
                                }



                                //__instance.jobs.EndCurrentJob(Verse.AI.JobCondition.InterruptForced, false, true);

                                //__instance.jobs.EndCurrentJob(JobCondition.InterruptForced, true, true);
                                //Log.Message("Dream applied: " + dream + " Chance roll is: " + dreamChanceRoll + " Chance progress: " + dreamChanceProgress + " Progress: " + (dreamChanceProgress + chanceForDream) + " Chance :" + (chanceForDream / totalDreamChance) * 100 + "%");
                                return;
                            }
                            else
                            {
                                dreamChanceProgress += chanceForDream;
                            }
                        }
                    }
                }
                else if (!IsAwake(__instance))
                {
                    //Messages.Message("Pawn awake. Adding " + __instance.Name + " to the list", MessageTypeDefOf.NeutralEvent);
                    SleepingPawnTracker.SleepingList.Remove(__instance);
                    SleepingPawnTracker.SleepingList.Add(__instance, Find.TickManager.TicksGame);
                    return;
                }
            }
            else if (SleepingPawnTracker.SleepingList.ContainsKey(__instance))
            {
                //Messages.Message("Deleted " + __instance.Name + "Dead? : " + __instance.Dead, MessageTypeDefOf.NeutralEvent);
                SleepingPawnTracker.SleepingList.Remove(__instance);
            }
        }
    }
}
