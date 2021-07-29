using HarmonyLib;
using System.Reflection;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    internal static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var totalDreams = 0;

            foreach (DreamDef dream in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(DreamDef)))
            {
                totalDreams++;
                PawnDreamTracker.listOfAllDreamDefs.Add(dream);

                //if you want to see if you dream loads correctly then uncomment that
                //Log.Message("Loaded " + dream.defName + ".");
                //Log.Message("It is " + PawnDreamTracker.listOfAllDreamDefs.IndexOf(dream) + " dream on the list.");
                //Log.Message("With chance of " + dream.chance + ".");
            }
            Log.Message("Dreamer's Dreams: succesfully loaded " + totalDreams + " dreams.");

            var harmony = new Harmony("com.company.QarsoonMeel.DreamersDreams");

            MethodInfo targetmethod = AccessTools.Method(typeof(Verse.Pawn), "Tick");

            HarmonyMethod prefixmethod = new HarmonyMethod(typeof(DreamersDream.HarmonyPatches).GetMethod("CheckSleep_Prefix"));

            harmony.Patch(targetmethod, prefixmethod, null);
        }

        public static void CheckSleep_Prefix(Pawn __instance)
        {
            PawnDreamTracker.Tick(__instance);
        }
    }
}