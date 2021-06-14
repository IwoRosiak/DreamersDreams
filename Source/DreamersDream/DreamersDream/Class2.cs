using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public class SleepingPawnTracker
    {
        public static Dictionary<string, int> SleepingList = new Dictionary<string, int>();
    }
}
