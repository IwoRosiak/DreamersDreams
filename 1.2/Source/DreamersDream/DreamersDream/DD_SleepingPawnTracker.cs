using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public class SleepingPawnTracker
    {
        public static Dictionary<Pawn, int> SleepingList = new Dictionary<Pawn, int>();
    }
}
