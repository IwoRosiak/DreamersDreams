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
            var harmony = new Harmony("com.company.QarsoonMeel.DreamersDreams");

            MethodInfo targetmethod = AccessTools.Method(typeof(Verse.Pawn), "Tick");

            HarmonyMethod prefixmethod = new HarmonyMethod(typeof(DreamersDream.HarmonyPatches).GetMethod("CheckSleep_Prefix"));

            harmony.Patch(targetmethod, prefixmethod, null);
        }

        public static void CheckSleep_Prefix(Pawn __instance)
        {
            PawnDreamHandler.Tick(__instance);
        }
    }
}