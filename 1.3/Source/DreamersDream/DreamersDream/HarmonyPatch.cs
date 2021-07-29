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
            foreach (DD_ThoughtDef dream in DD_ThoughtDefArray.dreams)
            {
                totalDreams++;
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