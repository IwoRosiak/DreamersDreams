using HarmonyLib;
using System.Reflection;
using Verse;


namespace DreamersDream
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("com.company.QarsoonMeel.DreamersDreams");

            MethodInfo targetmethod = AccessTools.Method(typeof(Verse.Pawn), "TickRare");

            HarmonyMethod postfixmethod = new HarmonyMethod(typeof(DreamersDream.HarmonyPatches).GetMethod("CheckSleep_Postfix"));

            harmony.Patch(targetmethod, null, postfixmethod);
        }
        public static void CheckSleep_Postfix(Pawn __pawn)
        {
            var currentTime = Find.TickManager.TicksGame;

            //is the instance a colonist and is it dead
            if (__pawn.RaceProps.Humanlike && !__pawn.Dead)
            {
                //needs to use different classes but it checks if the pawn is resting
                if (!(__pawn.health.capacities.CanBeAwake && (!__pawn.Spawned || __pawn.CurJob == null || __pawn.jobs.curDriver == null || !__pawn.jobs.curDriver.asleep)))
                {
                    //variable that holds whether the pawn can get a dream or not
                    var eligibleForDream = false;

                    //checks if pawn is on the dictionary to check last time it was awake
                    if (SleepingPawnTracker.SleepingList.ContainsKey(__pawn.ThingID))
                    {
                        //saves the time the pawn has fallen asleep
                        var pawnFallenAsleepTime = SleepingPawnTracker.SleepingList[__pawn.ThingID];
                        //Messages.Message("First sleep: " + pawnFallenAsleepTime + "Current time :" + currentTime, MessageTypeDefOf.NeutralEvent); //debug message about sleeping times

                        //timeToDream is the time pawn needs to sleep to be eligible for a dream
                        var timeToDream = Rand.Range(10000, 15000);

                        //checks if pawn has slepted enough to get a dream. If yes then it checks if the pawn has a dream already if yes 
                        if (currentTime >= pawnFallenAsleepTime + timeToDream)
                        {
                            //loop for all the avaible dreams, then it checks if pawn has any of those dreams
                            foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
                            {
                                if (__pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(dream) == null)
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

                    if (eligibleForDream)
                    {
                        //variable that holds sum of all chances for all the dreams to generate a random number
                        float totalDreamChance = 0;

                        //loop that fills out totalDreamChance to generate a random number later
                        foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
                        {
                            totalDreamChance += CheckForHigherChanceDream(dream);
                        }
                        var dreamChanceRoll = Rand.Range(0, totalDreamChance);

                        //variable that allows for determining rolls
                        var dreamChanceProgress = 0.0f;

                        //loop for applying dreams (thoughts)
                        foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
                        {
                            var chanceForDream = CheckForHigherChanceDream(dream);

                            if (dreamChanceRoll < dreamChanceProgress + chanceForDream)
                            {
                                __pawn.needs.mood.thoughts.memories.TryGainMemory(dream, null);
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
                else
                {
                    SleepingPawnTracker.SleepingList.Remove(__pawn.ThingID);
                    SleepingPawnTracker.SleepingList.Add(__pawn.ThingID, Find.TickManager.TicksGame);
                    return;
                }
            }

            float CheckForHigherChanceDream(DD_ThoughtDef dream)
            {
                /*if (CheckForHigh() && dream.defName == "VeryGoodDream")
                {
                    return dream.chance + 1000;

                }*/
                return dream.chance;
            }

            /*bool CheckForHigh()
            {
                for (int i = 0; i < __pawn.health.hediffSet.hediffs.Count; i++)
                {
                    Hediff hediff = __pawn.health.hediffSet.hediffs[i];

                    if (hediff.def.defName == "DreamHigh")
                    {
                        //Messages.Message("Dream High Detected!", MessageTypeDefOf.NeutralEvent);
                        return true;
                    }
                }
                return false;
            }*/
        }
    }
}
