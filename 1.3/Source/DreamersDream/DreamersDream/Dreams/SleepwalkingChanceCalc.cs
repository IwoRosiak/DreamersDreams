using Verse;

namespace DreamersDream
{
    internal static class SleepwalkingChanceCalc
    {
        public static SleepwalkingType ChooseSleepwalkingType(this DreamsComp comp)
        {
            float totalChance = 1;

            totalChance += comp.getPawnAggressiveness;

            float roll = Rand.Range(0, totalChance);

            if (roll <= 1)
            {
                return SleepwalkingType.calm;
            }
            else if (roll <= 1 + comp.getPawnAggressiveness)
            {
                return SleepwalkingType.rage;
            }
            else
            {
                Log.Warning("Dreamer's Dreams: Failed to choose sleepwalking type.");
                return SleepwalkingType.calm;
            }
        }
    }
}